using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int currentWeaponIndex;
    private int totalWeapons;

    public List<GameObject> guns;
    public GameObject weaponHolder;

    private bool shooting = false;

    private GameObject currentWeaponObject;
    private Weapon currentWeapon;

    private GameObject gameManager;
    private Skill skillManager;

    public GameObject CurrentWeaponObject { get => currentWeaponObject; private set => currentWeaponObject = value;  }


    // Start is called before the first frame update
    void Awake()
    {
        totalWeapons = weaponHolder.transform.childCount;

        for (int i=0; i<totalWeapons; i++)
        {
            guns.Add(weaponHolder.transform.GetChild(i).gameObject);
            guns[i].SetActive(false);
        }

        currentWeaponIndex = GameData.CurrentWeaponIndex;
        if (currentWeaponIndex > guns.Count) currentWeaponIndex = 0;

        CurrentWeaponObject = guns[currentWeaponIndex];
        CurrentWeaponObject.SetActive(true);

        // Get current weapon script
        currentWeapon = CurrentWeaponObject.GetComponent<Weapon>();

    }

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        skillManager = gameManager.GetComponent<Skill>();
    }

    private void FixedUpdate()
    {
        // If there is an enemy
        if (GameData.TargetEnemy != null && !skillManager.IsSkillShooting())
        {
            shooting = true;
            currentWeapon.Shoot();
        }
        else
        {
            shooting = false;
            currentWeapon.ResetFireRate();
        }

        if (GameData.AmmoCount <= 0)
        {
            shooting = false;
            currentWeapon.Reload();
            currentWeapon.ResetFireRate();
        }
    }

    public bool IsWeaponShooting()
    {
        return shooting;
    }
}
