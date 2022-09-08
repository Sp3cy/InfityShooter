using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
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
        gameSessionCurrentTimeTXT.text = GameData.CurrentPlayT.ToString("0") + "s";
    }
}
