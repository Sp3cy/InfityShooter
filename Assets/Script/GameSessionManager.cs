using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSessionManager : MonoBehaviour
{
    public float expToUnlock = 10f;
    public float expToUnlcokMul = 1.1f;

    private string audio_to_stop_tag = "audio_to_stop";

    private GameObject gameManager;

    private UI_Script ui_Script;

    private void Awake()
    {
        GameData.ExpToLevelUp = expToUnlock;
        GameData.ExpLevel = 0;

        GameData.CurrentPlayT = 0;
        GameData.ActualEnemy.Clear();
        GameData.MaxEnemy.Clear();
    }

    // Start is called before the first frame update
    void Start()
    {
        Resume();

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

    public void StopGame()
    {
        Pause();
        StopAllCoroutines();
        
        gameManager.GetComponent<UI_Script>().ShowEndGame();
    }


    private GameObject audioToResume;

    // Stop everything that works with Time -- EVEN COROUTINE BUT NOT THE EMEMY SPAWNER SCRIPT
    public void Pause()
    {
        Time.timeScale = 0f;
        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in allAudioSources)
        {
            if (source.isPlaying && source.TryGetComponent(out CustomTags customTags) && customTags.HasTag(audio_to_stop_tag))
            {
                audioToResume = source.gameObject;
                source.Stop();
                return;
            }
        }
    }

    // Resume normal Time
    public void Resume()
    {
        Time.timeScale = 1f;

        AudioSource[] allAudioSources = FindObjectsOfType<AudioSource>();

        foreach (AudioSource source in allAudioSources)
        {
            if (source.gameObject == audioToResume)
            {
                source.Play();
                return;
            }
        }
    }
}
