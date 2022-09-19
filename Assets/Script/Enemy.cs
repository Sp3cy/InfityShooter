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
    public float expDrop = 10f;

    [Space(1)]
    [Header("- Rotation Time")]
    public float lerpT = 0.5f;

    [Space(1)]
    [Header("- Animation")]
    public string name_atkAnim;
    public string name_hitAnim;
    public float waitAfetrAtkAnim;

    [Space(1)]
    [Header("- Sound FX")]
    public AudioSource sound_atkFX;
    public AudioSource sound_deathFX;

    [Space(1)]
    [Header("- Objects")]
    public GameObject enemyDeathEffectPrefab;

    private int index = 0;

    private float animAtkTime;
    private float animHitTime;

    private bool startCoro;
    private float keepLife;

    private Collider2D deathZone;

    private GameObject player;
    private Rigidbody2D rbEnemy;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        startCoro = false;
        keepLife = life;

        player = GameObject.FindGameObjectWithTag("Player");
        rbEnemy = gameObject.GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();

        deathZone = GameObject.FindGameObjectWithTag("DeathZone").GetComponent<Collider2D>();

        sound_atkFX = GameObject.Find("LittleMonsterAttackSound").GetComponent<AudioSource>();
        sound_deathFX = GameObject.Find("LittleMonesterDeathSound").GetComponent<AudioSource>();

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

        if (!gameObject.GetComponent<Collider2D>().IsTouching(deathZone))
        {
            gameObject.transform.position = GameMethods.RespawnEnemy(player);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "FlameThrower")
        {
            Hitted(Skill.flameThrowerDamage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Grenade")
        {
            Hitted(Powers.grenadeDamage);
        }
        if (collision.gameObject.tag == "Bolt")
        {
            Hitted(Powers.boltDamage);
        }
        if (collision.gameObject.tag == "Kunai")
        {
            Hitted(Powers.kunaiDamage);
        }
    }

    private void OnParticleCollision(GameObject other)
    {
        if (other.gameObject.tag == "CrazyCircle")
        {
            Hitted(Powers.crazyCircleDamage);         
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

        sound_atkFX.PlayOneShot(sound_atkFX.clip);

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

    public IEnumerator EnemyDeathEffect()
    {
        var deathFx = Instantiate(enemyDeathEffectPrefab, gameObject.transform.position, gameObject.transform.rotation);
        yield return new WaitForSeconds(1f);
        Destroy(deathFx);
    }
    private void Dead()
    {
        sound_deathFX.pitch = Random.Range(1.6f, 2.7f);
        sound_deathFX.Play();

        StartCoroutine(EnemyDeathEffect());

        GameData.EnemyDead++;
        GameData.ActualExp += expDrop;

        // Se i nemici totali instanziati sono maggiori del massimo di nemici
        if (GameData.MaxEnemy[index] < GameData.ActualEnemy[index])
        {
            GameData.ActualEnemy[index]--;
            Destroy(gameObject);
        } else
        {
            gameObject.transform.position = GameMethods.RespawnEnemy(player);
            life = keepLife;
        }
    }

    private void MoveToPlayer()
    {
        // Sposta i nemici verso il player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        if (transform.position.x > player.transform.position.x) gameObject.GetComponent<SpriteRenderer>().flipX = true;
        else gameObject.GetComponent<SpriteRenderer>().flipX = false;
    }

    public void SetIndex(int newIndex)
    {
        index = newIndex;
    }

    private void SetAnimationTime()
    {
        // Get Animation duration
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            if (clip.name == name_atkAnim) animAtkTime = clip.length;
            else if (clip.name == name_hitAnim) animHitTime = clip.length;
        }
    }
}
