using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : Powers
{
    private int grenadePwLevel;
    private int boltPwLevel;

   // public Powers powers;

    private void Start()
    {
        grenadePwLevel = 2;
        boltPwLevel = 0;

        player = GameObject.FindGameObjectWithTag("Player");

        // Momentaneo
        StartCoroutine(GrenadePowerUp());
        StartCoroutine(BoltPowerUp());
    }  

    public IEnumerator GrenadePowerUp()
    {
        yield return new WaitUntil(() => grenadePwLevel > 0);

        switch (grenadePwLevel)
        {
            default:
                Debug.LogError("PowerUp fail");
                break;

            case 1:
                StartCoroutine(Grenade());
                break;

            case 2:
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                break;
        }

        yield return new WaitForSeconds(grenadeRechargeT);
        yield return GrenadePowerUp();
    }

    public IEnumerator BoltPowerUp()
    {
        yield return new WaitUntil(() => boltPwLevel > 0);

        Debug.Log("Yo");
        switch (boltPwLevel)
        {
            default:
                Debug.LogError("PowerUp fail");
                break;

            case 1:
                StartCoroutine(Bolts());
                break;

            case 2:
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                break;
        }

        yield return new WaitForSeconds(boltRechargeT);
        yield return BoltPowerUp();
    }
}
