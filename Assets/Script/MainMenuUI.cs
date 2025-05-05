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

    public MenuUpgrades[] menuUpgrades;

    public Button hpUpgradeButton;

    public Text coinTxt;

    private RectTransform contentRect;

    void Start()
    {
        Time.timeScale = 1f;

        Animazioni.LoopScaleText(1.07f, 1.5f, World);
        homeScreen.SetActive(true);
        pwrUpScreen.SetActive(false);
        //refreshMaxHpValue();
        //refreshMoneyValueForHP();

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
}

