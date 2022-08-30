using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static int actualEnemy;
    private static int ammoCount;

    public static int ActualEnemy { get => actualEnemy; set => actualEnemy = value; }
    public static int AmmoCount { get => ammoCount; set => ammoCount = value; }
}
