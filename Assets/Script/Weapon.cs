using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
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
    

    [Space(1)]
    [Header("- Bullet Prefab")]
    public GameObject bulletPrefab;

    private float RowBullets;
    private float keepFireRate;
    private bool isShooting;
    private bool btnFirePressed;
    private Transform firePos;
    private AudioSource fireSound;
    public AudioSource reloadSound;

    // Start is called before the first frame update
    void Start()
    {
        GameData.AmmoCount = ammoMax;
        RowBullets = 0;
        keepFireRate = fireRate;

        btnFirePressed = false;
        isShooting = false;
        StartCoroutine(Shooting());

        // Reload every time ammocount = 0
        StartCoroutine(Reload(reloadT));

        fireSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (btnFirePressed) StartShooting();
        else StopShooting();
    }

    public void StartShooting()
    {
        isShooting = true;
    }
    public void StopShooting()
    {
        isShooting = false;
        RowBullets = 0;
        fireRate = keepFireRate;
    }

    // Spara a ripetizione ogni tot s finchè non viene stoppata la coroutine
    public IEnumerator Shooting()
    {
        if (GameData.AmmoCount <= 0) fireRate = keepFireRate; 

        yield return new WaitUntil(() => isShooting == true & GameData.AmmoCount > 0);

        // Crea l'oggetto bulletPrefab e gli assegna il damage
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        bullet.GetComponent<Bullet>().setDamage(bulletDmg);

        // Aggiunge la forza per far andare avanti il colpo
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePos.up * bulletForce, ForceMode2D.Impulse);

        RowBullets++;
        GameData.AmmoCount--;
        fireSound.Play();

        yield return new WaitForSeconds(fireRate);

        if (fireRateTarget < fireRate) fireRate = Mathf.Pow(coeffRPS, RowBullets) * keepFireRate;
        else fireRate = fireRateTarget;
        // Fa ripartire questa coroutine
        yield return Shooting();
    }

    private IEnumerator Reload(float reloadT)
    {
        yield return new WaitUntil(() => GameData.AmmoCount <= 0);

        isShooting = false;
        RowBullets = 0;

        reloadSound.Play();

        Debug.Log("reloading");
        yield return new WaitForSeconds(reloadT);

        GameData.AmmoCount = ammoMax;

        // Fa ripartire questa coroutine
        yield return Reload(reloadT);
    }

    public void ReloadWeapon()
    {
        if (GameData.AmmoCount > 0 & GameData.AmmoCount < ammoMax) GameData.AmmoCount = 0;
    }

    public void SetBtnFirePressed(bool value)
    {
        btnFirePressed = value;
    }

    // Get if player is shooting
    public bool IsShooting()
    {
        return isShooting;
    }

    // Set FirePos to handle bugs -- PlayerBehaviour
    public void setFirePos(Transform newFirePos)
    {
        firePos = newFirePos;
    }

    public int GetAmmoMax ()
    {
        return ammoMax;
    }


}
