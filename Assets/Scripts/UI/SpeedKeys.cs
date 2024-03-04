using CMF;
using UnityEngine;

public class SpeedKeys : MonoBehaviour
{
    [SerializeField] private GameObject speedKeysPanel; 
    [SerializeField] private ShopEnterAndExit inAppShop; 

    private void Start()
    {
        if(Geekplay.Instance.mobile)
        {
            speedKeysPanel.SetActive(false);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            Application.OpenURL("https://t.me/+uQFcFVwGmwM3ZDNi");
        }
        else if(Input.GetKeyDown(KeyCode.X) && Geekplay.Instance.canShowReward)
        {
            Geekplay.Instance.ShowRewardedAd("Acceleration");
        }
        else if(Input.GetKeyDown(KeyCode.P) && Geekplay.Instance.canShowReward)
        {
            Geekplay.Instance.ShowRewardedAd("IncreasePower");
        }
        else if (Input.GetKeyDown(KeyCode.F) && Geekplay.Instance.canShowReward)
        {
            Geekplay.Instance.ShowRewardedAd("DoubleSlaps");
        }
        else if (Input.GetKeyDown(KeyCode.H) && Geekplay.Instance.canShowReward)
        {
            Geekplay.Instance.ShowRewardedAd("IncreaseHP");
        }
        else if(Input.GetKeyDown(KeyCode.I))
        {
            inAppShop.OpenShop();
        }
    }
}
