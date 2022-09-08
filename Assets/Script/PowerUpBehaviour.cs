using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public static float grenadeDamage = 10f;

    public GameObject grenadePrefab;
    public GameObject grenadeParticlesPrefab;
    public float grenadeForce = 10f;
    public float grenadeTimer = 1f;
    public float grenadeRechargeT = 10f;
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

        grenadeRb.AddForce(player.transform.up * grenadeForce, ForceMode2D.Force);

        // Wait for grenade timer
        yield return new WaitForSeconds(grenadeTimer);

        // Set the collider active
        grenadeCol.enabled = true;

        // Wait fix for collider function
        yield return new WaitForFixedUpdate();
        grenadeCol.enabled = false;

        // Instantiate particles
        var grenadeParticles = Instantiate(grenadeParticlesPrefab, grenade.transform.position, Quaternion.identity);

        Destroy(grenade);

        // Wait for next grenade
        yield return new WaitForSeconds(grenadeRechargeT);

        StartCoroutine(Grenades());
    }
}
