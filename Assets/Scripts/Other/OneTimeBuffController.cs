using System.Collections.Generic;
using UnityEngine;

public class OneTimeBuffController : MonoBehaviour
{
    public List<GameObject> Buffs;
    public bool isBuffsTurnOn;
    private void Update()
    {
        bool hasBuff = Geekplay.Instance.BuffAcceleration || Geekplay.Instance.BuffDoubleSlap || Geekplay.Instance.BuffIncreaseHP || Geekplay.Instance.BuffIncreasePower;

        if (Geekplay.Instance.TimePasedFromLastReward > Geekplay.Instance.TimeToShowReward && !hasBuff && !isBuffsTurnOn)
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
