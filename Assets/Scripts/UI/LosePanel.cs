using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;

    [SerializeField] private TextMeshProUGUI slapCountText;
    [SerializeField] private TextMeshProUGUI placeSlapRewardText;
    [SerializeField] private TextMeshProUGUI placeDiamondRewardText;
    [SerializeField] private TextMeshProUGUI lastedTimeToLoadHubText;
    [SerializeField] private Slider slider;

    [SerializeField] private float timeBeforeToLoadHub;

    private bool flagThatUseToLoadSceneOneTime = true;
    private float lastedTime;

    private void Start()
    {
        Geekplay.Instance.SubscribeOnReward("DoubleAward", OnDoubleAward);
        Geekplay.Instance.SubscribeOnReward("PlayerRevive", OnPlayerRevive);
    }

    private void OnEnable()
    {
        slider.maxValue = timeBeforeToLoadHub;
        lastedTime = timeBeforeToLoadHub;
    }

    void Update()
    {
        lastedTime -= Time.deltaTime;

        if (lastedTime <= 0f && flagThatUseToLoadSceneOneTime)
        {
            AddEarnedMoney();
            flagThatUseToLoadSceneOneTime = false;
            SceneLoader sceneLoader = new SceneLoader(this);
            sceneLoader.LoadScene(0);
        }
        else if(lastedTime > 0)
        {
            lastedTimeToLoadHubText.text = string.Format("00:0{0:f1}", lastedTime);
            slider.value = lastedTime;
        }
    }

    public void ShowRewardedADV(string tag)
    {
        StartCoroutine(ShowADV(tag));
    }
    private IEnumerator ShowADV(string tag)
    {
        yield return new WaitForSeconds(0.5f);

        Geekplay.Instance.ShowRewardedAd(tag);
    }
    private void OnDoubleAward()
    {
        slapCountText.text = (Convert.ToInt32(slapCountText.text) * 2).ToString();

        flagThatUseToLoadSceneOneTime = false;
        SceneLoader sceneLoader = new SceneLoader(this);
        sceneLoader.LoadScene(0);
        AddEarnedMoney();
    }
    private void OnPlayerRevive()
    {
        eventManager.InvokePlayerReviveEvents();

        gameObject.SetActive(false);
    }
    private void AddEarnedMoney() 
    {
        Geekplay.Instance.PlayerData.money += Convert.ToInt32(slapCountText.text);

        if (placeSlapRewardText != null && placeDiamondRewardText != null)
        {
            Geekplay.Instance.PlayerData.money += Convert.ToInt32(placeSlapRewardText.text);
            Geekplay.Instance.PlayerData.DiamondMoney += Convert.ToInt32(placeDiamondRewardText.text);
        }
    }
    public void SetSlapCount(int slapCount) 
    {
        if(slapCountText != null)
            slapCountText.text = slapCount.ToString();
    }
}
