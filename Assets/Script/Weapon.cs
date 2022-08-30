using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("- Weapon Stats")]
    public float bulletDmg = 20f;
    public float bulletForce = 30f;
    public float fireGap = 0.5f;
    public int ammoMax = 30;
    public float reloadT = 1f;

    [Space(1)]
    [Header("- Bullet Prefab")]
    public GameObject bulletPrefab;

    private bool isShooting;
    private Transform firePos;
    private AudioSource fireSound;

    // Start is called before the first frame update
    void Start()
    {
        GameData.AmmoCount = ammoMax;

        isShooting = false;
        StartCoroutine(Shooting());

        // Reload every time ammocount = 0
        StartCoroutine(Reload(reloadT));

        fireSound = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void StartShooting()
    {
        isShooting = true;
    }
    public void StopShooting()
    {
        isShooting = false;
    }

    // Spara a ripetizione ogni tot s finchè non viene stoppata la coroutine
    public IEnumerator Shooting()
    {
        yield return new WaitUntil(() => isShooting == true & GameData.AmmoCount > 0);

        // Crea l'oggetto bulletPrefab e gli assegna il damage
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        bullet.GetComponent<Bullet>().setDamage(bulletDmg);

        // Aggiunge la forza per far andare avanti il colpo
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePos.up * bulletForce, ForceMode2D.Impulse);

        GameData.AmmoCount--;
        fireSound.Play();

        yield return new WaitForSeconds(fireGap);

        // Fa ripartire questa coroutine
        yield return Shooting();
    }

    private IEnumerator Reload(float reloadT)
    {
        yield return new WaitUntil(() => GameData.AmmoCount <= 0);

        isShooting = false;

        Debug.Log("reloading");
        yield return new WaitForSeconds(reloadT);

        GameData.AmmoCount = ammoMax;

        // Fa ripartire questa coroutine
        yield return Reload(reloadT);
    }

    public void ReloadWeapon()
    {
        if (GameData.AmmoCount > 0) GameData.AmmoCount = 0;
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

}
