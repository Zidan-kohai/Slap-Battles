using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] protected EventManager eventManager;

    [SerializeField] private TextMeshProUGUI slapCountText;
    [SerializeField] private TextMeshProUGUI diamondCountText;
    [SerializeField] protected TextMeshProUGUI lastedTimeToLoadHubText;
    [SerializeField] protected Slider slider;
    [SerializeField] private GameObject rewardButton;

    [SerializeField] protected float timeBeforeToLoadHub;

    protected bool flagThatUseToLoadSceneOneTime = true;
    protected float lastedTime;

    protected int earnedSlapCountBeforeDouble;
    protected int earnedSlapCount;
    protected int earnedDiamondCountBeforeDouble;
    protected int earnedDiamondCount;


    private void Start()
    {
        Geekplay.Instance.SubscribeOnReward("DoubleAward", OnDoubleAward);
        Geekplay.Instance.SubscribeOnReward("PlayerRevive", OnPlayerRevive);
    }

    private void OnPlayerRevive()
    {
        eventManager.InvokePlayerReviveEvents();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        slider.maxValue = timeBeforeToLoadHub;
        lastedTime = timeBeforeToLoadHub;
    }

    protected void Update()
    {
        lastedTime -= Time.deltaTime;

        if (lastedTime <= 0f && flagThatUseToLoadSceneOneTime)
        {
            flagThatUseToLoadSceneOneTime = false;
            AddEarnedMoney();
            SceneLoader sceneLoader = new SceneLoader(this);
            sceneLoader.LoadScene(0);
        }
        else if(lastedTime > 0)
        {
            lastedTimeToLoadHubText.text = string.Format("{0:f0}", lastedTime);
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
        earnedSlapCount = earnedSlapCountBeforeDouble * 2;
        earnedDiamondCount = earnedDiamondCountBeforeDouble * 2;
        float currentTime = 0;

        lastedTime = 3;

        slapCountText.rectTransform.DOScale(new Vector3(1.01f, 1.01f, 1.01f),3).OnUpdate(() =>
        {
            currentTime += Time.deltaTime;
            float currentSlap = CalculateMoney(Convert.ToInt32(slapCountText.text), earnedSlapCount, currentTime, 3);
            float currentDiamond = CalculateMoney(Convert.ToInt32(diamondCountText.text), earnedDiamondCount, currentTime, 3);

            slapCountText.text = string.Format("{0:f0}", currentSlap);
            diamondCountText.text = string.Format("{0:f0}", currentDiamond);

        }).SetEase(Ease.Linear);



        slider.gameObject.SetActive(false);
        DisableRewardButton();
    }

    public float CalculateMoney(float startMoney, float endMoney, float currentTime, float maxTime)
    {
        if (currentTime == 1)
        {
            return startMoney;
        }
        else if (currentTime >= maxTime)
        {
            return endMoney;
        }
        else
        {
            float t = currentTime / maxTime;
            float interpolatedMoney = startMoney + (endMoney - startMoney) * t;
            return interpolatedMoney;
        }
    }
    public virtual void AddEarnedMoney() 
    {
        Geekplay.Instance.PlayerData.money += earnedSlapCount - earnedSlapCountBeforeDouble;
        Geekplay.Instance.PlayerData.DiamondMoney += earnedDiamondCount - earnedDiamondCountBeforeDouble;
        Geekplay.Instance.PlayerData.LeaderboardSlap += earnedSlapCount - earnedSlapCountBeforeDouble;
    }
    public virtual void SetSlapCount(int slapCount) 
    {
        earnedSlapCountBeforeDouble = slapCount;
        earnedSlapCount = slapCount;

        if (slapCountText != null)
            slapCountText.text = slapCount.ToString();
    }

    public virtual int GetSlapCount() => earnedSlapCountBeforeDouble;
    public virtual void SetDiamondCount(int diamondCount)
    {
        earnedDiamondCountBeforeDouble = diamondCount;
        earnedDiamondCount = diamondCount;

        if (diamondCountText != null)
            diamondCountText.text = diamondCount.ToString();
    }

    public virtual int GetDiamondCount() => earnedDiamondCountBeforeDouble;


    public void SetTimeBeforeLoadHub(float timeInSeconds)
    {
        timeBeforeToLoadHub = timeInSeconds;
    }

    public void DisableRewardButton()
    {
        rewardButton.SetActive(false);
    }
}
