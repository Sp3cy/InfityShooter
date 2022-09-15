using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public float flameThrowerRechargeT = 10f;
    public static float flameThrowerDamage = 3;

    public AudioSource flameThrowerAudiofx;
    public GameObject flameThrowerPrefab;
    public GameObject player;
    public Text btnTxt;

    private bool active = false;
    private bool shooting = false;

    public IEnumerator FlameThrower()
    {
        shooting = true;

        // Change button text
        btnTxt.text = "BLOCCATO";
        btnTxt.color = new Color(0.3f, 0.3f, 0.3f);

        // Instantiate flame
        var flame = Instantiate(flameThrowerPrefab, player.transform.position, player.transform.rotation) as GameObject;
        flame.transform.parent = player.transform;
        flameThrowerAudiofx.Play();

        // Wait for flame time
        yield return new WaitForSeconds(flame.GetComponent<ParticleSystem>().main.duration);

        // Destroy flame and set shooting to false
        Destroy(flame);
        flameThrowerAudiofx.Stop();
        shooting = false;

        // Wait for skill recharge time
        yield return new WaitForSeconds(flameThrowerRechargeT);

        // Change button text
        btnTxt.color = new Color(1, 0, 0);
        btnTxt.text = "ULTRA FIRE";

        active = false;
    }    
    public void FlameThrowerStart()
    {
        if (active == true) return;
        else
        {
            StartCoroutine(FlameThrower());
            active = true;
        }
    }

    public bool IsSkillShooting()
    {
        return shooting;
    }
}
