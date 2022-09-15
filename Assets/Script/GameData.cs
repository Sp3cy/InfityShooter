using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Enemy
    private static int enemyDead;
    private static List<int> actualEnemy = new List<int>();
    private static List<int> maxEnemy = new List<int>();
    private static GameObject targetEnemy;

    // Weapon
    private static int ammoCount;
    private static int currentWeaponIndex = 0;

    // Player
    private static float playerLife;

    // Time
    private static float currentPlayT;

    // Exp
    private static float actualExp;
    private static float expToLevelUp;
    private static int expLevel;

    public static int EnemyDead { get => enemyDead; set => enemyDead = value; }
    public static int AmmoCount { get => ammoCount; set => ammoCount = value; }
    public static float PlayerLife { get => playerLife; set => playerLife = value; }
    public static float CurrentPlayT { get => currentPlayT; set => currentPlayT = value; }
    public static float ActualExp { get => actualExp; set => actualExp = value; }
    public static float ExpToLevelUp { get => expToLevelUp; set => expToLevelUp = value; }
    public static int ExpLevel { get => expLevel; set => expLevel = value; }
    public static int CurrentWeaponIndex { get => currentWeaponIndex; set => currentWeaponIndex = value; }
    public static GameObject TargetEnemy { get => targetEnemy; set => targetEnemy = value; }
    public static List<int> ActualEnemy { get => actualEnemy; set => actualEnemy = value; }
    public static List<int> MaxEnemy { get => maxEnemy; set => maxEnemy = value; }

    public static bool isCountChanged(int Count,int tempCount)
    {
        if (Count != tempCount) return true;
        return false;
    }
}
