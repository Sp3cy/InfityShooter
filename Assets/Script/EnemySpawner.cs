using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float minRangeX;
    public float minRangeY;

    public float maxRangeX;
    public float maxRangeY;

    public float spawnDelay = 2f;

    private Vector2 playerPos;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(EntitySpawner(enemyPrefab, spawnDelay));

        playerPos = player.transform.position;
        float angle = Mathf.Atan2(playerPos.y, playerPos.x) * Mathf.Rad2Deg - 90f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator EntitySpawner(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Posizione player attuale
        float posX = player.transform.position.x;
        float posY = player.transform.position.y;

        // Random tra distanza minima dal player e massima
        float enemyPosX = Random.Range(minRangeX, maxRangeX);
        float enemyPosY = Random.Range(minRangeY, maxRangeY);

        // Random.Range(0, 2) * 2 - 1 == -1 or 1
        Vector3 pos = new Vector2(posX + (Random.Range(0, 2) * 2 - 1) * enemyPosX, posY + (Random.Range(0, 2) * 2 - 1)*enemyPosY);
        
        // Non so se serva più di tanto creare un gameobject
        GameObject newEnemy = Instantiate(enemy, pos, Quaternion.identity);

        // Restart this coroutine
        StartCoroutine(EntitySpawner(enemy, delay));
    }
}
