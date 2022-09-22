using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GameMethods : MonoBehaviour
{
    private static Vector2 maxEnemyRespawnPos;
    private static Vector2 minEnemyRespawnPos;

    // Puzza
    private static GameObject spawner;

    public static Vector2 MaxEnemyRespawnPos { get => maxEnemyRespawnPos; set => maxEnemyRespawnPos = value; }
    public static Vector2 MinEnemyRespawnPos { get => minEnemyRespawnPos; set => minEnemyRespawnPos = value; }
    public static GameObject Spawner { get => spawner; set => spawner = value; }

    public static Vector3 RespawnEnemy()
    {
        // Posizione player attuale
        float posX = spawner.transform.position.x;
        float posY = spawner.transform.position.y;

        // Random tra distanza minima dal player e massima
        float enemyPosX = Random.Range(minEnemyRespawnPos.x, maxEnemyRespawnPos.x);
        float enemyPosY = Random.Range(minEnemyRespawnPos.y, maxEnemyRespawnPos.y);

        // Random.Range(0, 2) * 2 - 1 == -1 or 1
        Vector3 pos = new Vector2(posX + (Random.Range(0, 2) * 2 - 1) * enemyPosX, posY + (Random.Range(0, 2) * 2 - 1) * enemyPosY);

        return pos;
    }

    // Find nearest object with Enemy Tag
    public static GameObject GetClosestEnemyByLife(float maxDistance, int maxEnemy)
    {
        // Array di Enemy attuali
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");

        if (gos.Length == 0) return null;

        // Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        var nearest = gos
            .Where(t => Vector3.Distance(player.transform.position, t.transform.position) < maxDistance)
            .OrderBy(t => Vector3.Distance(player.transform.position, t.transform.position))
            .Take(maxEnemy)
            .OrderBy(t => t.GetComponent<Enemy>().life)
            .FirstOrDefault();

        return nearest;
    }

    public static GameObject GetRandomEnemy(float maxDistance)
    {
        // Array di Enemy attuali
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Enemy");

        if (gos.Length == 0) return null;

        // Player
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        var nearest = gos
          .Where(t => Vector3.Distance(player.transform.position, t.transform.position) < maxDistance)
          .ToArray();

        if (nearest.Length == 0) return null;

        GameObject rand = nearest[Random.Range(0, nearest.Length)];
        return rand;
    }
}
