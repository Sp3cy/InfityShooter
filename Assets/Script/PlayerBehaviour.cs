using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("- Objects")]
    public Rigidbody2D player;
    private Transform firePoint;
    public Weapon weapon;

    [Space(1)]
    [Header("- Rotation Time")]
    public float lerpT = 0.5f;

    [Space(1)]
    [Header("- Shot Behaviour")]
    public float maxEnemyDistance = 20f;

    private bool isShooting;
    private Joystick joystick;
    private IEnumerator shooting;

    // Start is called before the first frame update
    void Start()
    {
        shooting = weapon.Shooting(firePoint);
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();

        firePoint = GameObject.FindGameObjectWithTag("Firepoint").GetComponent<Transform>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lookDir;

        // If an enemy is found
        if (FindClosestEnemy() != null)
        {
            lookDir = FindClosestEnemy().transform.position - player.transform.position;

            if (!weapon.getIsShooting()) StartCoroutine(shooting);
            

        } 
        // Enemy not found
        else
        {
            lookDir = new Vector2(joystick.Horizontal, joystick.Vertical);

            if (weapon.getIsShooting())
            {
                StopCoroutine(shooting);
                weapon.setIsShooting(false);

                shooting = weapon.Shooting(firePoint);
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

}
