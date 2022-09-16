using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenaManagement : MonoBehaviour
{
public static void CaricaScena(string scena)
    {
        SceneManager.LoadScene(scena);
    }

}
