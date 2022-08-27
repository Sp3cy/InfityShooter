using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float life = 10f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
}
