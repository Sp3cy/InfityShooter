using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public int currentWeaponIndex = 0;
    private int totalWeapons;

    public List<GameObject> guns;
    public GameObject weaponHolder;
    public GameObject currentGun;


    // Start is called before the first frame update
    void Start()
    {
        totalWeapons = weaponHolder.transform.childCount;

        for (int i=0; i<totalWeapons; i++)
        {
            guns.Add(weaponHolder.transform.GetChild(i).gameObject);
            guns[i].SetActive(false);
        }

        currentWeaponIndex = GameData.CurrentWeaponIndex;
        if (currentWeaponIndex > guns.Count) currentWeaponIndex = 0;

        guns[currentWeaponIndex].SetActive(true);
    }
}
