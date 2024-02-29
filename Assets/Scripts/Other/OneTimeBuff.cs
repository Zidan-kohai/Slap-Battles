using System.Security.Cryptography;
using UnityEngine;

public class OneTimeBuff : MonoBehaviour
{
    [SerializeField] private BuffType buffType;
    public GameObject BuffImage;

    public OneTimeBuffController oneTimeBuffController;

    private void Start()
    {
        switch (buffType)
        {
            case BuffType.DoubleSlaps:
                Geekplay.Instance.SubscribeOnReward("DoubleSlaps", ActivateDoubleSlapBuff);
                break;
            case BuffType.Acceleration:
                Geekplay.Instance.SubscribeOnReward("Acceleration", ActivateAccelerationBuff);
                break;
            case BuffType.IncreaseHP:
                Geekplay.Instance.SubscribeOnReward("IncreaseHP", ActivateIncreaseHPBuff);
                break;
            case BuffType.IncreasePower:
                Geekplay.Instance.SubscribeOnReward("IncreasePower", ActivateIncreasePowerBuff);
                break;
        }
    }

    private void ActivateIncreaseHPBuff()
    {
        Geekplay.Instance.BuffIncreaseHP = true;
        Debug.Log("BuffIncreaseHP - " + Geekplay.Instance.BuffIncreaseHP);
        BuffImage.SetActive(true);
        oneTimeBuffController.DiactivateBuffs();
        Geekplay.Instance.TimePasedFromLastReward = 0;
    }

    private void ActivateAccelerationBuff()
    {
        Geekplay.Instance.BuffAcceleration = true;
        Debug.Log("BuffAcceleration - " + Geekplay.Instance.BuffAcceleration);
        BuffImage.SetActive(true);
        oneTimeBuffController.DiactivateBuffs();
        Geekplay.Instance.TimePasedFromLastReward = 0;
    }

    private void ActivateDoubleSlapBuff()
    {
        Geekplay.Instance.BuffDoubleSlap = true;
        Debug.Log("BuffDoubleSlap - " + Geekplay.Instance.BuffDoubleSlap);
        BuffImage.SetActive(true);
        oneTimeBuffController.DiactivateBuffs();
        Geekplay.Instance.TimePasedFromLastReward = 0;
    }

    private void ActivateIncreasePowerBuff()
    {
        Geekplay.Instance.BuffIncreasePower = true;
        Debug.Log("BuffIncreasePower - " + Geekplay.Instance.BuffIncreasePower);
        BuffImage.SetActive(true);
        oneTimeBuffController.DiactivateBuffs();
        Geekplay.Instance.TimePasedFromLastReward = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;

        switch(buffType)
        {
            case BuffType.DoubleSlaps:
                Geekplay.Instance.ShowRewardedAd("DoubleSlaps");
                break;
            case BuffType.Acceleration:
                Geekplay.Instance.ShowRewardedAd("Acceleration");
                break;
            case BuffType.IncreaseHP:
                Geekplay.Instance.ShowRewardedAd("IncreaseHP");
                break;
            case BuffType.IncreasePower:
                Geekplay.Instance.ShowRewardedAd("IncreasePower");
                break;
        }
    }

    public enum BuffType
    {
        DoubleSlaps,
        Acceleration,
        IncreaseHP,
        IncreasePower,
    }
}
