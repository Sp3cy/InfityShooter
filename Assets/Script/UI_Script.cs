using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public GameObject ammoTxtHolder;
    public Text ammoTxt;
    public float animAmmoPow = 1f;
    public float animAmmoTime = 0.1f;

    private int tempAmmoCount;
    private Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        tempAmmoCount = GameData.AmmoCount;

        hpBar = GameObject.FindGameObjectWithTag("PlayerHpBar").GetComponent<Slider>();

        hpBar.maxValue = GameData.PlayerLife;
        hpBar.minValue = 0;
        

        ammoTxt.text = GameData.AmmoCount.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (GameData.isAmmoCountChanged(tempAmmoCount))
        {
            tempAmmoCount = GameData.AmmoCount;

            ammoTxt.text = "x" + GameData.AmmoCount.ToString();
            Animazioni.BounceText(animAmmoPow, animAmmoTime, ammoTxtHolder);
        }

        hpBar.value = GameData.PlayerLife;
    }
}
