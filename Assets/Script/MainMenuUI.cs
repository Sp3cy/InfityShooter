using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject Play;
    // Start is called before the first frame update
    void Start()
    {
        Animazioni.LoopScaleText(1.5f, 2f, Play);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
