using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static int actualEnemy;
    private static int ammoCount;
    private static float playerLife;

    public static int ActualEnemy { get => actualEnemy; set => actualEnemy = value; }
    public static int AmmoCount { get => ammoCount; set => ammoCount = value; }
    public static float PlayerLife { get => playerLife; set => playerLife = value; }

    public static bool isAmmoCountChanged(int tempAmmoCount)
    {
        if (ammoCount != tempAmmoCount) return true;
        return false;
    }
}
