using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    public Text ammoTxt;
    private Slider hpBar;

    // Start is called before the first frame update
    void Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("PlayerHpBar").GetComponent<Slider>();

        hpBar.maxValue = GameData.PlayerLife;
        hpBar.minValue = 0;

        ammoTxt.text = GameData.AmmoCount.ToString();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        ammoTxt.text = GameData.AmmoCount.ToString();

        hpBar.value = GameData.PlayerLife;
    }
}
