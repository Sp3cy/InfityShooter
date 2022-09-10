using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMethods : MonoBehaviour
{
    private static Vector2 minPos;
    private static Vector2 maxPos;

    public static Vector2 MaxPos { get => maxPos; set => maxPos = value; }
    public static Vector2 MinPos { get => minPos; set => minPos = value; }

    public static Vector3 RespawnEnemy(GameObject player)
    {
        // Posizione player attuale
        float posX = player.transform.position.x;
        float posY = player.transform.position.y;

        // Random tra distanza minima dal player e massima
        float enemyPosX = Random.Range(minPos.x, maxPos.x);
        float enemyPosY = Random.Range(minPos.y, maxPos.y);

        // Random.Range(0, 2) * 2 - 1 == -1 or 1
        Vector3 pos = new Vector2(posX + (Random.Range(0, 2) * 2 - 1) * enemyPosX, posY + (Random.Range(0, 2) * 2 - 1) * enemyPosY);

        return pos;
    }
}
