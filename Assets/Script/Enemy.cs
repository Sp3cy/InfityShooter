using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float life = 10f; 
    public float speed = 1f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
}
