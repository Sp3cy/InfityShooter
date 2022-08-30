using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Script : MonoBehaviour
{
    private Slider hpBar;


    // Start is called before the first frame update
    void Start()
    {
        hpBar = GameObject.FindGameObjectWithTag("PlayerHpBar").GetComponent<Slider>();



        hpBar.maxValue = GameData.PlayerLife;
        hpBar.minValue = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hpBar.value = GameData.PlayerLife;
    }
}
