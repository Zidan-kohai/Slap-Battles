using UnityEngine;

public class OneTimeBuff : MonoBehaviour
{
    public BuffType buffType;

    private void Start()
    {
        Geekplay.Instance.SubscribeOnReward("DoubleSlaps", ActivateDoubleSlapBuff);
        Geekplay.Instance.SubscribeOnReward("Acceleration", ActivateAccelerationBuff);
        Geekplay.Instance.SubscribeOnReward("IncreaseHP", ActivateIncreaseHPBuff);
    }

    private void ActivateIncreaseHPBuff()
    {
        Geekplay.Instance.BuffIncreaseHP = true;
        Debug.Log("BuffIncreaseHP - " + Geekplay.Instance.BuffIncreaseHP);
    }

    private void ActivateAccelerationBuff()
    {
        Geekplay.Instance.BuffAcceleration = true;
        Debug.Log("BuffAcceleration - " + Geekplay.Instance.BuffAcceleration);
    }

    private void ActivateDoubleSlapBuff()
    {
        Geekplay.Instance.BuffDoubleSlap = true;
        Debug.Log("BuffDoubleSlap - " + Geekplay.Instance.BuffDoubleSlap);
    }

    private void OnTriggerEnter(Collider other)
    {
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
        }
    }

    public enum BuffType
    {
        DoubleSlaps,
        Acceleration,
        IncreaseHP
    }
}
