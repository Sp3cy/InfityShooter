using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("- Bullet Stats")]
    public float bulletDuration = 2f;
    public float speed = 10f;

    private float damage = 0f;
    private float BulletAnimDurata;

    private int _layerHittable = 3;

    private bool destroyed = false;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D bulletCollider;


    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        bulletCollider = gameObject.GetComponent<Collider2D>();

        SetAnimationTime();
        StartCoroutine(DestroyAfterSec(bulletDuration));
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damage == 0f) Debug.LogError("- Damage not setted");

        // if enemy is been hitted
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Hitted(damage);
        }

        if (collision.gameObject.layer != _layerHittable) return;

        StartCoroutine(AnimazioneBulletEsplode(BulletAnimDurata));
    }

    // Disrugge il gameobject dopo tot secondi
    private IEnumerator DestroyAfterSec(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Handle bug
        if (!destroyed) Destroy(gameObject);
    }

    // Animazione Bullet Contatto

    private IEnumerator AnimazioneBulletEsplode(float seconds)
    {
        // Se ha colpito qualcosa è come se fosse stato distrutto
        destroyed = true;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        bulletCollider.isTrigger = true;

        animator.SetBool("Hit", true);
        yield return new WaitForSeconds(seconds);
        animator.SetBool("Hit", false);

        
        Destroy(gameObject);

    }

    private void SetAnimationTime()
    {
        // Get Animation duration
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;

        foreach (AnimationClip clip in clips)
        {
            switch (clip.name)
            {
                case "BulletHit":
                    BulletAnimDurata = clip.length;
                    break;

                case null:
                    break;
            }
        }
    }
}

