using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJoystick : MonoBehaviour
{
    public float NormalSpeed;
    public float ShootingSpeed;
    public Rigidbody2D rb;

    private WeaponManager weaponManager;
    private Skill skillManager;
    private FloatingJoystick variableJoystick;

    private void Start()
    {
        weaponManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<WeaponManager>();
        skillManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Skill>();
        variableJoystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<FloatingJoystick>();
    }

    public void FixedUpdate()
    {
        // If not shooting going slower
        if (!weaponManager.IsWeaponShooting() && !skillManager.IsSkillShooting())
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
