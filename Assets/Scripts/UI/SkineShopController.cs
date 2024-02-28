using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//TO DO Refactoring
public class SkineShopController : MonoBehaviour
{
    [Space(10), Header("Buy Button")]
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI buyText;
    [SerializeField] private GameObject buySlapIcon;
    [SerializeField] private GameObject buyDiamondIcon;


    [Header("Material of Body Part"), Space(5)]
    [SerializeField] private Material bodyMaterial;
    [SerializeField] private List<Material> LeatherMaterial;
    [SerializeField] private Material hairMaterial;
    [SerializeField] private Material legMaterial;
    [SerializeField] private Material footMaterial;
    [Space(10)]

    [SerializeField] private List<BuyableColor> hairColors;
    [SerializeField] private List<BuyableColor> LeatherColors;
    [SerializeField] private List<BuyableColor> bodyColors;
    [SerializeField] private List<BuyableColor> legColors;
    [SerializeField] private List<BuyableColor> footColors;

    [Space(10), Header("Gender")]
    [SerializeField] private Button manButton;
    [SerializeField] private Button womanButton;
    [SerializeField] private GameObject manHairIcon;
    [SerializeField] private GameObject manHairColorIcon;
    [SerializeField] private GameObject manSelectIcon;
    [SerializeField] private GameObject womanHairIcon;
    [SerializeField] private GameObject womanHairColorIcon;
    [SerializeField] private GameObject womanSelectIcon;


    [Space(10), Header("Hair")]
    [SerializeField] HairSwitcher hairSwitcher;

    [SerializeField] private List<HairBuyable> manHairs;
    [SerializeField] private List<HairBuyable> womanHairs;


    [Space(10), Header("Accessory")]
    [SerializeField] AccessorySwitcher accessorySwitcher;
    [SerializeField] private List<BuyableAccessory> buyableAccessories;

    [Space(10), Header("Caps")]
    [SerializeField] CapSwitcher capSwitcher;
    [SerializeField] private List<BuyableCap> buyableCaps;


    [Space(10), Header("Buff")]
    [SerializeField] private HealtBuffSystem healthBuffSystem;
    [SerializeField] private TextMeshProUGUI currentHealthText;
    [SerializeField] private TextMeshProUGUI healthBuffText;


    [SerializeField] private HubEventManager eventManager;
    [SerializeField] private GameObject inAppShop;

    private GameObject leatherSelectedIcon;
    private GameObject hairColorSelectedIcon;
    private GameObject bodySelectedIcon;
    private GameObject legSelectedIcon;
    private GameObject footSelectedIcon;
    private GameObject hairManSelectedIcon;
    private GameObject hairWomanSelectedIcon;
    private GameObject accessorySelectedIcon;
    private GameObject capSelectedIcon;
    private void Start()
    {
        CheckIsBuyedOrEquippedColor();
        CheckIsBuyedOrEquippedHair();
        CheckIsBuyedOrEquippedAccessory();
        CheckIsBuyedOrEquippedCap();

        #region Subscribe On Color Buy Events
        foreach (var item in hairColors)
        {
            AddEventForBuyableColor(item);
        }
        foreach (var item in LeatherColors)
        {
            AddEventForBuyableColor(item);
        }
        foreach (var item in bodyColors)
        {
            AddEventForBuyableColor(item);
        }
        foreach (var item in legColors)
        {
            AddEventForBuyableColor(item);
        }
        foreach (var item in footColors)
        {
            AddEventForBuyableColor(item);
        }
        #endregion

        #region Hair
        foreach (var item in manHairs)
        {
            AddEventForBuyableHair(item);
        }
        foreach (var item in womanHairs)
        {
            AddEventForBuyableHair(item);
        }
        #endregion

        #region Accessory

        foreach (var item in buyableAccessories)
        {
            AddEventForBuyableAccessory(item);
        }
        #endregion

        #region Cap
        foreach (var item in buyableCaps)
        {
            AddEventForBuyableCap(item);
        }
        #endregion

        manButton.onClick.AddListener(() =>
        {
            currentHealthText.text = healthBuffSystem.GetMaxHealthObject.ToString();
            healthBuffSystem.OnSwitchGender();

            foreach (var item in manHairs)
            {
                if(Geekplay.Instance.PlayerData.currentManHair == item.GetIndexOfhair)
                {
                    ChangeHealthBuff(HealtBuffSystem.HealtBuffType.hair,item.HealthBuff);
                }
            }

            womanHairIcon.SetActive(false);
            womanHairColorIcon.SetActive(false);
            womanSelectIcon.SetActive(false);

            manSelectIcon.SetActive(true);
            manHairIcon.SetActive(true);
            manHairColorIcon.SetActive(true);  
        });

        womanButton.onClick.AddListener(() =>
        {
            currentHealthText.text = healthBuffSystem.GetMaxHealthObject.ToString();
            healthBuffSystem.OnSwitchGender();

            foreach (var item in womanHairs)
            {
                if (Geekplay.Instance.PlayerData.currentWomanHair == item.GetIndexOfhair)
                {
                    ChangeHealthBuff(HealtBuffSystem.HealtBuffType.hair, item.HealthBuff);
                }
            }

            womanHairIcon.SetActive(true);
            womanHairColorIcon.SetActive(true);
            womanSelectIcon.SetActive(true);

            manSelectIcon.SetActive(false);
            manHairIcon.SetActive(false);
            manHairColorIcon.SetActive(false);
        });

        if (Geekplay.Instance.PlayerData.isGenderMan)
        {
            womanHairIcon.SetActive(false);
            womanHairColorIcon.SetActive(false);
            womanSelectIcon.SetActive(false);

            manSelectIcon.SetActive(true);
            manHairIcon.SetActive(true);
            manHairColorIcon.SetActive(true);
        }
        else
        {
            womanHairIcon.SetActive(true);
            womanHairColorIcon.SetActive(true);
            womanSelectIcon.SetActive(true);

            manSelectIcon.SetActive(false);
            manHairIcon.SetActive(false);
            manHairColorIcon.SetActive(false);
        }
    }

