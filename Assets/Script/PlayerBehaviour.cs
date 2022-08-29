using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;

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
    public float angleFix = 10f;

    [Space(1)]
    [Header("- Shot Behaviour")]
    public float maxEnemyDistance = 20f;
    public int maxEnemy = 4;

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
        Vector2 lookDir = new Vector2(0,0);

        // If an enemy is found
        if (weapon.getIsShooting() & DoesEnemyExist())
        {
            lookDir = FindClosestEnemy().transform.position - player.transform.position;
        } 
        // Enemy not found
        else
        {
            lookDir = new Vector2(joystick.Horizontal, joystick.Vertical);
        }

        // If joystick is moving or an enemy is found
        if (!lookDir.Equals(new Vector2(0,0)))
        {
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            angle += angleFix;

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

    public bool DoesEnemyExist()
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");

        if (gos.Length == 0) return false;

        return true;
    }

    // Find nearest object with Enemy Tag
    public GameObject FindClosestEnemy()
    {
        // Array di Enemy attuali
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");

        var nearest = gos
          .OrderBy(t => Vector3.Distance(player.transform.position, t.transform.position))
          .Take(maxEnemy)
          .OrderBy(t => t.GetComponent<Enemy>().life)
          .FirstOrDefault();

        return nearest;
    }

}

public class EnemyTest
{
    public float distance = 0;
    public int arrayPos = 0;
}


