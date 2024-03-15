using CMF;
using UnityEngine;

public class SpeedKeys : MonoBehaviour
{
    [SerializeField] private GameObject speedKeysPanelOnDesc; 
    [SerializeField] private GameObject speedKeysPanelOnMobile; 
    [SerializeField] private ShopEnterAndExit inAppShop; 

    private void Start()
    {
        if (Geekplay.Instance.mobile)
        {
            speedKeysPanelOnDesc.SetActive(false);
            speedKeysPanelOnMobile.SetActive(true);
        }
        else
        {
            speedKeysPanelOnDesc.SetActive(true);
            speedKeysPanelOnMobile.SetActive(false);
        }
    }

    private void Update()
    {
        if (Geekplay.Instance.isOnPromocodeZone || Geekplay.Instance.adOpen) return;

        if(Input.GetKeyDown(KeyCode.T))
        {
            Application.OpenURL("https://t.me/+uQFcFVwGmwM3ZDNi");
        }
        else if(Input.GetKeyDown(KeyCode.X) && Geekplay.Instance.CanShowReward)
        {
            Geekplay.Instance.ShowRewardedAd("Acceleration");
        }
        else if(Input.GetKeyDown(KeyCode.P) && Geekplay.Instance.CanShowReward)
        {
            Geekplay.Instance.ShowRewardedAd("IncreasePower");
        }
        else if (Input.GetKeyDown(KeyCode.F) && Geekplay.Instance.CanShowReward)
        {
            Geekplay.Instance.ShowRewardedAd("DoubleSlaps");
        }
        else if (Input.GetKeyDown(KeyCode.H) && Geekplay.Instance.CanShowReward)
        {
            Geekplay.Instance.ShowRewardedAd("IncreaseHP");
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            inAppShop.OpenShop();
        }
    }
}