    #region ColorSwitching
    private void AddEventForBuyableColor(BuyableColor buyable)
    {
        Debug.Log(buyable.gameObject.name + " " + buyable.GetBodyType);
        buyable.SubscribeOnClick(() =>
        {

            buyButton.gameObject.SetActive(true);
            List<Material> material = new List<Material>();
            switch (buyable.GetBodyType)
            {
                case BodyPart.Leather:
                    material = LeatherMaterial;
                    ReplaceSelectedIcon(buyable, ref leatherSelectedIcon);
                    break;
                case BodyPart.Hair:
                    material.Add(hairMaterial);
                    ReplaceSelectedIcon(buyable, ref hairColorSelectedIcon);
                    break;
                case BodyPart.Body:
                    material.Add(bodyMaterial);
                    ReplaceSelectedIcon(buyable, ref bodySelectedIcon);
                    break;
                case BodyPart.Leg:
                    material.Add(legMaterial);
                    ReplaceSelectedIcon(buyable, ref legSelectedIcon);
                    break;
                case BodyPart.Foot:
                    material.Add(footMaterial);
                    ReplaceSelectedIcon(buyable, ref footSelectedIcon);
                    break;
            }

            buyButton.onClick.RemoveAllListeners();
            if(buyable.GetIsBuyed)
            {
                buySlapIcon.SetActive(false);
                buyDiamondIcon.SetActive(false);

                buyButton.onClick.AddListener(() =>
                {
                    foreach (Material m in material)
                        ChangeLastColor(m, buyable);

                    buyText.text = "Надето";
                });

                bool isEquiped = false;

                ChangeColorInShop(buyable);

                switch (buyable.GetBodyType)
                {
                    case BodyPart.Leather:
                        if (Geekplay.Instance.PlayerData.CurrentLeatherColorIndex == buyable.indexOfColor)
                            isEquiped = true;
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.HeadColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Hair:
                        if (Geekplay.Instance.PlayerData.CurrentHairColorIndex == buyable.indexOfColor)
                            isEquiped = true;
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.HairColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Body:
                        if (Geekplay.Instance.PlayerData.CurrentBodyColorIndex == buyable.indexOfColor)
                            isEquiped = true;
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.BodyColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Leg:
                        if (Geekplay.Instance.PlayerData.CurrentLegColorIndex == buyable.indexOfColor)
                            isEquiped = true;
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.legColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Foot:
                        if (Geekplay.Instance.PlayerData.CurrentFootColorIndex == buyable.indexOfColor)
                            isEquiped = true;
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.footColor, buyable.HealthBuff);
                        break;
                }

                if (isEquiped)
                {
                    buyText.text = "Надето";
                }
                else
                {
                    buyText.text = "Надеть";
                }
            }
            else
            {
                switch (buyable.GetBodyType)
                {
                    case BodyPart.Leather:
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.HeadColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Hair:
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.HairColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Body:
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.BodyColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Leg:
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.legColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Foot:
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.footColor, buyable.HealthBuff);
                        break;
                }

                if (buyable.TryBuy())
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        foreach(var m in material)
                            BuyAndChangeLastColor(m, buyable);

                        buyText.text = "Надето";
                        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
                    });

