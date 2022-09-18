using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prop_Hp : MonoBehaviour
{
    public float heal = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // do Animazione
            collision.gameObject.GetComponent<PlayerBehaviour>().Cure(heal, 0f);
            GameData.ActualProps--;
            Destroy(gameObject);
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeathZone")
        {
            GameData.ActualProps--;
            Destroy(gameObject);
        }
    }
}
