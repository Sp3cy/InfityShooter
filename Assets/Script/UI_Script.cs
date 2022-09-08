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
    public Text killedEnemyesTXT;
    public Text gameSessionCurrentTimeTXT;
    public Text ammoTxt;
    public float animAmmoPow = 1.6f;
    public float animAmmoTime = 1;


    private int tempAmmoCount;
    private int tempKilledEnemyes;
    private Slider hpBar;
    private Slider ammoBar;

    public Weapon weapon;

    // Start is called before the first frame update
    void Start()
    {
        tempAmmoCount = GameData.AmmoCount;

        hpBar = GameObject.FindGameObjectWithTag("PlayerHpBar").GetComponent<Slider>();
        ammoBar = GameObject.FindGameObjectWithTag("PlayerAmmoBar").GetComponent<Slider>();

        ammoBar.maxValue = weapon.GetAmmoMax();
        ammoBar.minValue = 0;

        hpBar.maxValue = GameData.PlayerLife;
        hpBar.minValue = 0;
        
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
        
    }
    private void Update()
    {
        gameSessionCurrentTimeTXT.text = GameData.CurrentPlayT.ToString("0") + " s";
    }

    public void GeneratePwMenu()
    {
        int randomIndex = Random.Range(0, powersScript.powers.Length - 1);

        var pw = powersScript.powers
            .Where(powerUp => powerUp.id == randomIndex)
            .FirstOrDefault();
    }
    
}

public class PowerUpMenu
{
    public GameObject powerUp0;
    public GameObject powerUp1;
    public GameObject powerUp2;
}
