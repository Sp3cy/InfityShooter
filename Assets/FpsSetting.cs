using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FpsSetting : MonoBehaviour
{
    // Start is called before the first frame update

    private void Awake()
    {

        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;

    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
