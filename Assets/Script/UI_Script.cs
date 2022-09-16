using UnityEngine;
using UnityEngine.UI;

// Powerup menu values
[System.Serializable]
public class PowerUpMenu
{
    public GameObject obj_PwMenu;
    public Text[] txt_PowerUp;
}

public class UI_Script : MonoBehaviour
{
    [Header("- Game Manager Object")]
    public GameObject gameManager;

    [Space(2)]
    public PowerUpMenu powerUpMenu;
    public Powers powersScript;
    public GameObject ammoTxtHolder;
    public Slider expSlider;
    public Text killedEnemyesTXT;
    public Text killedEnemyesTXT2;
    public Text gameSessionCurrentTimeTXT;
    public Text gameSessionCurrentTimeTXT2;
    public Text playerLevelTXT;
    public float animAmmoPow = 1.6f;
    public float animAmmoTime = 1;

    private int tempAmmoCount;
    private int tempKilledEnemyes;
    private Slider hpBar;
    private Slider ammoBar;

    private PowersStruct[] actualMenuPowers;

    // Start is called before the first frame update
    void Start()
    {
        // Powerup menu setup
        actualMenuPowers = new PowersStruct[3];
        powerUpMenu.obj_PwMenu.SetActive(false);

        // Hpbar Setup
        hpBar = GameObject.FindGameObjectWithTag("PlayerHpBar").GetComponent<Slider>();
        hpBar.maxValue = GameData.PlayerLife;
        hpBar.minValue = 0;

        // AmmoBar Setup
        ammoBar = GameObject.FindGameObjectWithTag("PlayerAmmoBar").GetComponent<Slider>();
        ammoBar.maxValue = GameData.AmmoCount;
        ammoBar.minValue = 0;

        // AmmoCount setup
        tempAmmoCount = GameData.AmmoCount;

        // ExpBar Setup
        expSlider.maxValue = GameData.ExpToLevelUp;
        expSlider.minValue = 0;

        playerLevelTXT.text = "Livello " + GameData.ExpLevel;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Praticamente inutile, da cambiare
        if (GameData.isCountChanged(GameData.EnemyDead,tempKilledEnemyes))
        {
            tempKilledEnemyes = GameData.EnemyDead;
            killedEnemyesTXT.text =  GameData.EnemyDead.ToString();
            killedEnemyesTXT2.text = "Killed Enemyes: " + GameData.EnemyDead.ToString();
        }

        // Change sliders value every fixedupdate
        hpBar.value = GameData.PlayerLife;
        expSlider.value = GameData.ActualExp;
        ammoBar.value = GameData.AmmoCount;
    }
    private void Update()
    {
        gameSessionCurrentTimeTXT.text = GameData.CurrentPlayT.ToString("0") + " s";
        gameSessionCurrentTimeTXT2.text = "Time survived: " + GameData.CurrentPlayT.ToString("0") + " s";
    }

    // Genera il menu dei powerup
    public void GeneratePwMenu()
    {
        powersScript.SetupAvaiblePowers();

        actualMenuPowers[0] = powersScript.GetRandPowerUp();
        actualMenuPowers[1] = powersScript.GetRandPowerUp();
        actualMenuPowers[2] = powersScript.GetRandPowerUp();

        for (int i=0; i<actualMenuPowers.Length; i++)
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
            }
            // Bug handle
            else
            {
                Debug.LogError("Text not found");
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

}


