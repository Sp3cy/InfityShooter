using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static int mapZone;

    public static int MapZone { get => mapZone; set => mapZone = value; }
}
