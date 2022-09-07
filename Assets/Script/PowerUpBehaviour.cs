using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public static float grenadeDamage = 20f;
    [Header("- Granata")]
    public AudioSource grenadeExplosionSound;
    public GameObject grenadePrefab;
    public GameObject grenadeParticleExplosion;
    public float grenadeForce = 10f;
    public float grenadeExplosionT = 1f;
    public float grenadeWaitT = 10f;
    public float offsetGrenade = 1f;

    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Momentaneo
        StartCoroutine(Grenades());
    }

    public IEnumerator Grenades()
    {
        var grenade = Instantiate(grenadePrefab, player.transform.position, player.transform.rotation);
        Rigidbody2D grenadeRb = grenade.GetComponent<Rigidbody2D>();
        Collider2D grenadeCol = grenade.GetComponent<Collider2D>();

        grenadeCol.enabled = false;

        grenadeRb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * grenadeForce, ForceMode2D.Force);

        // Wait for grenade explosion
        yield return new WaitForSeconds(grenadeExplosionT);
        // Set the collider active
        grenadeCol.enabled = true;

        // Wait fix for collider function
        yield return new WaitForFixedUpdate();
        grenadeCol.enabled = false;
        var grenadeExplodeEffect = Instantiate(grenadeParticleExplosion, grenade.transform.position, Quaternion.identity);

        grenadeExplosionSound.pitch = Random.Range(1,1.2f);

        grenadeExplosionSound.Play();
        Destroy(grenade);

        // Wait for particles anim
        yield return new WaitForSeconds(grenadeExplodeEffect.GetComponent<ParticleSystem>().main.duration);
        Destroy(grenadeExplodeEffect);

        // Wait for next grenade
        yield return new WaitForSeconds(grenadeWaitT);

        StartCoroutine(Grenades());
    }
}
