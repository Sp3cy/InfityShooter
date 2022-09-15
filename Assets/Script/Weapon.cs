using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
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
    private bool isShooting;

    private GameObject gameManager;
    private Skill skillScript;

    private void Awake()
    {
        GameData.AmmoCount = ammoMax;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        // Get skill script
        skillScript = gameManager.GetComponent<Skill>();

        // Set keepFireRate
        keepFireRate = fireRate;

        // Set variables to 0
        StopShooting();

        // Start the coroutine -- It will wait until isShooting == true
        StartCoroutine(Shooting());

        // Reload every time ammocount = 0
        StartCoroutine(Reload(reloadT));

        // Set isShooting to true
        StartShooting();
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

        yield return new WaitUntil(() => !skillScript.IsSkillShooting() & isShooting & GameData.AmmoCount > 0);

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
        // Fa ripartire questa coroutine
        yield return Shooting();
    }

    private IEnumerator Reload(float reloadT)
    {
        yield return new WaitUntil(() => GameData.AmmoCount <= 0);

        // Pause the shooting coroutine
        StopShooting();
        reloadSound.Play();

        yield return new WaitForSeconds(reloadT);

        // Reload and resume shooting
        GameData.AmmoCount = ammoMax;
        StartShooting();

        // Fa ripartire questa coroutine
        yield return Reload(reloadT);
    }

    // Get if player is shooting
    public bool IsShooting()
    {
        return isShooting;
    }
}
