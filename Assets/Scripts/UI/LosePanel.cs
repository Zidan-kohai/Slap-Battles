using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class LosePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slapCountText;
    [SerializeField] private TextMeshProUGUI lastedTimeToLoadHubText;

    [SerializeField] private float timeBeforeToLoadHub;

    private bool flagThatUseToLoadSceneOneTime = true;
    void Start()
    {
        
    }

    void Update()
    {
        timeBeforeToLoadHub -= Time.deltaTime;

        if(timeBeforeToLoadHub <= 0f && flagThatUseToLoadSceneOneTime)
        {
            flagThatUseToLoadSceneOneTime = false;
            SceneLoader sceneLoader = new SceneLoader(this);
            sceneLoader.LoadScene(0);
            AddEarnedMoney();
        }
        lastedTimeToLoadHubText.text = string.Format("{00:00}", timeBeforeToLoadHub.ToString());

    }

    private void AddEarnedMoney() => Geekplay.Instance.PlayerData.money += Convert.ToInt32(slapCountText.text);

    public void SetSlapCount(int slapCount) => slapCountText.text = slapCount.ToString();
}