                    ChangeColorInShop(buyable);
                }
                else
                {
                    ChangeColorInShop(buyable);

                    buyButton.onClick.AddListener(() =>
                    {
                        Debug.Log("Don`t have money");
                        inAppShop.SetActive(true);
                        gameObject.SetActive(false);
                    });
                }

                buyText.text = $"купить {buyable.GetCost}";
                if(buyable.costType == Buyable.TypeOfCost.money)
                {
                    buySlapIcon.SetActive(true);
                    buyDiamondIcon.SetActive(false);
                }
                else if(buyable.costType == Buyable.TypeOfCost.diamond)
                {
                    buyDiamondIcon.SetActive(true);
                    buySlapIcon.SetActive(false);
                }
            }
        });
    }

    private void ChangeColorInShop(BuyableColor buyable)
    {
        switch (buyable.GetBodyType)
        {
            case BodyPart.Leather:
                foreach(var material in LeatherMaterial)
                    material.color = buyable.GetColor;
                break;
            case BodyPart.Hair:
                hairMaterial.color = buyable.GetColor;
                break;
            case BodyPart.Body:
                bodyMaterial.color = buyable.GetColor;
                break;
            case BodyPart.Leg:
                legMaterial.color = buyable.GetColor;
                break;
            case BodyPart.Foot:
                footMaterial.color = buyable.GetColor;
                break;
        }
    }

    private void ChangeLastColor(Material material, BuyableColor buyable)
    {
        switch (buyable.GetBodyType)
        {
            case BodyPart.Leather:
                Geekplay.Instance.PlayerData.CurrentLeatherColorIndex = buyable.indexOfColor;
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.HeadColor, buyable.HealthBuff);
                break;
            case BodyPart.Hair:
                Geekplay.Instance.PlayerData.CurrentHairColorIndex = buyable.indexOfColor;
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.HairColor, buyable.HealthBuff);
                break;
            case BodyPart.Body:
                Geekplay.Instance.PlayerData.CurrentBodyColorIndex = buyable.indexOfColor;
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.BodyColor, buyable.HealthBuff);
                break;
            case BodyPart.Leg:
                Geekplay.Instance.PlayerData.CurrentLegColorIndex = buyable.indexOfColor;
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.legColor, buyable.HealthBuff);
                break;
            case BodyPart.Foot:
                Geekplay.Instance.PlayerData.CurrentFootColorIndex = buyable.indexOfColor;
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.footColor, buyable.HealthBuff);
                break;
        }


        material.color = buyable.GetColor;
    }

    private void BuyAndChangeLastColor(Material material, BuyableColor buyable)
    {
        switch (buyable.GetBodyType)
        {
            case BodyPart.Leather:
                Geekplay.Instance.PlayerData.CurrentLeatherColorIndex = buyable.indexOfColor;
                Geekplay.Instance.PlayerData.BuyedLeatherColors.Add(buyable.indexOfColor);
                break;
            case BodyPart.Hair:
                Geekplay.Instance.PlayerData.CurrentHairColorIndex = buyable.indexOfColor;
                Geekplay.Instance.PlayerData.BuyedHairColors.Add(buyable.indexOfColor);
                break;
            case BodyPart.Body:
                Geekplay.Instance.PlayerData.CurrentBodyColorIndex = buyable.indexOfColor;
                Geekplay.Instance.PlayerData.BuyedBodyColors.Add(buyable.indexOfColor);
                break;
            case BodyPart.Leg:
                Geekplay.Instance.PlayerData.CurrentLegColorIndex = buyable.indexOfColor;
                Geekplay.Instance.PlayerData.BuyedLegColors.Add(buyable.indexOfColor);
                break;
            case BodyPart.Foot:
                Geekplay.Instance.PlayerData.CurrentFootColorIndex = buyable.indexOfColor;
                Geekplay.Instance.PlayerData.BuyedFootColors.Add(buyable.indexOfColor);
                break;
        }

        buyable.Buy();
        ChangeLastColor(material, buyable);
    }

    private void CheckIsBuyedOrEquippedColor()
    {
        foreach(var buyable in LeatherColors)
        {
            if(Geekplay.Instance.PlayerData.BuyedLeatherColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
            }
            if(Geekplay.Instance.PlayerData.CurrentLeatherColorIndex == buyable.indexOfColor)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.HeadColor,buyable.HealthBuff);
                ReplaceSelectedIcon(buyable, ref leatherSelectedIcon);
            }
        }

        foreach (var buyable in hairColors)
        {
            if (Geekplay.Instance.PlayerData.BuyedHairColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
            }
            if (Geekplay.Instance.PlayerData.CurrentHairColorIndex == buyable.indexOfColor)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.HairColor, buyable.HealthBuff);
                ReplaceSelectedIcon(buyable, ref hairColorSelectedIcon);
            }
        }

        foreach (var buyable in bodyColors)
        {
            if (Geekplay.Instance.PlayerData.BuyedBodyColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
            }
            if (Geekplay.Instance.PlayerData.CurrentBodyColorIndex == buyable.indexOfColor)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.BodyColor, buyable.HealthBuff);
                ReplaceSelectedIcon(buyable, ref bodySelectedIcon);
            }
        }

        foreach (var buyable in legColors)
        {
            if (Geekplay.Instance.PlayerData.BuyedLegColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
            }
            if (Geekplay.Instance.PlayerData.CurrentLegColorIndex == buyable.indexOfColor)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.legColor, buyable.HealthBuff);
                ReplaceSelectedIcon(buyable, ref legSelectedIcon);
            }
        }

        foreach (var buyable in footColors)
        {
            if (Geekplay.Instance.PlayerData.BuyedFootColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
            }
            if (Geekplay.Instance.PlayerData.CurrentFootColorIndex == buyable.indexOfColor)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.footColor, buyable.HealthBuff);
                ReplaceSelectedIcon(buyable, ref footSelectedIcon);
            }
        }
    }

    public void ChangeColor(BodyPart bodyPart, Color color)
    {
        switch (bodyPart)
        {
            case BodyPart.Leather:
                foreach(var mat in LeatherMaterial)
                    mat.color = color;
                break;
            case BodyPart.Body:
                bodyMaterial.color = color;
                break;
            case BodyPart.Leg:
                legMaterial.color = color;
                break;
            case BodyPart.Foot:
                footMaterial.color = color;
                break;
        }
    }

    private void ChangePlayerMaterial()
    {
        foreach(var item in LeatherColors)
        {
            if (item.indexOfColor == Geekplay.Instance.PlayerData.CurrentLeatherColorIndex)
            {
                foreach (var mat in LeatherMaterial)
                    mat.color = item.GetColor;
            }
        }
        foreach (var item in hairColors)
        {
            if (item.indexOfColor == Geekplay.Instance.PlayerData.CurrentHairColorIndex)
                hairMaterial.color = item.GetColor;
        }
        foreach (var item in bodyColors)
        {
            if (item.indexOfColor == Geekplay.Instance.PlayerData.CurrentBodyColorIndex)
                bodyMaterial.color = item.GetColor;
        }
        foreach (var item in legColors)
        {
            if (item.indexOfColor == Geekplay.Instance.PlayerData.CurrentLegColorIndex)
                legMaterial.color = item.GetColor;
        }
        foreach (var item in footColors)
        {
            if (item.indexOfColor == Geekplay.Instance.PlayerData.CurrentFootColorIndex)
                footMaterial.color = item.GetColor;
        }
    }

    #endregion

    #region HairSwitching
    private void AddEventForBuyableHair(HairBuyable buyable)
    {
        buyable.SubscribeOnClick(() =>
        {
            if (Geekplay.Instance.PlayerData.isGenderMan)
            {
                ReplaceSelectedIcon(buyable, ref hairManSelectedIcon);
            }
            else
            {
                ReplaceSelectedIcon(buyable, ref hairWomanSelectedIcon);
            }

            buyButton.gameObject.SetActive(true);

            capSwitcher.SaveAndSwitchCap(buyableCaps[0].GetIndexOfCap);
            ChangeHealthBuff(HealtBuffSystem.HealtBuffType.cap, buyableCaps[0].HealthBuff);
            ReplaceSelectedIcon(buyableCaps[0], ref capSelectedIcon);

            hairSwitcher.SwitchHair(buyable.GetIndexOfhair, Geekplay.Instance.PlayerData.isGenderMan);
            ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.hair, buyable.HealthBuff);


            buyButton.onClick.RemoveAllListeners();

            if (buyable.GetIsBuyed)
            {
                buySlapIcon.SetActive(false);
                buyDiamondIcon.SetActive(false);

                if (Geekplay.Instance.PlayerData.isGenderMan)
                {
                    if (Geekplay.Instance.PlayerData.currentManHair == buyable.GetIndexOfhair)
                    {
                        buyText.text = "Надето";
                    }
                    else
                    {
                        buyText.text = "Надеть";

                        buyButton.onClick.AddListener(() =>
                        {
                            hairSwitcher.ChangeHair(buyable.GetIndexOfhair, true);
                            buyText.text = "Надето";

                            ChangeHealthBuff(HealtBuffSystem.HealtBuffType.hair, buyable.HealthBuff);
                        });
                    }
                }
                else
                {
                    if (Geekplay.Instance.PlayerData.currentWomanHair == buyable.GetIndexOfhair)
                    {
                        buyText.text = "Надето";
                    }
                    else
                    {
                        buyText.text = "Надеть";

                        buyButton.onClick.AddListener(() =>
                        {
                            hairSwitcher.ChangeHair(buyable.GetIndexOfhair, false);
                            buyText.text = "Надето";

                            ChangeHealthBuff(HealtBuffSystem.HealtBuffType.hair, buyable.HealthBuff);
                        });
                    }
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
                        hairSwitcher.SwitchAndBuyHair(buyable.GetIndexOfhair, Geekplay.Instance.PlayerData.isGenderMan);
                        ChangeHealthBuff(HealtBuffSystem.HealtBuffType.hair, buyable.HealthBuff);

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

    private void CheckIsBuyedOrEquippedHair()
    {
        foreach (var buyable in manHairs)
        {
            if (Geekplay.Instance.PlayerData.BuyedManHairs.Contains(buyable.GetIndexOfhair))
            {
                buyable.Buyed();
            }
            if(Geekplay.Instance.PlayerData.currentManHair ==  buyable.GetIndexOfhair && Geekplay.Instance.PlayerData.isGenderMan)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.hair, buyable.HealthBuff);
                ReplaceSelectedIcon(buyable, ref hairManSelectedIcon);
            }
        }

        foreach (var buyable in womanHairs)
        {
            if (Geekplay.Instance.PlayerData.BuyedWomanHairs.Contains(buyable.GetIndexOfhair))
            {
                buyable.Buyed();
            }
            if (Geekplay.Instance.PlayerData.currentWomanHair == buyable.GetIndexOfhair && !Geekplay.Instance.PlayerData.isGenderMan)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.hair, buyable.HealthBuff);
                ReplaceSelectedIcon(buyable, ref hairWomanSelectedIcon);
            }
        }
    }

    private void ChangePlayerHair()
    {
        hairSwitcher.SwitchHair(Geekplay.Instance.PlayerData.currentWomanHair, false);
        hairSwitcher.SwitchHair(Geekplay.Instance.PlayerData.currentManHair, true);
    }
    #endregion

    #region Accessory

    private void AddEventForBuyableAccessory(BuyableAccessory buyable)
    {
        buyable.SubscribeOnClick(() =>
        {
            ReplaceSelectedIcon(buyable, ref accessorySelectedIcon);

            buyButton.gameObject.SetActive(true);
            accessorySwitcher.SwitchAccessory(buyable.GetIndexOfAccessory);
            ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.accessory, buyable.HealthBuff);

            buyButton.onClick.RemoveAllListeners();

            if (buyable.GetIsBuyed)
            {
                buySlapIcon.SetActive(false);
                buyDiamondIcon.SetActive(false);

                if (Geekplay.Instance.PlayerData.currentAccessory == buyable.GetIndexOfAccessory)
                {
                    buyText.text = "Надето";
                }
                else
                {
                    buyText.text = "Надеть";

                    buyButton.onClick.AddListener(() =>
                    {
                        accessorySwitcher.SaveAndSwitchAccessory(buyable.GetIndexOfAccessory);
                        buyText.text = "Надето";

                        ChangeHealthBuff(HealtBuffSystem.HealtBuffType.accessory, buyable.HealthBuff);
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
                        accessorySwitcher.SwitchAndBuyAccessory(buyable.GetIndexOfAccessory);
                        ChangeHealthBuff(HealtBuffSystem.HealtBuffType.accessory, buyable.HealthBuff);

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

    private void CheckIsBuyedOrEquippedAccessory()
    {
        foreach (var buyable in buyableAccessories)
        {
            if (Geekplay.Instance.PlayerData.BuyedAccessory.Contains(buyable.GetIndexOfAccessory))
            {
                buyable.Buyed();
            }
            if (Geekplay.Instance.PlayerData.currentAccessory == buyable.GetIndexOfAccessory)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.accessory, buyable.HealthBuff);

                ReplaceSelectedIcon(buyable, ref accessorySelectedIcon);
            }
        }
    }

    private void ChangePlayerAccessory()
    {
        accessorySwitcher.SwitchAccessory(Geekplay.Instance.PlayerData.currentAccessory);
    }
    #endregion

    #region Cap
    private void AddEventForBuyableCap(BuyableCap buyable)
    {
        buyable.SubscribeOnClick(() =>
        {
            ReplaceSelectedIcon(buyable, ref capSelectedIcon);

            buyButton.gameObject.SetActive(true);
            buyButton.onClick.RemoveAllListeners();

            hairSwitcher.ChangeHair(manHairs[0].GetIndexOfhair, true);
            hairSwitcher.ChangeHair(womanHairs[0].GetIndexOfhair, false);
            ReplaceSelectedIcon(manHairs[0], ref hairManSelectedIcon);
            ReplaceSelectedIcon(womanHairs[0], ref hairWomanSelectedIcon);

            if (Geekplay.Instance.PlayerData.isGenderMan)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.hair, manHairs[0].HealthBuff);
            }
            else
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.hair, womanHairs[0].HealthBuff);
            }

            capSwitcher.SwitchCap(buyable.GetIndexOfCap);
            ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.cap, buyable.HealthBuff);



            if (buyable.GetIsBuyed)
            {
                buySlapIcon.SetActive(false);
                buyDiamondIcon.SetActive(false);

                if (Geekplay.Instance.PlayerData.currentCap == buyable.GetIndexOfCap)
                {
                    buyText.text = "Надето";
                }
                else
                {
                    buyText.text = "Надеть";

                    buyButton.onClick.AddListener(() =>
                    {
                        capSwitcher.SaveAndSwitchCap(buyable.GetIndexOfCap);
                        buyText.text = "Надето";

                        ChangeHealthBuff(HealtBuffSystem.HealtBuffType.cap, buyable.HealthBuff);
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
                        capSwitcher.SwitchAndBuyCap(buyable.GetIndexOfCap);
                        ChangeHealthBuff(HealtBuffSystem.HealtBuffType.cap, buyable.HealthBuff);

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

    private void CheckIsBuyedOrEquippedCap()
    {
        foreach (var buyable in buyableCaps)
        {
            if (Geekplay.Instance.PlayerData.BuyedCaps.Contains(buyable.GetIndexOfCap))
            {
                buyable.Buyed();
            }
            if (Geekplay.Instance.PlayerData.currentCap == buyable.GetIndexOfCap)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.accessory, buyable.HealthBuff);

                ReplaceSelectedIcon(buyable, ref capSelectedIcon);
            }
        }
    }

    private void ChangePlayerCap()
    {
        capSwitcher.SwitchCap(Geekplay.Instance.PlayerData.currentCap);
    }
    #endregion

    private void ChangeHealthBuffText(HealtBuffSystem.HealtBuffType type, float buffPower)
    {
        float difference = healthBuffSystem.CompareBuff(type, buffPower);
        if (difference > 0)
            healthBuffText.text = "+" + difference.ToString();
        else if (difference < 0)
            healthBuffText.text = difference.ToString();
        else
            healthBuffText.text = "";
    }
    private void ChangeHealthBuff(HealtBuffSystem.HealtBuffType type, float buffPower)
    {
        healthBuffSystem.AddBuff(type, buffPower, currentHealthText);

        ChangeHealthBuffText(type, buffPower);
    }
    private void ReplaceSelectedIcon(Buyable buyable, ref GameObject selectedIcon)
    {
        RectTransform rect = buyable.GetComponent<RectTransform>();
        if (selectedIcon != null) Destroy(selectedIcon);

        selectedIcon = Instantiate(Resources.Load<GameObject>("SelectedIcon"), rect);
    }

    private void OnDisable()
    {
        ChangePlayerMaterial();
        ChangePlayerHair();
        ChangePlayerAccessory();
        ChangePlayerCap();
    }
}
public enum BodyPart
{
    Body,
    Leather,
    Hair,
    Arm,
    Leg,
    Foot
}