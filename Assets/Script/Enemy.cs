using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    [Header("- Enemy Stats")]
    public float life = 50f;
    public float attack = 10f;
    public float speed = 2f;

    [Space(1)]
    [Header("- Rotation Time")]
    public float lerpT = 0.5f;


    private float animAtkTime;

    private GameObject player;
    private Rigidbody2D rbEnemy;
    private Animator animator;

    private IEnumerator atkEachSec;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbEnemy = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        SetAnimationTime();
        atkEachSec = AttackEachSecond(animAtkTime);
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();

        if (life <= 0)
        {
            Dead();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Quando entra in collisione col player
        if (collision.gameObject.tag == "Player")
        {
            StartCoroutine(atkEachSec);
            animator.SetBool("Attacca", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Quando entra in collisione col player
        if (collision.gameObject.tag == "Player")
        {
            StopCoroutine(atkEachSec);
            atkEachSec = AttackEachSecond(1);
            animator.SetBool("Attacca", false);
        }
    }

    private IEnumerator AttackEachSecond(float sec)
    {
        player.gameObject.GetComponent<PlayerBehaviour>().HittedByEnemy(attack);
        yield return new WaitForSeconds(sec);

        yield return AttackEachSecond(sec);
    }

    public void Hitted(float damage)
    {
        life -= damage;
       
    }

    private void Dead()
    {
        Destroy(gameObject);
    }

    private void MoveToPlayer()
    {
        Vector2 lookDir;

        // Sposta i nemici verso il player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (transform.position.x > player.transform.position.x) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    private void SetAnimationTime()
    {
        // Get Animation duration
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "LittleMonster_Attacca":
                    animAtkTime = clip.length;
                    break;

                case null:
                    break;
            }
        }
    }
}
