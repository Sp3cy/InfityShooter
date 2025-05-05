using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUpgrades : MonoBehaviour
{
    private int hpUpgradeCost;

    private int maxHp;
    private int hpUpgradeValue = 10;

    private int money;

    private void Awake()
    {
        //resetAll();
        maxHp = PlayerPrefs.GetInt("PlayerMaxHP", 50);
        money = PlayerPrefs.GetInt("PlayerMoney", 100);
        hpUpgradeCost = PlayerPrefs.GetInt("hpUpgradeCost", 50);
    }

    public void upgradeMaxHp()
    {
        PlayerPrefs.SetInt("PlayerMaxHP", maxHp + hpUpgradeValue);
        PlayerPrefs.Save();
        maxHp += hpUpgradeValue;
    }

    public void upgradeMoney()
    {
        PlayerPrefs.SetInt("PlayerMoney", money-hpUpgradeCost);
        PlayerPrefs.SetInt("hpUpgradeCost", (int)(hpUpgradeCost * 1.20));
        hpUpgradeCost = PlayerPrefs.GetInt("hpUpgradeCost", 50);
        money = PlayerPrefs.GetInt("PlayerMoney", 100);
        PlayerPrefs.Save();
    }

    public void resetAll()
    {
        PlayerPrefs.SetInt("PlayerMoney", 100);
        PlayerPrefs.SetInt("hpUpgradeCost", 50);
        PlayerPrefs.SetInt("PlayerMaxHP", 50);
    }
}
