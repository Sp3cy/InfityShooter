using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("- Object")]
    public GameObject enemyPrefab;

    public int maxEnemy = 20;
    public int enemyIncrease = 10;
    public float enemyIncreaseT = 1f;
    private float tempEnemyIncreaseT;

    [Header("- Spawner Related")]
    public Vector2 minPos;
    [Space(5)]
    public Vector2 maxPos;
    [Space(5)]
    public float spawnDelay = 2f;

    private Vector2 playerPos;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // Set global respawn boundaries
        GameMethods.MinPos = minPos;
        GameMethods.MaxPos = maxPos;

        tempEnemyIncreaseT = enemyIncreaseT;
        GameData.ActualEnemy = 0;

        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(EntitySpawner(enemyPrefab, spawnDelay));

        playerPos = player.transform.position;
        float angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg - 90f;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameData.CurrentPlayT > tempEnemyIncreaseT)
        {
            maxEnemy += enemyIncrease;
            tempEnemyIncreaseT = GameData.CurrentPlayT + enemyIncreaseT;
        }
    }

    private IEnumerator EntitySpawner(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);
        yield return new WaitUntil(() => GameData.ActualEnemy < maxEnemy);

        Vector3 pos = GameMethods.RespawnEnemy(player);

        // Non so se serva più di tanto creare un gameobject
        GameObject newEnemy = Instantiate(enemy, pos, Quaternion.identity);
        GameData.ActualEnemy++;

        // Restart this coroutine
        StartCoroutine(EntitySpawner(enemy, delay));
    }
}
