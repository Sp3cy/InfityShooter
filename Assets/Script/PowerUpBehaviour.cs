using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : Powers
{
    private void Start()
    {
        // Setup powerup lists
        SetupPowers();
        SetupAvaiblePowers();

        player = GameObject.FindGameObjectWithTag("Player");

        // Momentaneo
        StartCoroutine(GrenadePowerUp());
        StartCoroutine(BoltPowerUp());
        StartCoroutine(KunaiPowerUp());
    }

    public IEnumerator GrenadePowerUp()
    {
        yield return new WaitUntil(() => grenadePowerUp.Level > 0);

        switch (grenadePowerUp.Level)
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

            case 3:
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                break;

            case 4:
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                break;

            case 5:
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                break;
        }

        yield return new WaitForSeconds(grenadeRechargeT);
        yield return GrenadePowerUp();
    }

    public IEnumerator BoltPowerUp()
    {
        yield return new WaitUntil(() => boltPowerUp.Level > 0);

        switch (boltPowerUp.Level)
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

            case 3:
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                break;

            case 4:
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                break;

            case 5:
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                break;
        }

        yield return new WaitForSeconds(boltRechargeT);
        yield return BoltPowerUp();
    }

    public IEnumerator KunaiPowerUp()
    {
        yield return new WaitUntil(() => powers[2].level > 0);

        switch (powers[2].level)
        {
            default:
                Debug.LogError("PowerUp fail");
                break;

            case 1:
                StartCoroutine(Kunai());
                kunayRechargeT = 5f;

                break;

            case 2:
                StartCoroutine(Kunai());
                kunayRechargeT = 3.5f;

                break;

            case 3:
                StartCoroutine(Kunai());
                kunayRechargeT = 2f;

                break;

            case 4:
                StartCoroutine(Kunai());
                kunayRechargeT = 1f;

                break;

            case 5:
                StartCoroutine(Kunai());
                kunayRechargeT = 0.33f;

                break;
        }
                yield return new WaitForSeconds(kunayRechargeT);
                yield return KunaiPowerUp();
    }
    
}
