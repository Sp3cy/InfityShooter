using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Linq;
using UnityEngine.SceneManagement;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("- Player Stats")]
    public float life = 100f;

    [Header("- Objects")]
    public Rigidbody2D player;

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

    private Joystick joystick;
    private Weapon selectedWeapon;

    private void Awake()
    {
        GameData.PlayerLife = life;
    }

    // Start is called before the first frame update
    void Start()
    {
        selectedWeapon = GameObject.FindGameObjectWithTag("WeaponHolder").transform.GetChild(GameData.CurrentWeaponIndex)
            .GetComponent<Weapon>();

        joystick = GameObject.FindGameObjectWithTag("Joystick").GetComponent<Joystick>();
    }

    private void Update()
    {
        // Se il player muore
        if (GameData.PlayerLife <= 0)
        {
            Dead();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector2 lookDir = new Vector2(0,0);
        GameData.TargetEnemy = GameMethods.GetClosestEnemyByLife(maxEnemyDistance, maxEnemy);

        // Se non esistono enemy vicine smette di sparare -- NO ME GUSTA SHOOTING IN UPDATE
        if (GameData.TargetEnemy == null) selectedWeapon.StopShooting();
        else selectedWeapon.StartShooting();

        // If isShooting
        if (selectedWeapon.IsShooting())
        {
            // Enemy too close bug handler
            if (Vector2.Distance(player.transform.position, GameData.TargetEnemy.transform.position) < enemyTooClose)
                lookDir = GameData.TargetEnemy.transform.position - player.transform.position;
            else
                lookDir = GameData.TargetEnemy.transform.position - selectedWeapon.transform.position;
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
        ScenaManagement.CaricaScena("MainMenu");
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

    

}


