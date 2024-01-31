using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//TO DO Refactoring
public class SkineController : MonoBehaviour
{
    [Header("Material of Body Part"), Space(5)]
    [SerializeField] private Material bodyMaterial;
    [SerializeField] private Material headMaterial;
    [SerializeField] private Material hairMaterial;
    [SerializeField] private Material armMaterial;
    [SerializeField] private Material legMaterial;
    [SerializeField] private Material footMaterial;
    [Space(10)]

    [SerializeField] private List<BuyableColor> hairColors;
    [SerializeField] private List<BuyableColor> headColors;
    [SerializeField] private List<BuyableColor> bodyColors;
    [SerializeField] private List<BuyableColor> armColors;
    [SerializeField] private List<BuyableColor> legColors;
    [SerializeField] private List<BuyableColor> footColors;

    [Space(10), Header("Gender")]
    [SerializeField] private Button manButton;
    [SerializeField] private Button womanButton;

    [Space(10), Header("Buy Button")]
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI buyText;

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



    private void Start()
    {
        CheckIsBuyedOrEquippedColor();
        CheckIsBuyedOrEquippedHair();
        CheckIsBuyedAccessory();
        CheckIsBuyedCap();

        #region Subscribe On Color Buy Events
        foreach (var item in hairColors)
        {
            AddEventForBuyableColor(item);
        }
        foreach (var item in headColors)
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
        foreach (var item in armColors)
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
        });
    }

    #region ColorSwitching
    private void AddEventForBuyableColor(BuyableColor buyable)
    {
        Debug.Log(buyable.gameObject.name + " " + buyable.GetBodyType);
        buyable.SubscribeOnClick(() =>
        {
            Material material = null;
            switch (buyable.GetBodyType)
            {
                case BodyPart.Head:
                    material = headMaterial;
                    break;
                case BodyPart.Hair:
                    material = hairMaterial;
                    break;
                case BodyPart.Body:
                    material = bodyMaterial;
                    break;
                case BodyPart.Arm:
                    material = armMaterial;
                    break;
                case BodyPart.Leg:
                    material = legMaterial;
                    break;
                case BodyPart.Foot:
                    material = footMaterial;
                    break;
            }

            buyButton.onClick.RemoveAllListeners();
            if(buyable.GetIsBuyed)
            {
                buyButton.onClick.AddListener(() =>
                {
                    ChangeLastColor(material, buyable);

                    buyText.text = "Надето";
                });

                bool isEquiped = false;

                ChangeColorInShop(buyable);

                switch (buyable.GetBodyType)
                {
                    case BodyPart.Head:
                        if (Geekplay.Instance.PlayerData.CurrentHeadColorIndex == buyable.indexOfColor)
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
                    case BodyPart.Arm:
                        if (Geekplay.Instance.PlayerData.CurrentArmColorIndex == buyable.indexOfColor)
                            isEquiped = true;
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.armColor, buyable.HealthBuff);
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
                    case BodyPart.Head:
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.HeadColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Hair:
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.HairColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Body:
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.BodyColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Arm:
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.armColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Leg:
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.legColor, buyable.HealthBuff);
                        break;
                    case BodyPart.Foot:
                        ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.footColor, buyable.HealthBuff);
                        break;
                }

                if (buyable.TryBuy(Geekplay.Instance.PlayerData.money))
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        BuyAndChangeLastColor(material, buyable);

                        buyText.text = "Надето";

                    });

                    ChangeColorInShop(buyable);
                }
                else
                {
                    ChangeColorInShop(buyable);

                    buyButton.onClick.AddListener(() =>
                    {
                        Debug.Log("Don`t have money");
                    });
                }

                buyText.text = $"купить за {buyable.GetCost}";
            }
        });
    }

    private void ChangeColorInShop(BuyableColor buyable)
    {
        switch (buyable.GetBodyType)
        {
            case BodyPart.Head:
                headMaterial.color = buyable.GetColor;
                break;
            case BodyPart.Hair:
                hairMaterial.color = buyable.GetColor;
                break;
            case BodyPart.Body:
                bodyMaterial.color = buyable.GetColor;
                break;
            case BodyPart.Arm:
                armMaterial.color = buyable.GetColor;
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
            case BodyPart.Head:
                Geekplay.Instance.PlayerData.CurrentHeadColorIndex = buyable.indexOfColor;
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
            case BodyPart.Arm:
                Geekplay.Instance.PlayerData.CurrentArmColorIndex = buyable.indexOfColor;
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.armColor, buyable.HealthBuff);
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
            case BodyPart.Head:
                Geekplay.Instance.PlayerData.CurrentHeadColorIndex = buyable.indexOfColor;
                Geekplay.Instance.PlayerData.BuyedHeadColors.Add(buyable.indexOfColor);
                break;
            case BodyPart.Hair:
                Geekplay.Instance.PlayerData.CurrentHairColorIndex = buyable.indexOfColor;
                Geekplay.Instance.PlayerData.BuyedHairColors.Add(buyable.indexOfColor);
                break;
            case BodyPart.Body:
                Geekplay.Instance.PlayerData.CurrentBodyColorIndex = buyable.indexOfColor;
                Geekplay.Instance.PlayerData.BuyedBodyColors.Add(buyable.indexOfColor);
                break;
            case BodyPart.Arm:
                Geekplay.Instance.PlayerData.CurrentArmColorIndex = buyable.indexOfColor;
                Geekplay.Instance.PlayerData.BuyedArmColors.Add(buyable.indexOfColor);
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

        buyable.Buy(Geekplay.Instance.PlayerData.money);
        ChangeLastColor(material, buyable);
    }

    private void CheckIsBuyedOrEquippedColor()
    {
        foreach(var buyable in headColors)
        {
            if(Geekplay.Instance.PlayerData.BuyedHeadColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
            }
            if(Geekplay.Instance.PlayerData.CurrentHeadColorIndex == buyable.indexOfColor)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.HeadColor,buyable.HealthBuff);
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
            }
        }

        foreach (var buyable in armColors)
        {
            if (Geekplay.Instance.PlayerData.BuyedArmColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
            }
            if (Geekplay.Instance.PlayerData.CurrentArmColorIndex == buyable.indexOfColor)
            {
                ChangeHealthBuff(HealtBuffSystem.HealtBuffType.armColor, buyable.HealthBuff);
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
            }
        }
    }

    public void ChangeColor(BodyPart bodyPart, Color color)
    {
        switch (bodyPart)
        {
            case BodyPart.Head:
                headMaterial.color = color;
                break;
            case BodyPart.Body:
                bodyMaterial.color = color;
                break;
            case BodyPart.Arm:
                armMaterial.color = color;
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
        foreach(var item in headColors)
        {
            if (item.indexOfColor == Geekplay.Instance.PlayerData.CurrentHeadColorIndex)
                headMaterial.color = item.GetColor;
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
        foreach (var item in armColors)
        {
            if (item.indexOfColor == Geekplay.Instance.PlayerData.CurrentArmColorIndex)
                armMaterial.color = item.GetColor;
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
            hairSwitcher.SwitchHair(buyable.GetIndexOfhair, Geekplay.Instance.PlayerData.isGenderMan);
            ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.hair, buyable.HealthBuff);

            buyButton.onClick.RemoveAllListeners();

            if (buyable.GetIsBuyed)
            {

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

                if (buyable.TryBuy(Geekplay.Instance.PlayerData.money))
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        hairSwitcher.SwitchAndBuyHair(buyable.GetIndexOfhair, Geekplay.Instance.PlayerData.isGenderMan);
                        ChangeHealthBuff(HealtBuffSystem.HealtBuffType.hair, buyable.HealthBuff);

                        buyable.Buy(Geekplay.Instance.PlayerData.money);
                        buyText.text = "Надето";
                    });
                }
                else
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        Debug.Log("You don`t Have money");
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
            accessorySwitcher.SwitchAccessory(buyable.GetIndexOfAccessory);
            ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.accessory, buyable.HealthBuff);

            buyButton.onClick.RemoveAllListeners();

            if (buyable.GetIsBuyed)
            {
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

                if (buyable.TryBuy(Geekplay.Instance.PlayerData.money))
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        accessorySwitcher.SwitchAndBuyAccessory(buyable.GetIndexOfAccessory);
                        ChangeHealthBuff(HealtBuffSystem.HealtBuffType.accessory, buyable.HealthBuff);

                        buyable.Buy(Geekplay.Instance.PlayerData.money);
                        buyText.text = "Надето";
                    });
                }
                else
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        Debug.Log("You don`t Have money");
                    });
                }
            }
        });
    }

    private void CheckIsBuyedAccessory()
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
            ChangeHealthBuffText(HealtBuffSystem.HealtBuffType.cap, buyable.HealthBuff);
            capSwitcher.SwitchCap(buyable.GetIndexOfCap);
            buyButton.onClick.RemoveAllListeners();

            if (buyable.GetIsBuyed)
            {
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

                if (buyable.TryBuy(Geekplay.Instance.PlayerData.money))
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        capSwitcher.SwitchAndBuyCap(buyable.GetIndexOfCap);
                        ChangeHealthBuff(HealtBuffSystem.HealtBuffType.cap, buyable.HealthBuff);

                        buyable.Buy(Geekplay.Instance.PlayerData.money);
                        buyText.text = "Надето";
                    });
                }
                else
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        Debug.Log("You don`t Have money");
                    });
                }
            }
        });
    }

    private void CheckIsBuyedCap()
    {
        foreach (var buyable in buyableCaps)
        {
            if (Geekplay.Instance.PlayerData.BuyedCaps.Contains(buyable.GetIndexOfCap))
            {
                buyable.Buyed();
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
    Head,
    Hair,
    Arm,
    Leg,
    Foot
}