using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlapShopController : MonoBehaviour
{
    [Space(10), Header("Slaps")]
    [SerializeField] SlapSwitcher slapSwitcher;
    [SerializeField] private List<BuyableSlap> buyableSlaps;


    [Space(10), Header("Buy Button")]
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private GameObject buySlapIcon;
    [SerializeField] private GameObject buyDiamondIcon;


    [SerializeField] private HubEventManager eventManager;
    [SerializeField] private GameObject inAppShop;
    private void OnEnable()
    {
        slapSwitcher.ShowSlaps();
    }

    private void Start()
    {
        CheckIsBuyedSlap();

        for(int i = 0; i < buyableSlaps.Count; i++)
        {
            AddEventForBuyableSlap(buyableSlaps[i]);
        }
    }
    private void AddEventForBuyableSlap(BuyableSlap buyable)
    {
        buyable.SubscribeOnClick(() =>
        {
            buyButton.gameObject.SetActive(true);
            slapSwitcher.SwitchSlap(buyable.GetIndexOfSlap);
            buyButton.onClick.RemoveAllListeners();

            if (buyable.GetIsBuyed)
            {
                buySlapIcon.SetActive(false);
                buyDiamondIcon.SetActive(false);

                if (Geekplay.Instance.PlayerData.currentSlap == buyable.GetIndexOfSlap)
                {
                    buyText.text = "Надето";
                }
                else
                {
                    buyText.text = "Надеть";

                    buyButton.onClick.AddListener(() =>
                    {
                        slapSwitcher.SaveAndSwitchSlap(buyable.GetIndexOfSlap);
                        buyText.text = "Надето";
                    });
                }
            }
            else
            {
                buyText.text = $"купить {buyable.GetCost}";

                if (buyable.costType == Buyable.TypeOfCost.money)
                {
                    buySlapIcon.SetActive(true);
                    buyDiamondIcon.SetActive(false);
                }
                else if (buyable.costType == Buyable.TypeOfCost.diamond)
                {
                    buyDiamondIcon.SetActive(true);
                    buySlapIcon.SetActive(false);
                }

                if (buyable.TryBuy())
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        slapSwitcher.SwitchAndBuySlap(buyable.GetIndexOfSlap);

                        buyable.Buy();
                        buyText.text = "Надето";
                        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
                    });
                }
                else
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        Debug.Log("You don`t Have money");
                        inAppShop.SetActive(true);
                        gameObject.SetActive(false);
                    });
                }
            }
        });
    }

    private void CheckIsBuyedSlap()
    {
        foreach (var buyable in buyableSlaps)
        {
            if (Geekplay.Instance.PlayerData.BuyedSlaps.Contains(buyable.GetIndexOfSlap))
            {
                buyable.Buyed();
            }
        }
    }

    private void ChangePlayerSlap()
    {
        slapSwitcher.SwitchSlap(Geekplay.Instance.PlayerData.currentSlap);
    }

    private void OnDisable()
    {
        ChangePlayerSlap();

        slapSwitcher.DisableSlaps();
    }
}
