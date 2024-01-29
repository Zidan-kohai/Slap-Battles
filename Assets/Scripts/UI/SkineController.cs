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
    [SerializeField] private Material hearMaterial;
    [SerializeField] private Material armMaterial;
    [SerializeField] private Material legMaterial;
    [SerializeField] private Material footMaterial;
    [Space(10)]

    [SerializeField] private List<BuyableColor> hearColors;
    [SerializeField] private List<BuyableColor> headColors;
    [SerializeField] private List<BuyableColor> bodyColors;
    [SerializeField] private List<BuyableColor> armColors;
    [SerializeField] private List<BuyableColor> legColors;
    [SerializeField] private List<BuyableColor> footColors;

    [Space(10), Header("Buy Button")]
    [SerializeField] private Button buyButton;
    [SerializeField] private TextMeshProUGUI buyText;

    private void Start()
    {
        CheckIsBuyed();

        foreach (var item in hearColors)
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
    }
    private void AddEventForBuyableColor(BuyableColor buyable)
    {
        buyable.SubscribeOnClick(() =>
        {
            buyButton.onClick.RemoveAllListeners();
            if(buyable.GetIsBuyed)
            {
                buyButton.onClick.AddListener(() =>
                {
                    ChangeLastColor(headMaterial, buyable);

                    buyText.text = "Надето";
                });

                bool isEquiped = false;

                ChangeColorInShop(buyable);

                switch (buyable.GetBodyType)
                {
                    case BodyPart.Head:
                        if (Geekplay.Instance.PlayerData.CurrentHeadColorIndex == buyable.indexOfColor)
                            isEquiped = true;
                        break;
                    case BodyPart.Hear:
                        if (Geekplay.Instance.PlayerData.CurrentHearColorIndex == buyable.indexOfColor)
                            isEquiped = true;
                        break;
                    case BodyPart.Body:
                        if (Geekplay.Instance.PlayerData.CurrentBodyColorIndex == buyable.indexOfColor)
                            isEquiped = true;
                        break;
                    case BodyPart.Arm:
                        if (Geekplay.Instance.PlayerData.CurrentArmColorIndex == buyable.indexOfColor)
                            isEquiped = true;
                        break;
                    case BodyPart.Leg:
                        if (Geekplay.Instance.PlayerData.CurrentLegColorIndex == buyable.indexOfColor)
                            isEquiped = true;
                        break;
                    case BodyPart.Foot:
                        if (Geekplay.Instance.PlayerData.CurrentFootColorIndex == buyable.indexOfColor)
                            isEquiped = true;
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
                if(buyable.TryBuy(Geekplay.Instance.PlayerData.money))
                {
                    buyButton.onClick.AddListener(() =>
                    {
                        BuyAndChangeLastColor(headMaterial, buyable);

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
            case BodyPart.Hear:
                hearMaterial.color = buyable.GetColor;
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
                break;
            case BodyPart.Hear:
                Geekplay.Instance.PlayerData.CurrentHearColorIndex = buyable.indexOfColor;
                break;
            case BodyPart.Body:
                Geekplay.Instance.PlayerData.CurrentBodyColorIndex = buyable.indexOfColor;
                break;
            case BodyPart.Arm:
                Geekplay.Instance.PlayerData.CurrentArmColorIndex = buyable.indexOfColor;
                break;
            case BodyPart.Leg:
                Geekplay.Instance.PlayerData.CurrentLegColorIndex = buyable.indexOfColor;
                break;
            case BodyPart.Foot:
                Geekplay.Instance.PlayerData.CurrentFootColorIndex = buyable.indexOfColor;
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
            case BodyPart.Hear:
                Geekplay.Instance.PlayerData.CurrentHearColorIndex = buyable.indexOfColor;
                Geekplay.Instance.PlayerData.BuyedHearColors.Add(buyable.indexOfColor);
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

    private void CheckIsBuyed()
    {
        foreach(var buyable in headColors)
        {
            if(Geekplay.Instance.PlayerData.BuyedHearColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
            }
        }

        foreach (var buyable in headColors)
        {
            if (Geekplay.Instance.PlayerData.BuyedHeadColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
            }
        }

        foreach (var buyable in bodyColors)
        {
            if (Geekplay.Instance.PlayerData.BuyedBodyColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
            }
        }

        foreach (var buyable in legColors)
        {
            if (Geekplay.Instance.PlayerData.BuyedLegColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
            }
        }

        foreach (var buyable in footColors)
        {
            if (Geekplay.Instance.PlayerData.BuyedFootColors.Contains(buyable.indexOfColor))
            {
                buyable.Buyed();
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
        foreach (var item in hearColors)
        {
            if (item.indexOfColor == Geekplay.Instance.PlayerData.CurrentHearColorIndex)
                hearMaterial.color = item.GetColor;
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

    private void OnDisable()
    {
        ChangePlayerMaterial();
    }
}
public enum BodyPart
{
    Body,
    Head,
    Hear,
    Arm,
    Leg,
    Foot
}