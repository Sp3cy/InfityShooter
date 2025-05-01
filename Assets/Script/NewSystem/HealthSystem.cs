using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [field:SerializeField] public float HpBase { get; private set; }
    private float HpActual;

    private void Awake()
    {
        if (!(HpBase > 0))
        {
            Debug.LogError("ERROR HEALTH_SYSTEM: " + this.gameObject + " - HpBase is not valid, passing base value of 100!");
        }

        HpActual = (HpBase > 0) ? HpBase : 100f;
    }

    private void RemoveHealth(float amount)
    {

    }
}
