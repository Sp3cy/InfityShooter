using UnityEngine;

public class Prop_Hp : MonoBehaviour
{
    public float heal = 10f;

    private bool destroyed = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (destroyed) return;

        if (collision.gameObject.tag == "Player")
        {
            // do Animazione
            collision.gameObject.GetComponent<PlayerBehaviour>().Cure(heal, 0f);

            // Destroy gameobject with bug handler
            GameData.ActualProps--;
            destroyed = true;
            Destroy(gameObject);
        }

        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (destroyed) return;

        if (collision.gameObject.tag == "DeathZone")
        {
            // Destroy gameobject with bug handler
            GameData.ActualProps--;
            destroyed = true;
            Destroy(gameObject);
        }
    }
}
