using System.Collections.Generic;
using TMPro;
using Unity.PlasticSCM.Editor.WebApi;
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
    [SerializeField] private ShopEnterAndExit inAppShop;


    [SerializeField] private Button buyThreeSlapButton;
    [SerializeField] private Button buyAllSlapsButton;

    private GameObject selectedIcons;
    [SerializeField] private int currentSlapIndexOnShop;
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

        if(Geekplay.Instance.PlayerData.BuyAllSlaps)
        {
            buyAllSlapsButton.gameObject.SetActive(false);
        }
        else
        {
            buyAllSlapsButton.onClick.AddListener(GiveAllSlaps);
        }

        if(Geekplay.Instance.PlayerData.BuyThreeSlaps)
        {
            buyThreeSlapButton.gameObject.SetActive(false);
        }
        else
        {
            buyThreeSlapButton.onClick.AddListener(Give3Slap);
        }
    }

    public void Give3Slap()
    {
        if (Geekplay.Instance.PlayerData.DiamondMoney >= 50)
        {
            Geekplay.Instance.PlayerData.DiamondMoney -= 50;
            eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
            Geekplay.Instance.StartRatingSystem();

            Geekplay.Instance.PlayerData.BuyThreeSlaps = true;
            buyThreeSlapButton.gameObject.SetActive(false);


            if(currentSlapIndexOnShop == 9 || currentSlapIndexOnShop == 11 || currentSlapIndexOnShop == 12)
            {
                if (!buyableSlaps[currentSlapIndexOnShop].GetIsBuyed)
                {
                    if (Geekplay.Instance.language == "ru")
                    {
                        buyText.text = "Надеть";
                    }
                    else if (Geekplay.Instance.language == "en")
                    {
                        buyText.text = "Put on";
                    }
                    else if (Geekplay.Instance.language == "tr")
                    {
                        buyText.text = "Giymek";
                    }
                    buySlapIcon.SetActive(false);
                    buyDiamondIcon.SetActive(false);
                }
            }

            buyableSlaps[9].Buyed();
            buyableSlaps[11].Buyed();
            buyableSlaps[12].Buyed();
        }
        else
        {
            inAppShop.OpenShop();
            gameObject.SetActive(false);
        }


    }

    public void GiveAllSlaps()
    {
        if (Geekplay.Instance.PlayerData.DiamondMoney >= 100)
        {
            Geekplay.Instance.PlayerData.DiamondMoney -= 100;
            eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
            Geekplay.Instance.StartRatingSystem();
            Geekplay.Instance.PlayerData.BuyAllSlaps = true;
            buyAllSlapsButton.gameObject.SetActive(false);


            if (!buyableSlaps[currentSlapIndexOnShop].GetIsBuyed)
            {
                if (Geekplay.Instance.language == "ru")
                {
                    buyText.text = "Надеть";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    buyText.text = "Put on";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    buyText.text = "Giymek";
                }
                buySlapIcon.SetActive(false);
                buyDiamondIcon.SetActive(false);
            }

            foreach (var item in buyableSlaps)
            {
                item.Buyed();
            }
        }
        else
        {
            inAppShop.OpenShop();
            gameObject.SetActive(false);
        }
    }
    public void InApp(string id)
    {
        Geekplay.Instance.RealBuyItem(id);
    }

    private void AddEventForBuyableSlap(BuyableSlap buyable)
    {
        buyable.SubscribeOnClick(() =>
        {
            buyButton.gameObject.SetActive(true);
            slapSwitcher.SwitchSlap(buyable.GetIndexOfSlap);
            buyButton.onClick.RemoveAllListeners();

            ReplaceSelectedIcon(buyable);

            currentSlapIndexOnShop = buyable.GetIndexOfSlap;

            if (buyable.GetIsBuyed)
            {
                buySlapIcon.SetActive(false);
                buyDiamondIcon.SetActive(false);

                if (Geekplay.Instance.PlayerData.currentSlap == buyable.GetIndexOfSlap)
                {
                    if (Geekplay.Instance.language == "ru")
                    {
                        buyText.text = "Надето";
                    }
                    else if (Geekplay.Instance.language == "en") 
                    {
                        buyText.text = "Wearing";
                    }
                    else if (Geekplay.Instance.language == "tr")
                    {
                        buyText.text = "giyme";
                    }

                }
                else
                {
                    if (Geekplay.Instance.language == "ru")
                    {
                        buyText.text = "Надеть";
                    }
                    else if (Geekplay.Instance.language == "en")
                    {
                        buyText.text = "Put on";
                    }
                    else if (Geekplay.Instance.language == "tr")
                    {
                        buyText.text = "Giymek";
                    }

                    buyButton.onClick.AddListener(() =>
                    {
                        slapSwitcher.SaveAndSwitchSlap(buyable.GetIndexOfSlap);

                        if (Geekplay.Instance.language == "ru")
                        {
                            buyText.text = "Надето";
                        }
                        else if (Geekplay.Instance.language == "en")
                        {
                            buyText.text = "Wearing";
                        }
                        else if (Geekplay.Instance.language == "tr")
                        {
                            buyText.text = "giyme";
                        }
                    });
                }
            }
            else
            {
                if (Geekplay.Instance.language == "ru")
                {
                    buyText.text = $"купить {buyable.GetCost}";
                }
                else if (Geekplay.Instance.language == "en")
                {
                    buyText.text = $"Buy {buyable.GetCost}";
                }
                else if (Geekplay.Instance.language == "tr")
                {
                    buyText.text = $"satin almak {buyable.GetCost}";
                }

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
                        if (Geekplay.Instance.language == "ru")
                        {
                            buyText.text = "Надето";
                        }
                        else if (Geekplay.Instance.language == "en")
                        {
                            buyText.text = "Wearing";
                        }
                        else if (Geekplay.Instance.language == "tr")
                        {
                            buyText.text = "giyme";
                        }
                        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);

                        buySlapIcon.SetActive(false);
                        buyDiamondIcon.SetActive(false);
                        Geekplay.Instance.StartRatingSystem();

                        Analytics.Instance.SendEvent($"Buy_Glove_{buyable.GetIndexOfSlap}");
                    });
                }

                else
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        Debug.Log("You don`t Have money");
                        inAppShop.OpenShop();
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
            if(Geekplay.Instance.PlayerData.currentSlap == buyable.GetIndexOfSlap)
            {
                //Put on
                currentSlapIndexOnShop = buyable.GetIndexOfSlap;
                ReplaceSelectedIcon(buyable);
            }
        }
    }

    private void ReplaceSelectedIcon(Buyable buyable)
    {
        RectTransform rect = buyable.GetComponent<RectTransform>();
        if (selectedIcons != null) Destroy(selectedIcons);

        selectedIcons = Instantiate(Resources.Load<GameObject>("SelectedIcon"), rect);
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
