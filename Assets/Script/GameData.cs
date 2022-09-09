using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    // Enemy
    private static int enemyDead;
    private static int actualEnemy;

    // Weapon
    private static int ammoCount;

    // Player
    private static float playerLife;

    // Time
    private static float currentPlayT;

    // Exp
    private static float actualExp;
    private static float expToLevelUp;
    private static int expLevel;

    public static int EnemyDead { get => enemyDead; set => enemyDead = value; }
    public static int ActualEnemy { get => actualEnemy; set => actualEnemy = value; }
    public static int AmmoCount { get => ammoCount; set => ammoCount = value; }
    public static float PlayerLife { get => playerLife; set => playerLife = value; }
    public static float CurrentPlayT { get => currentPlayT; set => currentPlayT = value; }
    public static float ActualExp { get => actualExp; set => actualExp = value; }
    public static float ExpToLevelUp { get => expToLevelUp; set => expToLevelUp = value; }
    public static int ExpLevel { get => expLevel; set => expLevel = value; }

    public static bool isCountChanged(int Count,int tempCount)
    {
        if (Count != tempCount) return true;
        return false;
    }
}
