using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public GameObject World;
    // Start is called before the first frame update
    void Start()
    {
        Animazioni.LoopScaleText(1.07f, 1.5f, World);
    }

    // Update is called once per frame
    void Update()
    {
       
    }
}
