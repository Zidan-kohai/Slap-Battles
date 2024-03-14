using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class HubUI : MonoBehaviour
{
    [SerializeField] private HubEventManager eventManager;

    [SerializeField] private TextMeshProUGUI slapCountText;
    [SerializeField] private TextMeshProUGUI diamondCountText;
    [SerializeField] private GameObject mobilePanel;

    private void Start()
    {
        slapCountText.text = Geekplay.Instance.PlayerData.money.ToString();
        diamondCountText.text = Geekplay.Instance.PlayerData.DiamondMoney.ToString();

        eventManager.SubscribeOnChangeMoney(ChangeMoney);

        if(Geekplay.Instance.mobile)
        {
            mobilePanel.SetActive(true);
        }
        else
        {
            mobilePanel.SetActive(false);
        }
    }

    private void ChangeMoney(int slapCount, int diamondCount)
    {
        slapCountText.text = slapCount.ToString();
        diamondCountText.text = diamondCount.ToString();
    }

    public void Pause()
    {
        Geekplay.Instance.StopOrResume();
    }
}
