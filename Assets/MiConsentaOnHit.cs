using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiConsentaOnHit : MonoBehaviour
{
    public AudioSource miConsentaMp3;
    public GameObject Berlusca;
    public GameObject italy;

    public PolygonCollider2D lolCollider;
    public BoxCollider2D lolColluder;

    // Start is called before the first frame update
    void Start()
    {
      //  lolCollider = Berlusca.GetComponent<PolygonCollider2D>();
       // lolColluder = italy.GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (miConsentaMp3.isPlaying)
        {
            return;
        }
        if(lolCollider.IsTouching(lolColluder))
        {
            miConsentaMp3.Play();    
        }
    }
}
