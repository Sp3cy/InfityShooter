using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("- Bullet Stats")]
    public float bulletDuration = 2f;
    public float speed = 10f;

    private float damage = 0f;

    //private int _layerHittable = 3;

    private bool destroyed = false;

    private Animator animator;
    private Rigidbody2D rb;
    private Collider2D bulletCollider;


    private void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        bulletCollider = gameObject.GetComponent<Collider2D>();

        StartCoroutine(DestroyAfterSec(bulletDuration));
    }

    public void SetDamage(float newDamage)
    {
        damage = newDamage;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (damage == 0f) Debug.LogError("- Damage not setted");
        
        // BUG HANDLER: read at AnimazioneBulletEsplode
        if (rb == null) return;

        // if enemy is been hitted
        if (collision.gameObject.tag == "Enemy" && !destroyed)
        {
            destroyed = true;
            collision.gameObject.GetComponent<Enemy>().Hitted(damage);

            AnimazioneBulletEsplode();
        }

        
    }

    // Disrugge il gameobject dopo tot secondi
    private IEnumerator DestroyAfterSec(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Handle bug
        if (rb != null) Destroy(gameObject);
    }

    // Animazione Bullet al Contatto
    private void AnimazioneBulletEsplode()
    {
        // BUG: bullet quando hitta appena instanziato non imposta i valori di rb, animator e collider2D, i quali risultano in null (penso)
        // Da capire perche'

        try
        {
            // Disabilita collider, ferma bullet nello spazio
            bulletCollider.enabled = false;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;

            // Start anim; bullet get destroyed in animator code
            animator.SetBool("Hit", true);
        }
        catch {
            Debug.LogWarning("AVVISO:  Il bullet ha avuto un problema diverso da quello conosciuto! (at Bullet.cs:AnimazioneBulletEsplode)");
        }
        
    }
}

