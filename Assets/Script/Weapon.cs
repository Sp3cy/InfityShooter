using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float bulletDmg = 10f;
    public float bulletForce = 20f;
    public float fireGap = 0.5f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    private bool selected;
    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        selected = false;
        isShooting = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSelected(bool newBool)
    {
        selected = newBool;
    }

    public bool getIsShooting()
    {
        return isShooting;
    }

    public void setIsShooting(bool newBool)
    {
        isShooting = newBool;
    }

    public IEnumerator Shooting(Transform firePoint)
    {
        isShooting = true;
        yield return new WaitForSeconds(fireGap);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Bullet>().setDamage(bulletDmg);

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        yield return Shooting(firePoint);
    }

}
