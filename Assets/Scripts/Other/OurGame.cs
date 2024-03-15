using UnityEngine;

public class OurGame : MonoBehaviour
{
    public GameObject OurGamePanel;


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            OurGamePanel.SetActive(true);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Geekplay.Instance.canPause = false;

            Geekplay.Instance.StopOrResumeWithoutPausePanel();
            Analytics.Instance.SendEvent($"Open_Other_Start");
        }
    }

    public void CloseOurGamePanel()
    {
        OurGamePanel.SetActive(false);

        Geekplay.Instance.canPause = true;

        Geekplay.Instance.StopOrResumeWithoutPausePanel();
        Geekplay.Instance.ShowInterstitialAd();
    }

    public void OpenGame(int appID)
    {
        switch (Geekplay.Instance.Platform)
        {
            case Platform.Editor:
#if INIT_DEBUG
                Debug.Log($"<color={colorDebug}>OPEN OTHER GAMES</color>");
#endif
                break;
            case Platform.Yandex:
                var domain = Utils.GetDomain();
                Application.OpenURL($"https://yandex.{domain}/games/#app={appID}");
                break;
        }

        Analytics.Instance.SendEvent($"Open_Other_Game_{appID}");
    }

    public void OpenOtherGame()
    {
        Geekplay.Instance.OpenOtherGames();
    }
}
