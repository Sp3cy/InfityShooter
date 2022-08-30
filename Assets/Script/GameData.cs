using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static int actualEnemy;

    public static int ActualEnemy { get => actualEnemy; set => actualEnemy = value; }
}
