using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminCheat : MonoBehaviour
{
    public bool disable = true;
    public int currentWeaponIndex;

    private Weapon weapon;
    private GameObject gameManager;

    private void Awake()
    {
        if (!disable) GameData.CurrentWeaponIndex = currentWeaponIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        weapon = GameObject.FindGameObjectWithTag("WeaponHolder").transform.GetChild(GameData.CurrentWeaponIndex)
            .GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Open powerups menu and stop the game
            gameManager.GetComponent<GameSessionManager>().Pause();
            gameManager.GetComponent<GameSessionManager>().ui_Script.GeneratePwMenu();
        }

        if (Input.GetKeyDown(KeyCode.P)) gameManager.GetComponent<GameSessionManager>().Resume();

        if (Input.GetKeyDown(KeyCode.RightArrow)) Time.timeScale += 0.1f;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) Time.timeScale -= 0.1f;
    }
}
