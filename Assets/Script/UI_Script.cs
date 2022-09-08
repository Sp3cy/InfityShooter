using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class UI_Script : MonoBehaviour
{
    public PowerUpMenu powerUpMenu;
    public Powers powersScript;
    public GameObject ammoTxtHolder;
    public Slider expSlider;
    public Text killedEnemyesTXT;
    public Text gameSessionCurrentTimeTXT;
    public Text ammoTxt;
    public float animAmmoPow = 1.6f;
    public float animAmmoTime = 1;


    private int tempAmmoCount;
    private int tempKilledEnemyes;
    private Slider hpBar;
    private Slider ammoBar;

    private PowersStruct[] actualMenuPowers;

    public Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        powerUpMenu.obj_PwMenu.SetActive(false);

        tempAmmoCount = GameData.AmmoCount;

        hpBar = GameObject.FindGameObjectWithTag("PlayerHpBar").GetComponent<Slider>();
        ammoBar = GameObject.FindGameObjectWithTag("PlayerAmmoBar").GetComponent<Slider>();

        ammoBar.maxValue = weapon.GetAmmoMax();
        ammoBar.minValue = 0;

        hpBar.maxValue = GameData.PlayerLife;
        hpBar.minValue = 0;

        expSlider.maxValue = 200;
        expSlider.minValue = 0;

        actualMenuPowers = new PowersStruct[3];
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (GameData.isCountChanged(GameData.EnemyDead,tempKilledEnemyes))
        {
            tempKilledEnemyes = GameData.EnemyDead;
            killedEnemyesTXT.text = GameData.EnemyDead.ToString();
        }

        hpBar.value = GameData.PlayerLife;
        expSlider.value = GameData.ActualExp;
        
    }
    private void Update()
    {
        gameSessionCurrentTimeTXT.text = GameData.CurrentPlayT.ToString("0") + " s";
    }

    public void GeneratePwMenu()
    {
        // Cringe -- Da modificare possibilmente
        actualMenuPowers[0] = powersScript.GetRandPowerUp();
        
        if (actualMenuPowers[0].level < actualMenuPowers[0].descLevel.Length)
            powerUpMenu.txt_PowerUp0.text = actualMenuPowers[0].descLevel[actualMenuPowers[0].level];

        actualMenuPowers[1] = powersScript.GetRandPowerUp();

        if (actualMenuPowers[1].level < actualMenuPowers[1].descLevel.Length)
            powerUpMenu.txt_PowerUp1.text = actualMenuPowers[1].descLevel[actualMenuPowers[1].level];

        actualMenuPowers[2] = powersScript.GetRandPowerUp();

        if (actualMenuPowers[2].level < actualMenuPowers[2].descLevel.Length)
            powerUpMenu.txt_PowerUp2.text = actualMenuPowers[2].descLevel[actualMenuPowers[2].level];

        powerUpMenu.obj_PwMenu.SetActive(true);
    }

    public void PowerUpSelected(int id)
    {
        if (actualMenuPowers[id].level < actualMenuPowers[id].descLevel.Length )
        {
            powersScript.powers[actualMenuPowers[id].id].level++;
        }

        powerUpMenu.obj_PwMenu.SetActive(false);
        Resume();
    }

    private void Resume()
    {
        Time.timeScale = 1f;
    }

}

[System.Serializable]
public class PowerUpMenu
{
    public GameObject obj_PwMenu;

    public Text txt_PowerUp0;

    public Text txt_PowerUp1;

    public Text txt_PowerUp2;
}
