using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminCheat : MonoBehaviour
{
    public bool enableCheat00 = true;
    public int currentWeaponIndex;

    [Space(5)]
    public bool enableFixedUpdate = false;
    public float fixedUpdateT = 1f;

    private float keepFixedUpdateT;

    private Weapon weapon;
    private GameObject gameManager;

    private void Awake()
    {
        if (enableCheat00) GameData.CurrentWeaponIndex = currentWeaponIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        // Get gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        weapon = GameObject.FindGameObjectWithTag("WeaponHolder").transform.GetChild(GameData.CurrentWeaponIndex)
            .GetComponent<Weapon>();

        keepFixedUpdateT = fixedUpdateT;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            // Open powerups menu and stop the game
            gameManager.GetComponent<GameSessionManager>().Pause();
            gameManager.GetComponent<UI_Script>().GeneratePwMenu();
        }

        if (Input.GetKeyDown(KeyCode.P)) gameManager.GetComponent<GameSessionManager>().Resume();

        if (Input.GetKeyDown(KeyCode.RightArrow)) Time.timeScale += 0.1f;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) Time.timeScale -= 0.1f;


        if (Time.time > fixedUpdateT && enableFixedUpdate)
        {
            int totalEnemy = GameData.GetTotalActualEnemy();
            fixedUpdateT = Time.time + keepFixedUpdateT;
        }
    }
}
