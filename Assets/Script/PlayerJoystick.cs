using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJoystick : MonoBehaviour
{
    public float NormalSpeed;
    public float ShootingSpeed;
    public Rigidbody2D rb;

    private Weapon weapon;
    private VariableJoystick variableJoystick;

    private void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
        variableJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<VariableJoystick>();
    }

    public void FixedUpdate()
    {
        if (!weapon.IsShooting())
        {
            Vector2 direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;
            rb.AddForce(direction * NormalSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
        }
        else
        {
            Vector2 direction = Vector2.up * variableJoystick.Vertical + Vector2.right * variableJoystick.Horizontal;
            rb.AddForce(direction * ShootingSpeed * Time.fixedDeltaTime, ForceMode2D.Force);
        }
    }
}
