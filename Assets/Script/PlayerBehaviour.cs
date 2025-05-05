using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("- Player Stats")]
    private float life;

    [Space(1)]
    [Header("- Rotation Time")]
    public float lerpT = 0.5f;

    [Space(1)]
    [Header("- Shot Behaviour")]
    public float maxEnemyDistance = 20f;
    public int maxEnemy = 3;
    public float knockbackT = 0.2f;

    [Space(5)]
    [Header("- Fix")]
    public float enemyTooClose = 1f;

    private Rigidbody2D player;
    private Joystick joystick;

    private GameObject gameManager;
    private WeaponManager weaponManager;
    private Skill skillScript;
    private bool isPlayerDead; // Serve per non far andare in loop la funzione che sta nell'update

    private void Awake()
    {
        life = PlayerPrefs.GetInt("PlayerMaxHP", 50);
        GameData.PlayerLife = life;
        isPlayerDead = false;
}

    // Start is called before the first frame update
    void Start()
    {
        // Get gamemanager
        gameManager = GameObject.FindGameObjectWithTag("GameManager");

        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
        player = gameObject.GetComponent<Rigidbody2D>();

        weaponManager = GameObject.FindGameObjectWithTag("WeaponHolder").GetComponent<WeaponManager>();

        skillScript = gameManager.GetComponent<Skill>();
    }

    private void Update()
    {
        // Se il player muore
        if (GameData.PlayerLife <= 0 && isPlayerDead == false)
        {
            Dead();
            isPlayerDead=true;
            setMoney();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lookDir = new Vector2(0,0);
        GameData.TargetEnemy = GameMethods.GetClosestEnemyByLife(maxEnemyDistance, maxEnemy);


        // If weapon isShooting or skill isShooting
        if ((weaponManager.IsWeaponShooting() || skillScript.IsSkillShooting()) && GameData.TargetEnemy != null)
        {
            lookDir = GameData.TargetEnemy.transform.position - weaponManager.CurrentWeaponObject.transform.position;
        }
        else lookDir = new Vector2(joystick.Horizontal, joystick.Vertical);

        // If joystick is moving or an enemy is found
        if (!lookDir.Equals(new Vector2(0,0)))
        {
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;

            player.rotation = Mathf.LerpAngle(player.rotation, angle, lerpT);
        }
    }

    // Quando player morto
    public void Dead()
    {
        gameManager.GetComponent<GameSessionManager>().StopGame();
    }

    // When player hitted
    public void HittedByEnemy(float damage, float knockback, Transform enemy)
    {
        GameData.PlayerLife -= damage;

        StartCoroutine(DoKnockback(knockbackT, knockback, enemy));
    }

    private IEnumerator DoKnockback(float knockbackT, float knockback, Transform enemy)
    {
        player.gameObject.GetComponent<Rigidbody2D>().AddForce((player.transform.position - enemy.transform.position).normalized * knockback, ForceMode2D.Impulse);
        yield return new WaitForSeconds(knockbackT);
        player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
    }

    public void Cure(float amount, float time)
    {
        if (amount < 0) return;
        if (GameData.PlayerLife + amount > life) GameData.PlayerLife = life;

        // fare la cosa col tempo

        GameData.PlayerLife += amount;
    }

    public void setMoney()
    {
        int moneyEarned = ((int)GameData.EnemyDead / 5) + ((int)GameData.CurrentPlayT / 10);
        Debug.Log(moneyEarned);
        int money = PlayerPrefs.GetInt("PlayerMoney", 100);
        PlayerPrefs.SetInt("PlayerMoney", money + moneyEarned);
        PlayerPrefs.Save();
    }

}


