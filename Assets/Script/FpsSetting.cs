using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsSetting : MonoBehaviour
{
    //Fps Cap

    private void Awake()
    {

        QualitySettings.vSyncCount = 0; //V-Sync Off
        Application.targetFrameRate = 120; //FPS

    }
}
