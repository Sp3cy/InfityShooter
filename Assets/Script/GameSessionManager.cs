using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    public float expToUnlock = 10f;
    public float expToUnlcokMul = 1.1f;

    private GameObject gameManager;

    private UI_Script ui_Script;

    private void Awake()
    {
        GameData.ExpToLevelUp = expToUnlock;
        GameData.ExpLevel = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        Resume();

        GameData.CurrentPlayT = 0;
        GameData.EnemyDead = 0;
        GameData.ActualExp = 0f;

        // Get game manager and scripts
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        ui_Script = gameManager.GetComponent<UI_Script>();
    }

    // Update is called once per frame
    void Update()
    {
        GameData.CurrentPlayT += Time.deltaTime;

        // Se sono stati raggiunti tot exp
        if (GameData.ActualExp >= GameData.ExpToLevelUp)
        {
            // Pause the game
            Pause();

            // Change next exp to level up
            GameData.ExpToLevelUp *= expToUnlcokMul;
            GameData.ActualExp = 0;

            // Open powerups menu
            ui_Script.GeneratePwMenu();
        }
    }

    // Stop everything that works with Time -- EVEN COROUTINE BUT NOT THE EMEMY SPAWNER SCRIPT
    public void Pause()
    {
        Time.timeScale = 0f;
    }

    // Resume normal Time
    public void Resume()
    {
        Time.timeScale = 1f;
    }

}
