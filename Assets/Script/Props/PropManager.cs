using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropManager : MonoBehaviour
{
    public PropStats[] props;

    public int maxProps = 5;
    public float spawnT = 10f;

    private float keepSpawnT;

    private GameObject player;

    private void Awake()
    {
        GameData.MaxProps = maxProps;
        GameData.ActualProps = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        keepSpawnT = spawnT;

        // Get Player
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void FixedUpdate()
    {
        if (GameData.ActualProps < GameData.MaxProps & GameData.CurrentPlayT > spawnT)
        {
            // Aggiungere probabilita
            Vector3 pos = GameMethods.RespawnEnemy();

            Instantiate(props[Random.Range(0, props.Length)].propPrefab, pos, Quaternion.identity);

            GameData.ActualProps++;

            spawnT = GameData.CurrentPlayT + keepSpawnT;
        }
    }
}

[System.Serializable]
public class PropStats
{
    public GameObject propPrefab;

    public float probability;
}

