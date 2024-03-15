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

    protected void Update()
    {
        lastedTime -= Time.deltaTime;

        if (lastedTime <= 0f && flagThatUseToLoadSceneOneTime)
        {
            AddEarnedMoney();
            flagThatUseToLoadSceneOneTime = false;
            Geekplay.Instance.LoadScene(1);
        }
        else if (lastedTime > 0)
        {
            if (Geekplay.Instance.language == "ru")
            {
                if (lastedTime >= 5f)
                {
                    lastedTimeToLoadHubText.text = string.Format("{0:f0}", lastedTime) + " секунд";
                }
                else if (lastedTime >= 2)
                {
                    lastedTimeToLoadHubText.text = string.Format("{0:f0}", lastedTime) + " секунды";
                }
                else
                {
                    lastedTimeToLoadHubText.text = string.Format("{0:f0}", lastedTime) + " секунда";
                }
            }
            if (Geekplay.Instance.language == "en")
            {
                if (lastedTime >= 2f)
                {
                    lastedTimeToLoadHubText.text = string.Format("{0:f0}", lastedTime) + " soconds";
                }
                else
                {
                    lastedTimeToLoadHubText.text = string.Format("{0:f0}", lastedTime) + " socond";
                }
            }
            if (Geekplay.Instance.language == "tr")
            {
                lastedTimeToLoadHubText.text = string.Format("{0:f0}", lastedTime) + " saniye";
            }
            slider.value = lastedTime;
        }
    }

    private void OnPlayerRevive()
    {
        eventManager.InvokePlayerReviveEvents();

        gameObject.SetActive(false);
    }

    public override void AddEarnedMoney()
    {
        Geekplay.Instance.PlayerData.money += Convert.ToInt32(placeSlapRewardText.text) - earnedSlapCount;
        Geekplay.Instance.PlayerData.DiamondMoney += Convert.ToInt32(placeDiamondRewardText.text);
    }

    public override void SetSlapCount(int slapCount)
    {
        earnedSlapCount = slapCount;

        placeSlapRewardText.text = (Convert.ToInt32(placeSlapRewardText.text) + slapCount).ToString();
    }
}
