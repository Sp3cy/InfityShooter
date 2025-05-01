using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("- Object")]
    public GameObject[] enemiesPrefab;
    public SpawnerDetails[] spawnerDetails;

    [Space(2)]
    public GameObject enemySpawnerHolder;

    [Space(5)]
    [Header("- Spawner Related")]
    public Vector2 minPos;
    [Space(5)]
    public Vector2 maxPos;
    [Space(5)]
    public float spawnDelay = 2f;
    public float rotationSpeed = 2f;

    private float hpMultiplier;
    private float atkMultiplier;

    private GameObject player;
    private GameObject gameManager;
    private GameObject enemySpawnObject;

    private UI_Script ui_Script;

    private IEnumerator spawnerCoroutine;

    private void Awake()
    {
        // Set global respawn boundaries
        GameMethods.MinEnemyRespawnPos = minPos;
        GameMethods.MaxEnemyRespawnPos = maxPos;

        // Get and Set spawnerobject
        enemySpawnObject = enemySpawnerHolder.transform.GetChild(0).gameObject;
        GameMethods.Spawner = enemySpawnObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Setup variables
        Setup();

        // Get game manager and scripts
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        ui_Script = gameManager.GetComponent<UI_Script>();

        // Get player
          player = GameObject.FindGameObjectWithTag("Player");


        // Start the changes coroutine
        spawnerCoroutine = EntitySpawner(0, 0, 0);
        StartCoroutine(CheckSpawnerDetails());
    }

    private void FixedUpdate()
    {
        enemySpawnerHolder.transform.Rotate(new Vector3(0f, 0f, rotationSpeed));
        enemySpawnerHolder.transform.position = player.transform.position;
    }

    private void Setup()
    {
        for (int i=0; i<enemiesPrefab.Length; i++)
        {
            GameData.ActualEnemy.Add(0);
            GameData.MaxEnemy.Add(0);
        }
    }

    private IEnumerator EntitySpawner(int enemyIndex, int amount, float delay)
    {
        if (amount <= 0) yield return null;

        for (int i=0; i<amount; i++)
        {
            Vector3 pos = GameMethods.RespawnEnemy();

            // Instantiate an enemy and set his index for Dead() method
            GameObject newEnemy = Instantiate(enemiesPrefab[enemyIndex], pos, Quaternion.identity);
            newEnemy.GetComponent<Enemy>().SetIndex(enemyIndex);
            newEnemy.GetComponent<Enemy>().life *= hpMultiplier;
            newEnemy.GetComponent<Enemy>().attack *= atkMultiplier;

            GameData.ActualEnemy[enemyIndex]++;

            yield return new WaitForSeconds(delay);
        }

        yield return null;
    }

    // Da fare in multitreadhing
    private IEnumerator CheckSpawnerDetails()
    {
        foreach (SpawnerDetails sp in spawnerDetails)
        {
            if (sp.enabled & GameData.CurrentPlayT >= sp.startTime)
            {
                // Stop the coroutine
                StopCoroutine(spawnerCoroutine);

                // Change Values
                spawnDelay = (601f - sp.startTime) /sp.enemyAddAmount;
                GameData.MaxEnemy[sp.enemyIndex] += sp.enemyAddAmount;
                sp.enabled = false;
                atkMultiplier = sp.AtkMultiplier;
                hpMultiplier = sp.HpMultiplier;
                Debug.Log(sp.enemyAddAmount);
                Debug.Log(spawnDelay);

                // Change ui text
                if (sp.uiText != "") StartCoroutine(ui_Script.ShowEnemyText(sp.uiText, sp.uiTextDuration));

                // Restart coroutine
                spawnerCoroutine = EntitySpawner(sp.enemyIndex, sp.enemyAddAmount, spawnDelay);
                StartCoroutine(spawnerCoroutine);
            }
        }

        yield return new WaitForFixedUpdate();
        yield return CheckSpawnerDetails();
    }
}

[System.Serializable]
public class SpawnerDetails
{
    public int enemyIndex;
    public int enemyAddAmount;

    public float startTime;
    public float spawnDelay;

    public string uiText;
    // Momentaneo?
    public float uiTextDuration;

    public float AtkMultiplier = 1f;
    public float HpMultiplier = 1f;

    public bool enabled;
}
