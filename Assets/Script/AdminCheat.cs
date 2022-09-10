using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdminCheat : MonoBehaviour
{
    public GameObject gameManager;

    public bool disable = true;
    public int currentWeaponIndex;

    private Weapon weapon;

    private void Awake()
    {
        if (!disable) GameData.CurrentWeaponIndex = currentWeaponIndex;
    }

    // Start is called before the first frame update
    void Start()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon").GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            weapon.SetBtnFirePressed(true);
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            weapon.SetBtnFirePressed(false);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            // Open powerups menu and stop the game
            gameManager.GetComponent<GameSessionManager>().Pause();
            gameManager.GetComponent<GameSessionManager>().ui_Script.GeneratePwMenu();
        }

        if (Input.GetKeyDown(KeyCode.P)) gameManager.GetComponent<GameSessionManager>().Resume();

        if (Input.GetKeyDown(KeyCode.RightArrow)) GameData.CurrentWeaponIndex++;

        if (Input.GetKeyDown(KeyCode.LeftArrow)) GameData.CurrentWeaponIndex--;
    }
}
