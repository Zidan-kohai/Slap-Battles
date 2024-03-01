using System.Collections.Generic;
using UnityEngine;

public class OneTimeBuffController : MonoBehaviour
{
    public List<GameObject> Buffs;
    public bool isBuffsTurnOn;
    private void LateUpdate()
    {
        if (Geekplay.Instance.canShowReward)
        {
            isBuffsTurnOn = false;

            foreach (var buff in Buffs)
            {
                buff.SetActive(true);
            }
        }
    }

    public void DiactivateBuffs()
    {
        isBuffsTurnOn = false;
        foreach (var buff in Buffs)
        {
            buff.SetActive(false);
        }
    }

}
