using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBuff : MonoBehaviour
{
    [SerializeField] private IHealthObject healthObject;

    public float HealhBuff;

    private void OnEnable()
    {
        healthObject.MaxHealth += HealhBuff;
    }

    private void OnDisable()
    {
        healthObject.MaxHealth -= HealhBuff;
    }
}
