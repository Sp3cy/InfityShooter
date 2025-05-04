using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject World;
    public GameObject homeScreen;
    public GameObject pwrUpScreen;
    public GameObject scrollViewContent; // Questo è il contenuto della ScrollView

    public Button hpUpgradeButton;

    public Text maxHpTxt;
    public Text coinTxt;
    public Text hpUpgradeCostTxt;

    private RectTransform contentRect;

    void Start()
    {
        Time.timeScale = 1f;

        Animazioni.LoopScaleText(1.07f, 1.5f, World);
        homeScreen.SetActive(true);
        pwrUpScreen.SetActive(false);
        refreshMaxHpValue();
        refreshMoneyValueForHP();

        // Salvo il RectTransform
        contentRect = scrollViewContent.GetComponent<RectTransform>();
    }

    public void homeButton()
    {
        homeScreen.SetActive(true);
        pwrUpScreen.SetActive(false);
        ResetScrollPosition();
    }

    public void pwrupButton()
    {
        pwrUpScreen.SetActive(true);
        homeScreen.SetActive(false);
        ResetScrollPosition();
    }

    private void ResetScrollPosition()
    {
        if (contentRect != null)
        {
            contentRect.anchoredPosition = new Vector2(0, 0); // Resetta la posizione in alto
        }
    }

    public void refreshMaxHpValue()
    {
        maxHpTxt.text = PlayerPrefs.GetInt("PlayerMaxHP", 50).ToString() + "HP";
    }

    public void refreshMoneyValueForHP()
    {
        int money = PlayerPrefs.GetInt("PlayerMoney", 100);
        int upgradeCost = PlayerPrefs.GetInt("hpUpgradeCost", 50);

        coinTxt.text = money.ToString();
        hpUpgradeCostTxt.text = upgradeCost.ToString();

        if (money >= upgradeCost)
        {
            hpUpgradeButton.interactable = true;
        }
        else if (money < upgradeCost)
        {
            hpUpgradeButton.interactable = false;
        }
    }
}

