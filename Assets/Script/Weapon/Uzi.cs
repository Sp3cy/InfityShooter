using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : MonoBehaviour, Weapon
{
    [Header("- Objects")]
    public Transform firePos;

    [Header("- Weapon Stats")]
    public float bulletDmg = 20f;
    public float bulletForce = 30f;
    [Space(2)]
    public float fireRate = 0.5f;
    public float fireRateTarget = 0.1f;
    [Tooltip("MUST BE: VALUE < 1")]
    public float coeffRPS = 0.6f;
    [Space(2)]
    public int ammoMax;
    public float reloadT = 1f;

    [Space(5)]
    [Header("- Bullet Prefab")]
    public GameObject bulletPrefab;

    [Space(5)]
    [Header("- Sound FX")]
    public AudioSource fireSound;
    public AudioSource reloadSound;

    private float RowBullets;
    private float keepFireRate;
    private float fireRatedeltaT;

    private GameObject gameManager;

    private void Awake()
    {
        GameData.AmmoCount = ammoMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        // Set keepFireRate
        keepFireRate = fireRate;
        fireRatedeltaT = 0;
    }

    public void Shoot()
    {
        if (fireRatedeltaT <= 0)
        {
            StartCoroutine(ShootCoro());
            fireRatedeltaT = fireRate;
        }
        else
        {
            fireRatedeltaT -= Time.deltaTime;
        }
        
    }

    public void ResetFireRate()
    {
        fireRate = keepFireRate;
        RowBullets = 0;
    }
 


    public IEnumerator ShootCoro()
    {
        // Se munizioni maggiori di 0
        // Instazia proiettili

        if (GameData.AmmoCount > 0)
        {
            // Crea l'oggetto bulletPrefab e gli assegna il damage
            GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
            bullet.GetComponent<Bullet>().SetDamage(bulletDmg);

            // Aggiunge la forza per far andare avanti il colpo
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePos.up * bulletForce, ForceMode2D.Impulse);

            RowBullets++;
            GameData.AmmoCount--;
            fireSound.Play();

            yield return new WaitForSeconds(fireRate);

            if (fireRateTarget < fireRate) fireRate = Mathf.Pow(coeffRPS, RowBullets) * keepFireRate;
            else fireRate = fireRateTarget;
        }
    }

    public void Reload()
    {
        StartCoroutine(ReloadCoro());
    }

    public IEnumerator ReloadCoro()
    {
        // Reload bro
        reloadSound.Play();

        yield return new WaitForSeconds(reloadT);

        // Reload and resume shooting
        GameData.AmmoCount = ammoMax;
    }
}
