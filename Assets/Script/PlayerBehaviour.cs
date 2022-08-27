using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    public Joystick joystick;

    public Rigidbody2D player;
    public float lerpT = 0.5f;

    public Transform firePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public float fireGap = 0.5f;

    public float maxEnemyDistance = 20f;

    private IEnumerator shooting;
    private bool isShooting;

    // Start is called before the first frame update
    void Start()
    {
        // Get selcted weapon data --- TO DO

        shooting = Shooting(bulletPrefab, firePoint, bulletForce, fireGap);
        isShooting = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lookDir;

        // If an enemy is found
        if (FindClosestEnemy() != null)
        {
            lookDir = FindClosestEnemy().transform.position - player.transform.position;

            if (!isShooting) StartCoroutine(shooting);
            

        } 
        // Enemy not found
        else
        {
            lookDir = new Vector2(joystick.Horizontal, joystick.Vertical);

            if (isShooting)
            {
                StopCoroutine(shooting);
                shooting = Shooting(bulletPrefab, firePoint, bulletForce, fireGap);
                isShooting = false;
            }
        }

        // If joystick is moving
        if (!lookDir.Equals(new Vector2(0,0)))
        {
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            player.rotation = Mathf.LerpAngle(player.rotation, angle, lerpT);
        }
    }

    // Find nearest object with Enemy Tag
    public GameObject FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = maxEnemyDistance;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    // Shoot a bullet every fireGap(seconds)
    private IEnumerator Shooting(GameObject bulletPrefab, Transform firePoint, float bulletForce, float fireGap)
    {
        isShooting = true;
        yield return new WaitForSeconds(fireGap);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);

        yield return Shooting(bulletPrefab, firePoint, bulletForce, fireGap);
    }

}
