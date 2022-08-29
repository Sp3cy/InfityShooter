using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("- Weapon Stats")]
    public float bulletDmg = 20f;
    public float bulletForce = 30f;
    public float fireGap = 0.5f;

    [Space(1)]
    [Header("- Bullet Prefab")]
    public GameObject bulletPrefab;

    private bool selected;
    public bool isShooting;
    private Transform firePos;

    private IEnumerator coroShooting;

    // Start is called before the first frame update
    void Start()
    {
        coroShooting = Shooting();

        selected = false;
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartShooting()
    {
        StartCoroutine(coroShooting);
    }
    public void StopShooting()
    {
        StopCoroutine(coroShooting);

        // Bug Handler
        isShooting = false;
        coroShooting = Shooting();
    }

    // Spara a ripetizione ogni tot s finchè non viene stoppata la coroutine
    public IEnumerator Shooting()
    {
        isShooting = true;

        // Crea l'oggetto bulletPrefab e gli assegna il damage
        GameObject bullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        bullet.GetComponent<Bullet>().setDamage(bulletDmg);

        // Aggiunge la forza per far andare avanti il colpo
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePos.up * bulletForce, ForceMode2D.Impulse);

        yield return new WaitForSeconds(fireGap);

        // Fa ripartire questa coroutine
        yield return Shooting();
    }

    // 
    public void setSelected(bool newBool)
    {
        selected = newBool;
    }

    // Get if player is shooting
    public bool getIsShooting()
    {
        return isShooting;
    }

    // Set FirePos to handle bugs -- PlayerBehaviour
    public void setFirePos(Transform newFirePos)
    {
        firePos = newFirePos;
    }

}
