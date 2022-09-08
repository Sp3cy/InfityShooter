using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    public float expToUnlock = 10f;
    public float expToUnlcokMul = 1.1f;

    public UI_Script ui_Script;

    // Start is called before the first frame update
    void Start()
    {
        GameData.CurrentPlayT = 0;
        GameData.EnemyDead = 0;
        GameData.ActualExp = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        GameData.CurrentPlayT += Time.deltaTime;

        // Se sono stati raggiunti tot exp
        if (GameData.ActualExp > expToUnlock)
        {
            expToUnlock *= expToUnlcokMul;
            GameData.ActualExp = 0;

            // Open powerups menu and stop the game
            Pause();
            ui_Script.GeneratePwMenu();
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            // Open powerups menu and stop the game
            Pause();
            ui_Script.GeneratePwMenu();
        }

        if (Input.GetKeyDown(KeyCode.P)) Resume();
    }

    private void Pause()
    {
        Time.timeScale = 0f;
    }

    private void Resume()
    {
        Time.timeScale = 1f;
    }
}
