using System;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI DiamondText;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private LosePanel LosePanel;

    [SerializeField] private GameObject mobilePanel;

    public GameObject accelerationIcon;
    public GameObject doubleSlapIcon;
    public GameObject IncreaseHPIcon;
    public GameObject IncreasePowerIcon;
    private void Start()
    {
        eventManager.SubscribeOnChangeMoney(OnChangeMoney);
        eventManager.SubscribeOnChangeDiamond(OnChangeDiamond);
        eventManager.SubscribeOnPlayerDeath(OnPlayerDeath);
        eventManager.SubscribeOnPlayerRevive(OnRevive);

        moneyText.text = Geekplay.Instance.PlayerData.money.ToString();
        DiamondText.text = Geekplay.Instance.PlayerData.DiamondMoney.ToString();

        if(Geekplay.Instance.mobile)
        {
            mobilePanel.SetActive(true);
        }
        else
        {
            mobilePanel.SetActive(false);
        }

        accelerationIcon.SetActive(Geekplay.Instance.BuffAcceleration);
        doubleSlapIcon.SetActive(Geekplay.Instance.BuffDoubleSlap);
        IncreaseHPIcon.SetActive(Geekplay.Instance.BuffIncreaseHP);
        IncreasePowerIcon.SetActive(Geekplay.Instance.BuffIncreasePower);
    }

    private void OnRevive()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnPlayerDeath(int deadCount, int stolenSlaps)
    {
        LosePanel.SetSlapCount(stolenSlaps);

        if (deadCount < 2)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            LosePanel.gameObject.SetActive(true);;

        }else if(deadCount >= 2)
        {
            //TO Do Refactoring
            if (Geekplay.Instance.currentMode == Modes.ClashRoyal)
                LosePanel.AddEarnedMoney();

            SceneLoader sceneLoader = new SceneLoader(this);
            sceneLoader.LoadScene(0);
        }
    }

    private void OnChangeMoney(int money)
    {
        Geekplay.Instance.PlayerData.money += money;
        moneyText.text = Geekplay.Instance.PlayerData.money.ToString();
    }

    private void OnChangeDiamond(int diamond)
    {
        Geekplay.Instance.PlayerData.DiamondMoney += diamond;
        DiamondText.text = Geekplay.Instance.PlayerData.DiamondMoney.ToString();
    }
}
