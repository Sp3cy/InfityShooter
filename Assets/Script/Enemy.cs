using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("- Enemy Stats")]
    public float life = 10f; 
    public float speed = 1f;

    [Space(1)]
    [Header("- Rotation Time")]
    public float lerpT = 0.5f;

    private GameObject player;
    private Rigidbody2D rbEnemy;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rbEnemy = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveToPlayer();

        if (life <= 0)
        {
            Dead();
        }
    }

    public void Hitted(float damage)
    {
        life -= damage;
    }

    private void Dead()
    {
        Destroy(gameObject);
    }

    private void MoveToPlayer()
    {
        Vector2 lookDir;

        // Sposta i nemici verso il player
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);

        // Gira il rigidbody dei nemici
        lookDir = player.transform.position - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rbEnemy.rotation = Mathf.LerpAngle(rbEnemy.rotation, angle, lerpT);
    }
}
