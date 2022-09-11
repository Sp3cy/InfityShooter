using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public UI_Script ui;

    public static bool shooting;
    public static float flameThrowerDamage = 3;
    public float flameThrowerRechargeT = 10f;

    public AudioSource flameThrowerAudiofx;
    public GameObject flameThrowerPrefab;
    public GameObject player;
    public IEnumerator FlameThrower()
    {
        ui.buttonFire.text = "BLOCCATO";
        ui.buttonFire.color = new Color(0.3f, 0.3f, 0.3f);
        var flame = Instantiate(flameThrowerPrefab, player.transform.position, player.transform.rotation) as GameObject;
        flame.transform.parent = player.transform;
        flameThrowerAudiofx.Play();
        yield return new WaitForSeconds(flame.GetComponent<ParticleSystem>().main.duration);
        Destroy(flame);
        flameThrowerAudiofx.Stop();
        yield return new WaitForSeconds(flameThrowerRechargeT);
        ui.buttonFire.color = new Color(1, 0, 0);
        ui.buttonFire.text = "ULTRA FIRE";
        shooting = false;
    }    
    public void FlameThrowerStart()
    {
        if (shooting == true)
            return;
       
        else
        {
            StartCoroutine(FlameThrower());
            shooting = true;
        }
    }

}
