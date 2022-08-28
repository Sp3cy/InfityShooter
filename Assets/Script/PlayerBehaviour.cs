using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("- Player Stats")]
    public float life = 100f;

    [Header("- Objects")]
    public Rigidbody2D player;
    public Weapon weapon;

    [Space(1)]
    [Header("- Rotation Time")]
    public float lerpT = 0.5f;

    [Space(1)]
    [Header("- Shot Behaviour")]
    public float maxEnemyDistance = 20f;

    // Se viene preso direttamente da weapon non funziona
    private Transform firePos;

    private Joystick joystick;

    // Start is called before the first frame update
    void Start()
    {
        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        firePos = GameObject.FindGameObjectWithTag("Firepoint").GetComponent<Transform>();

        // Handle firePos bug
        weapon.setFirePos(firePos);
    }

    private void Update()
    {
        // Se il player muore
        if (life <= 0)
        {
            Dead();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lookDir;

        // If an enemy is found
        /* if (FindClosestEnemy() != null)
         {
            lookDir = FindClosestEnemy().transform.position - player.transform.position;

             // Se non sta sparando, inizia a sparare
            // if (!weapon.getIsShooting()) weapon.StartShooting();
         } 
         // Enemy not found
         else
         {
             lookDir = new Vector2(joystick.Horizontal, joystick.Vertical);

             // Se sta sparando, smette e reset variabili
            /* if (weapon.getIsShooting())
             {
                 weapon.StopShooting();
             }*/
        //}

        lookDir = new Vector2(joystick.Horizontal, joystick.Vertical);

        // If joystick is moving or an enemy is found
        if (!lookDir.Equals(new Vector2(0,0)))
        {
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            player.rotation = Mathf.LerpAngle(player.rotation, angle, lerpT);
        }
    }

    public void Dead()
    {
        transform.position = new Vector2(0, 0);
        life = 100f;
    }

    public void HittedByEnemy(float damage)
    {
        life -= damage;
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
