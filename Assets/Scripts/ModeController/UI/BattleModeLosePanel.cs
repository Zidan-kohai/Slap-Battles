using System;
using TMPro;
using UnityEngine;

public class BattleModeLosePanel : LosePanel
{
    [SerializeField] private TextMeshProUGUI placeSlapRewardText;
    [SerializeField] private TextMeshProUGUI placeDiamondRewardText;
    private void Start()
    {
        Geekplay.Instance.SubscribeOnReward("PlayerRevive", OnPlayerRevive);
    }
    private void OnPlayerRevive()
    {
        eventManager.InvokePlayerReviveEvents();

        gameObject.SetActive(false);
    }

    public override void AddEarnedMoney()
    {
        Geekplay.Instance.PlayerData.money += earnedSlapCount;
        Geekplay.Instance.PlayerData.money += Convert.ToInt32(placeSlapRewardText.text);
        Geekplay.Instance.PlayerData.DiamondMoney += Convert.ToInt32(placeDiamondRewardText.text);
    }

    public override void SetSlapCount(int slapCount)
    {
        earnedSlapCount = slapCount;

        placeSlapRewardText.text += (Convert.ToInt32(placeSlapRewardText.text) + slapCount).ToString();
    }
}
