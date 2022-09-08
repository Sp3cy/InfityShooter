using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : Powers
{
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        // Momentaneo
        StartCoroutine(GrenadePowerUp());
        StartCoroutine(BoltPowerUp());
    }  

    public IEnumerator GrenadePowerUp()
    {
        yield return new WaitUntil(() => powers[0].level > 0);

        switch (powers[0].level)
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
        yield return new WaitUntil(() => powers[1].level > 0);

        switch (powers[1].level)
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
