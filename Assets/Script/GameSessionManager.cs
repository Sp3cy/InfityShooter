using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    private float timeT;

    // Start is called before the first frame update
    void Start()
    {
        timeT = 0;

        GameData.CurrentPlayT = 0;
        GameData.EnemyDead = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GameData.CurrentPlayT += Time.deltaTime;
    }
}
