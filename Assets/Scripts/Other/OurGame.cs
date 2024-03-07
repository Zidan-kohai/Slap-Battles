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
        }
    }

    public void CloseOurGamePanel()
    {
        OurGamePanel.SetActive(false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Geekplay.Instance.canPause = true;

        Geekplay.Instance.StopOrResumeWithoutPausePanel();
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
    }

    public void OpenOtherGame()
    {
        Geekplay.Instance.OpenOtherGames();
    }
}
