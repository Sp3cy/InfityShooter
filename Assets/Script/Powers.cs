using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Powers : MonoBehaviour
{
    [Header("- Powers")]
    public PowersStruct[] powers;

    [Header("- Granata")]
    public AudioSource grenadeExplosionSound;
    public GameObject grenadePrefab;
    public GameObject grenadeParticleExplosion;
    public float grenadeForce = 10f;
    public float grenadeTimer = 1f;
    public float grenadeRechargeT = 10f;
    public float offsetGrenade = 1f;

    public static float grenadeDamage = 20f;

    [Header("- Bolt")]
    public AudioSource boltSoundfx;
    public GameObject boltPrefab;
    public GameObject boltHitZone;
    public GameObject particleBruciatura;
    public float boltRechargeT = 2f;

    public static float boltDamage = 20f;

    [Header("- Kunai")]
    public GameObject kunaiPrefab;
    public int kunayDestroyTime = 3;

    public float kunayRechargeT = 3f;
    public float kunaiForce = 35f;

    public static float kunaiDamage = 20f;




    [System.NonSerialized]
    public GameObject player;

    public PowersStruct GetRandPowerUp()
    {
        int randomIndex = Random.Range(0, powers.Length);

        var pw = powers
            .Where(powerUp => powerUp.id == randomIndex)
            .FirstOrDefault();

        return pw;
    }

    public IEnumerator Grenade()
    {
        var grenade = Instantiate(grenadePrefab, player.transform.position, player.transform.rotation);
        Rigidbody2D grenadeRb = grenade.GetComponent<Rigidbody2D>();
        Collider2D grenadeCol = grenade.GetComponent<Collider2D>();

        grenadeCol.enabled = false;

        grenadeRb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * grenadeForce, ForceMode2D.Force);

        // Wait for grenade timer
        yield return new WaitForSeconds(grenadeTimer);

        // Set the collider active
        grenadeCol.enabled = true;

        // Wait fix for collider function
        yield return new WaitForFixedUpdate();
        grenadeCol.enabled = false;
        var grenadeExplodeEffect = Instantiate(grenadeParticleExplosion, grenade.transform.position, Quaternion.identity);

        grenadeExplosionSound.pitch = Random.Range(1f, 1.3f);
        grenadeExplosionSound.Play();
        Destroy(grenade);

        // Wait for particles anim
        yield return new WaitForSeconds(grenadeExplodeEffect.GetComponent<ParticleSystem>().main.duration);
        Destroy(grenadeExplodeEffect);

        yield return null;
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

        yield return null;
    }

    public IEnumerator Kunai()
    {
        var kunai = Instantiate(kunaiPrefab, player.transform.position, player.transform.rotation);
        Rigidbody2D kunaiRb = kunai.GetComponent<Rigidbody2D>();
        kunaiRb.AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * grenadeForce, ForceMode2D.Force);

        yield return new WaitForSeconds(kunayDestroyTime);
        Destroy(kunai);
        yield return null;
    }

}

[System.Serializable]
public class PowersStruct
{
    public string name;
    public int id;
    public string[] descLevel;
    public int level = 0;
}
