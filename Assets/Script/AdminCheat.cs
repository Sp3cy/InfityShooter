using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminCheat : MonoBehaviour
{
    private Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) & !weapon.getIsShooting())
        {
            weapon.StartShooting();
        }

        if (Input.GetKeyUp(KeyCode.Space) & weapon.getIsShooting())
        {
            weapon.StopShooting();
        }
    }
}