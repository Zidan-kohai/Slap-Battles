using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private TextMeshProUGUI DiamondText;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private LosePanel LosePanel;
    [SerializeField] private LosePanel winPanel;
    [SerializeField] private GameObject mobilePanel;

    private RectTransform moneyTextRect;
    private RectTransform DiamondTextRect;

    public GameObject accelerationIcon;
    public GameObject doubleSlapIcon;
    public GameObject IncreaseHPIcon;
    public GameObject IncreasePowerIcon;


    public float moneyChangeAnimationDuration;
    public Vector3 increaseSize;
    private void Start()
    {
        eventManager.SubscribeOnChangeMoney(OnChangeMoney);
        eventManager.SubscribeOnChangeDiamond(OnChangeDiamond);
        eventManager.SubscribeOnPlayerDeath(OnPlayerDeath);
        eventManager.SubscribeOnPlayerRevive(OnRevive);
        eventManager.SubscribeOnBossDeath(OnBossDeath);

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

        moneyTextRect = moneyText.rectTransform;
        DiamondTextRect = DiamondText.rectTransform;
    }

    private void OnBossDeath()
    {
        winPanel.gameObject.SetActive(true);
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

            Geekplay.Instance.LoadScene(1);
        }
    }



    private void OnChangeMoney(int money)
    {
        if(winPanel != null)
        {
            winPanel.SetSlapCount(winPanel.GetSlapCount() + money);
        }
        Geekplay.Instance.PlayerData.money += money;
        moneyText.text = Geekplay.Instance.PlayerData.money.ToString();
        moneyTextRect.DOScale(increaseSize, moneyChangeAnimationDuration).SetDelay(0.1f).OnComplete(() =>
        {
            moneyTextRect.DOScale(Vector3.one, moneyChangeAnimationDuration);
        });
    }

    private void OnChangeDiamond(int diamond)
    {
        if (winPanel)
        {
            winPanel.SetDiamondCount(winPanel.GetDiamondCount() + diamond);
        }
        Geekplay.Instance.PlayerData.DiamondMoney += diamond;
        DiamondText.text = Geekplay.Instance.PlayerData.DiamondMoney.ToString();

        DiamondTextRect.DOScale(increaseSize, moneyChangeAnimationDuration).SetDelay(0.1f).OnComplete(() =>
        {
            DiamondTextRect.DOScale(Vector3.one, moneyChangeAnimationDuration);
        });
    }
}
