using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public float rangeX;
    public float rangeY;

    public float spawnDelay = 2f;

    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnWithDealy(enemyPrefab, spawnDelay));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator SpawnWithDealy(GameObject enemy, float delay)
    {
        yield return new WaitForSeconds(delay);

        float posX = player.transform.position.x;
        float posY = player.transform.position.y;
        
        Vector3 pos = new Vector2(Random.Range(posX - rangeX, posX + rangeX), Random.Range(posY - rangeY, posY + rangeY));
        GameObject newEnemy = Instantiate(enemy, pos, Quaternion.identity);

        StartCoroutine(SpawnWithDealy(enemy, delay));
    }
}
