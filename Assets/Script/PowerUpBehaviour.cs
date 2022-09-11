using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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
                grenadeAmount = 1;
                break;

            case 2:
                grenadeRechargeT = 4;
                grenadeAmount = 2;
                break;

            case 3:
                grenadeRechargeT = 3;
                grenadeAmount = 3;
                break;

            case 4:
                grenadeRechargeT = 3;
                grenadeAmount = 4;
                break;

            case 5:
                grenadeRechargeT = 2;
                grenadeAmount = 5;
                break;
        }

        for (int i = 0; i<grenadeAmount; i++)
        {
            StartCoroutine(Grenade());
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
                boltAmount = 1;
                break;

            case 2:
                boltRechargeT = 3;
                boltAmount = 2;
                break;

            case 3:
                boltRechargeT = 2;
                boltAmount = 3;
                break;

            case 4:
                boltRechargeT = 1;
                boltAmount = 4;
                break;

            case 5:
                boltRechargeT = 1;
                boltAmount = 5;
                break;
        }

        for (int i = 0; i < boltAmount; i++)
        {
            var enemyPos = GetRandEnemy(boltMaxRange);
            if (enemyPos == null) break;

            StartCoroutine(Bolts(enemyPos));
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
                kunaiAmount = 1;
                break;

            case 2:
                kunayRechargeT = 3;
                kunaiDamage = 20;
                kunaiAmount = 2;
                break;

            case 3:
                kunayRechargeT = 3;
                kunaiDamage = 30;
                kunaiAmount = 3;
                break;

            case 4:
                kunaiDamage = 40;
                kunayRechargeT = 2;
                kunaiAmount = 4;
                break;

            case 5:
                kunaiDamage = 50;
                kunayRechargeT = 1;
                kunaiAmount = 5;
                break;
        }

        for (int i = 0; i < kunaiAmount; i++)
        {
            StartCoroutine(Kunai());
        }

        yield return new WaitForSeconds(kunayRechargeT);
        yield return KunaiPowerUp();
    }


    private Transform GetRandEnemy(float maxRange)
    {
        // Array di Enemy attuali
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");

        if (gos.Length == 0) return null;

        var nearest = gos
          .Where(t => Vector3.Distance(player.transform.position, t.transform.position) < maxRange)
          .ToArray();

        if (nearest.Length == 0) return null;

        Transform rand = nearest[Random.Range(0, nearest.Length)].transform;
        return rand;
    }
}
