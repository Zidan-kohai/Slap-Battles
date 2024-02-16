using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slapCountText;
    [SerializeField] private TextMeshProUGUI lastedTimeToLoadHubText;
    [SerializeField] private Slider slider;

    [SerializeField] private float timeBeforeToLoadHub;
    private bool flagThatUseToLoadSceneOneTime = true;
    void Start()
    {
        Geekplay.Instance.SubscribeOnReward("DoubleAward", DoubleAward);
        slider.maxValue = timeBeforeToLoadHub;
    }

    void Update()
    {
        timeBeforeToLoadHub -= Time.deltaTime;

        if (timeBeforeToLoadHub <= 0f && flagThatUseToLoadSceneOneTime)
        {
            flagThatUseToLoadSceneOneTime = false;
            SceneLoader sceneLoader = new SceneLoader(this);
            sceneLoader.LoadScene(0);
            AddEarnedMoney();
        }
        else if(timeBeforeToLoadHub > 0)
        {
            lastedTimeToLoadHubText.text = string.Format("00:0{0:f1}", timeBeforeToLoadHub);
            slider.value = timeBeforeToLoadHub;
        }
    }

    public void ShowRewardedADV()
    {
        StartCoroutine(ShowADV());
    }
    private void DoubleAward()
    {
        slapCountText.text = (Convert.ToInt32(slapCountText.text) * 2).ToString();

        flagThatUseToLoadSceneOneTime = false;
        SceneLoader sceneLoader = new SceneLoader(this);
        sceneLoader.LoadScene(0);
        AddEarnedMoney();
    }
    private IEnumerator ShowADV()
    {
        yield return new WaitForSeconds(0.5f);

        Geekplay.Instance.ShowRewardedAd("DoubleAward");
    }


    private void AddEarnedMoney() => Geekplay.Instance.PlayerData.money += Convert.ToInt32(slapCountText.text);

    public void SetSlapCount(int slapCount) => slapCountText.text = slapCount.ToString();
}
