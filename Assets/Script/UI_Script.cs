using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// Powerup menu values
[System.Serializable]
public class PowerUpMenu
{
    public GameObject obj_PwMenu;
    public Text[] txt_PowerUp;
    public GameObject[] img_PowerUp;
}

public class UI_Script : MonoBehaviour
{
    [Header("- PowerUp Menu")]
    public PowerUpMenu powerUpMenu;

    [Space(5)]
    [Header("- UI Objects")]
    // public GameObject ammoTxtHolder;
    public GameObject gameOverPanel;
    public Slider expSlider;
    public Text killedEnemyesTXT;
    public Text gameSessionCurrentTimeTXT;
    public Text playerLevelTXT;
    public Text enemySpawnTXT;

    [Space(5)]
    [Header("- Game over Panel")]
    public Text killTXT;
    public Text timeTXT;
    public Text moneyTXT;

    [Space(5)]
    [Header("- Animation")]
    public float animAmmoPow = 1.6f;
    public float animAmmoTime = 1;

    private int tempAmmoCount;
    private int tempKilledEnemyes;
    private Slider hpBar;
    private Slider ammoBar;

    private GameObject gameManager;
    private Powers powersScript;

    private PowersStruct[] actualMenuPowers;

    // Start is called before the first frame update
    void Start()
    {
        // Get gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        // Get power script
        powersScript = gameManager.GetComponent<PowerUpBehaviour>();

        // Powerup menu setup
        actualMenuPowers = new PowersStruct[3];
        powerUpMenu.obj_PwMenu.SetActive(false);

        // Set gameoverpanel
        gameOverPanel.SetActive(false);

        // Hpbar Setup
        hpBar = GameObject.FindGameObjectWithTag("PlayerHpBar").GetComponent<Slider>();
        hpBar.maxValue = GameData.PlayerLife;
        hpBar.minValue = 0;

        // AmmoBar Setup
        // ammoBar = GameObject.FindGameObjectWithTag("PlayerAmmoBar").GetComponent<Slider>();
        //ammoBar.maxValue = GameData.AmmoCount;
        //ammoBar.minValue = 0;

        // AmmoCount setup
        //tempAmmoCount = GameData.AmmoCount;

        // ExpBar Setup
        expSlider.maxValue = GameData.ExpToLevelUp;
        expSlider.minValue = 0;

        playerLevelTXT.text = "Livello " + GameData.ExpLevel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Praticamente inutile, da cambiare
        if (GameData.isCountChanged(GameData.EnemyDead, tempKilledEnemyes))
        {
            tempKilledEnemyes = GameData.EnemyDead;
            killedEnemyesTXT.text = GameData.EnemyDead.ToString();
        }

        // Change sliders value every fixedupdate
        hpBar.value = GameData.PlayerLife;
        expSlider.value = GameData.ActualExp;
        //ammoBar.value = GameData.AmmoCount;
    }
    private void Update()
    {
        gameSessionCurrentTimeTXT.text = GameData.CurrentPlayT.ToString("0") + " s";
    }

    // Genera il menu dei powerup
    public void GeneratePwMenu()
    {
        powersScript.SetupAvaiblePowers();

        actualMenuPowers[0] = powersScript.GetRandPowerUp();
        actualMenuPowers[1] = powersScript.GetRandPowerUp();
        actualMenuPowers[2] = powersScript.GetRandPowerUp();

        for (int i = 0; i < actualMenuPowers.Length; i++)
        {
            // Se il random non ha trovato poteri rimasti -- NON DOVREBBE
            if (actualMenuPowers[i] == null)
            {
                Debug.LogError("PowerUp not found");
                powerUpMenu.txt_PowerUp[i].text = "No Powerup";
            }
            // Se il livello attuale � minore o uguale dei testi esistenti per il powerup
            else if (actualMenuPowers[i].Level < actualMenuPowers[i].descLevel.Length)
            {
                powerUpMenu.txt_PowerUp[i].text = actualMenuPowers[i].descLevel[actualMenuPowers[i].Level];

                powerUpMenu.img_PowerUp[i].GetComponent<SpriteRenderer>().sprite = (actualMenuPowers[i].sprite != null) ?
                     actualMenuPowers[i].sprite
                    : null;
            }
            // Bug handle
            else
            {
                // Debug.LogError("Text not found" + actualMenuPowers[i]);
                powerUpMenu.txt_PowerUp[i].text = "?";
            }
        }

        // Expbar fix
        expSlider.value = GameData.ExpToLevelUp;

        // Mostra il powerup menu
        powerUpMenu.obj_PwMenu.SetActive(true);
    }

    // Serve ai bottoni per dire quale � stato cliccato 
    public void PowerUpSelected(int id)
    {
        if (actualMenuPowers[id] == null)
        {
            powerUpMenu.obj_PwMenu.SetActive(false);
            gameManager.GetComponent<GameSessionManager>().Resume();
            return;
        }

        if (actualMenuPowers[id].Level >= actualMenuPowers[id].maxLevel)
        {
            powerUpMenu.obj_PwMenu.SetActive(false);
            gameManager.GetComponent<GameSessionManager>().Resume();
            return;
        }


        // Se non returna
        // Aumenta di un livello il powerUp
        powersScript.powersData[actualMenuPowers[id].Id].Level++;
        GameData.ExpLevel++;

        // Aggiorna UI
        expSlider.maxValue = GameData.ExpToLevelUp;
        playerLevelTXT.text = "Livello " + GameData.ExpLevel;

        // Chiude il powerup menu e riprende il gioco
        powerUpMenu.obj_PwMenu.SetActive(false);
        gameManager.GetComponent<GameSessionManager>().Resume();
    }

    public void ShowEndGame()
    {
        int moneyEarned = ((int)GameData.EnemyDead / 5) + ((int)GameData.CurrentPlayT / 10);
        gameOverPanel.SetActive(true);
        // Avvia animazioni per ciascun valore
        StartCoroutine(AnimateNumber(killTXT, 0, (int)GameData.EnemyDead, 1f));
        StartCoroutine(AnimateNumber(timeTXT, 0, (int)GameData.CurrentPlayT, 1f));
        StartCoroutine(AnimateNumber(moneyTXT, 0, moneyEarned, 1f));
    }

    // Show the text of important enemy or bosses - In the upper ui
    public IEnumerator ShowEnemyText(string text, float duration)
    {
        enemySpawnTXT.text = text;

        yield return new WaitForSeconds(duration);

        enemySpawnTXT.text = "";
    }

    IEnumerator AnimateNumber(Text txt, int from, int to, float duration)
    {
        float elapsed = 0f;
        while (elapsed < duration)
        {
            int value = Mathf.RoundToInt(Mathf.Lerp(from, to, elapsed / duration));
            txt.text = value.ToString();
            elapsed += Time.deltaTime;
            yield return null;
        }
        txt.text = to.ToString(); // Imposta il valore finale con precisione
    }
}


