using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("- Bullet Stats")]
    public float bulletDuration = 2f;

    private float damage = 0f;

    private void Start()
    {
        StartCoroutine(DestroyAfterSec(bulletDuration));
    }

    public void setDamage(float newDamage)
    {
        damage = newDamage;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (damage == 0f) Debug.LogError("- Damage not setted");

        // if enemy is been hitted
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<Enemy>().Hitted(damage);
        }

        Destroy(gameObject);
    }

    // Disrugge il gameobject dopo tot secondi
    private IEnumerator DestroyAfterSec(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        // Handle bug
        if (gameObject.Equals(gameObject)) Destroy(gameObject);
    }
}
