using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [field:SerializeField] public float HP_BASE { get; private set; }
    private float _hpActual;

    public UnityEvent<float> OnCure;
    public UnityEvent<float> OnDamage;

    private void OnEnable()
    {
        OnCure.AddListener(HpAdd);
        OnDamage.AddListener(HpRemove);
    }

    private void OnDisable()
    {
        OnCure.RemoveListener(HpAdd);
        OnDamage.RemoveListener(HpRemove);
    }

    private void Awake()
    {
        if (!(HP_BASE > 0))
        {
            Debug.LogError("ERROR HEALTH_SYSTEM: " + this.gameObject + " - HpBase is not valid, passing base value of 100!");
        }

        _hpActual = (HP_BASE > 0) ? HP_BASE : 100f;
    }

    private void HpAdd(float amount)
    {

    }

    private void HpRemove(float amount)
    {
        _hpActual -= amount;

        if (!(_hpActual > 0))
        {
            
        }
    }
}
