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
        int maxHp = PlayerPrefs.GetInt("PlayerMaxHP", 50);
        //maxHpTxt.text = maxHp.ToString();
        StartCoroutine(AnimateNumber(maxHpTxt, (maxHp-10), maxHp, 1f));
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

    // Coroutine per animare il numero crescente
    IEnumerator AnimateNumber(Text txt, int from, int to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            int value = Mathf.RoundToInt(Mathf.Lerp(from, to, elapsed / duration));
            txt.text = value.ToString() + " HP"; // Aggiungi "HP" al valore
            elapsed += Time.deltaTime;
            yield return null;
        }
        txt.text = to.ToString() + " HP"; // Assicurati che il numero finale sia quello giusto
    }
}

