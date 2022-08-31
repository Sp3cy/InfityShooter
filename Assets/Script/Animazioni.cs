using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animazioni : MonoBehaviour
{

    public static void BounceText(float potenzaAnimazione, float durataAnimazione, GameObject oggetto) //Animazione bouncing.
    {
        LeanTween.cancel(oggetto);

        oggetto.gameObject.transform.localScale = new Vector3(1, 1, 1);

        LeanTween.scale(oggetto, Vector3.one * potenzaAnimazione, durataAnimazione).setEasePunch();
    }
    public static void LoopScaleText(float potenzaAnimazione, float durataAnimazione, GameObject oggetto)
    {
        LeanTween.scale(oggetto, Vector3.one * potenzaAnimazione, durataAnimazione).setEaseInOutQuad().setLoopPingPong();
    }
}