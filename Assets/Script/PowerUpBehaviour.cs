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
                grenadeRechargeT = 4;
                StartCoroutine(Grenade());
                break;

            case 2:
                grenadeRechargeT = 4;
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                break;

            case 3:
                grenadeRechargeT = 3;
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                break;

            case 4:
                grenadeRechargeT = 3;
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                StartCoroutine(Grenade());
                break;

            case 5:
                grenadeRechargeT = 2;
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
                boltRechargeT = 5;
                StartCoroutine(Bolts());
                break;

            case 2:
                boltRechargeT = 3;
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                break;

            case 3:
                boltRechargeT = 2;
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                break;

            case 4:
                boltRechargeT = 1;
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                StartCoroutine(Bolts());
                break;

            case 5:
                boltRechargeT = 1;
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
        yield return new WaitUntil(() => kunaiPowerUp.Level > 0);

        switch (kunaiPowerUp.Level)
        {
            default:
                Debug.LogError("PowerUp fail");
                break;

            case 1:
                kunayRechargeT = 3;
                kunaiDamage = 20;
                StartCoroutine(Kunai());
                

                break;

            case 2:
                kunayRechargeT = 3;
                kunaiDamage = 20;
                StartCoroutine(Kunai());
                StartCoroutine(Kunai());


                break;

            case 3:
                kunayRechargeT = 3;
                kunaiDamage = 30;
                StartCoroutine(Kunai());
                StartCoroutine(Kunai());
                StartCoroutine(Kunai());


                break;

            case 4:
                kunaiDamage = 40;
                kunayRechargeT = 2;
                StartCoroutine(Kunai());
                StartCoroutine(Kunai());
                StartCoroutine(Kunai());
                StartCoroutine(Kunai());
      

                break;

            case 5:
                kunaiDamage = 50;
                kunayRechargeT = 1;
                StartCoroutine(Kunai());
                StartCoroutine(Kunai());
                StartCoroutine(Kunai());
                StartCoroutine(Kunai());
                StartCoroutine(Kunai());

                break;
        }
                yield return new WaitForSeconds(kunayRechargeT);
                yield return KunaiPowerUp();
    }
    
}
