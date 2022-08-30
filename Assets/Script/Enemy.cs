using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy : MonoBehaviour
{
    [Header("- Enemy Stats")]
    public float life = 50f;
    public float attack = 10f;
    public float speed = 2f;
    public float knockback = 100f;

    [Space(1)]
    [Header("- Rotation Time")]
    public float lerpT = 0.5f;

    [Space(1)]
    [Header("- Anim Time")]
    public float waitAfetrAtkAnim;

    private float animAtkTime;
    private float animHitTime;

    private bool startCoro;
    private bool isPassedT;

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

        startCoro = false;
        isPassedT = false;

        SetAnimationTime();
        StartCoroutine(AttackEachSecond(animAtkTime, waitAfetrAtkAnim));
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
            startCoro = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Quando entra in collisione col player
        if (collision.gameObject.tag == "Player")
        {
            startCoro = false;
        }
    }

    private IEnumerator AttackEachSecond(float sec, float waitAfterAnim)
    {
        yield return new WaitUntil(() => startCoro == true);

        animator.SetBool("Attacca", true);
        player.gameObject.GetComponent<PlayerBehaviour>().HittedByEnemy(attack, knockback, gameObject.transform);
        
        yield return new WaitForSeconds(sec);
        animator.SetBool("Attacca", false);

        yield return new WaitForSeconds(waitAfterAnim);

        yield return AttackEachSecond(sec, waitAfterAnim);
    }

    public void Hitted(float damage)
    {
        life -= damage;

        if (!animator.GetBool("Colpito"))
        {
            StartCoroutine(HitAnim(animHitTime));
        }
        
    }

    private IEnumerator HitAnim(float sec)
    {
        animator.SetBool("Colpito", true);
        yield return new WaitForSeconds(sec);
        animator.SetBool("Colpito", false);
    }

    private void Dead()
    {
        Destroy(gameObject);
    }

    private void MoveToPlayer()
    {
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

                case "LittleMonster_Colpito":
                    animHitTime = clip.length;
                    break;

                case null:
                    break;
            }
        }
    }
}
