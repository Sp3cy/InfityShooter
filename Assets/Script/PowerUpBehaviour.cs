using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{
    public static float grenadeDamage = 10f;

    public GameObject grenadePrefab;
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

        grenadeRb.AddForce(player.transform.up * grenadeForce, ForceMode2D.Force);

        // Wait for grenade explosion
        yield return new WaitForSeconds(grenadeExplosionT);
        // Set the collider active
        grenadeCol.enabled = true;

        // Wait fix for collider function
        yield return new WaitForSeconds(0.1f);
        grenadeCol.enabled = false;

        // Wait for animT
        yield return new WaitForSeconds(1f);

        Destroy(grenade);

        // Wait for next grenade
        yield return new WaitForSeconds(grenadeWaitT);

        StartCoroutine(Grenades());
    }
}
