using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class HubUI : MonoBehaviour
{
    [SerializeField] private HubEventManager eventManager;

    [SerializeField] private TextMeshProUGUI slapCountText;
    [SerializeField] private TextMeshProUGUI diamondCountText;
    [SerializeField] private GameObject mobilePanel;
    [SerializeField] private GameObject TutorPanel;
    [SerializeField] private SkineShopController skinController;

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
        
        if(Geekplay.Instance.PlayerData.IsFirstPlay)
        {
            TutorPanel.gameObject.SetActive(true);
            //Geekplay.Instance.StopOrResumeWithoutPausePanel();

            Time.timeScale = 0f;
            Geekplay.Instance.PlayerData.IsFirstPlay = false;
            CanPause(canPause: false);

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        skinController.ResetToWeared();

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

    public void CanPause(bool canPause)
    {
        Geekplay.Instance.canPause = canPause;
    }

    public void ReturnFromTutor()
    {
        Time.timeScale = 1f;
        if(!Geekplay.Instance.mobile)
        {
            Cursor.lockState= CursorLockMode.Locked; 
            Cursor.visible = false;
        }
    }
}
