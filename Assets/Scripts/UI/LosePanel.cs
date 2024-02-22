using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] protected EventManager eventManager;

    [SerializeField] private TextMeshProUGUI slapCountText;
    [SerializeField] private TextMeshProUGUI lastedTimeToLoadHubText;
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject rewardButton;

    [SerializeField] private float timeBeforeToLoadHub;

    private bool flagThatUseToLoadSceneOneTime = true;
    private float lastedTime;

    protected int earnedSlapCount;

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
            AddEarnedMoney();
            flagThatUseToLoadSceneOneTime = false;
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
        slapCountText.text = (earnedSlapCount * 2).ToString();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        flagThatUseToLoadSceneOneTime = false;
        SceneLoader sceneLoader = new SceneLoader(this);
        sceneLoader.LoadScene(0);
        AddEarnedMoney();
    }
    public virtual void AddEarnedMoney() 
    {
        Geekplay.Instance.PlayerData.money += earnedSlapCount;
    }
    public virtual void SetSlapCount(int slapCount) 
    {
        earnedSlapCount = slapCount;

        if (slapCountText != null)
            slapCountText.text = slapCount.ToString();
    }

    public void SetTimeBeforeLoadHub(float timeInSeconds)
    {
        timeBeforeToLoadHub = timeInSeconds;
    }

    public void DisableRewardButton()
    {
        rewardButton.SetActive(false);
    }
}
