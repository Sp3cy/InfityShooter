using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{

    [Header("- Granata")]
    public AudioSource grenadeExplosionSound;
    public GameObject grenadePrefab;
    public GameObject grenadeParticleExplosion;
    public float grenadeForce = 10f;
    public float grenadeExplosionT = 1f;
    public float grenadeWaitT = 10f;
    public float offsetGrenade = 1f;

    public static float grenadeDamage = 20f;


    [Header("- Bolt")]
    public AudioSource boltSoundfx;
    public GameObject boltPrefab;
    public GameObject boltHitZone;
    public GameObject particleBruciatura;

    public static float boltDamage = 20f;



    private GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Momentaneo
        StartCoroutine(Grenades());
        StartCoroutine(Bolts());
        StartCoroutine(Bolts());
        StartCoroutine(Bolts());
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

        grenadeExplosionSound.pitch = Random.Range(1f,1.3f);

        grenadeExplosionSound.Play();
        Destroy(grenade);

        // Wait for particles anim
        yield return new WaitForSeconds(grenadeExplodeEffect.GetComponent<ParticleSystem>().main.duration);
        Destroy(grenadeExplodeEffect);

        // Wait for next grenade
        yield return new WaitForSeconds(grenadeWaitT);

        StartCoroutine(Grenades());
    }

    public IEnumerator Bolts()
    {         
        var bolt = Instantiate(boltPrefab, player.transform.localPosition + new Vector3(Random.Range(-4f,4f), Random.Range(-1.5f, 20f),0), Quaternion.Euler(55,180,0));
        var hitZone = Instantiate(boltHitZone,bolt.transform.position + new Vector3(0,-11.6f,0), Quaternion.identity);
        boltSoundfx.Play();
        yield return new WaitForFixedUpdate();
        Destroy(hitZone);
        yield return new WaitForSeconds(particleBruciatura.GetComponent<ParticleSystem>().main.duration);
        Destroy(bolt);

        yield return new WaitForSeconds(0f);
        StartCoroutine(Bolts());
    }
}
