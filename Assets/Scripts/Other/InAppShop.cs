using UnityEngine;

public class InAppShop : MonoBehaviour
{
    [SerializeField] private HubEventManager eventManager;
    
    private void Start()
    {
        Geekplay.Instance.SubscribeOnPurshace("ItemSlap1", ItemSlap1);
        Geekplay.Instance.SubscribeOnPurshace("ItemSlap2", ItemSlap2);
        Geekplay.Instance.SubscribeOnPurshace("ItemSlap3", ItemSlap3);
        Geekplay.Instance.SubscribeOnPurshace("ItemSlap4", ItemSlap4);
        Geekplay.Instance.SubscribeOnPurshace("ItemSlap5", ItemSlap5);
        Geekplay.Instance.SubscribeOnPurshace("ItemDiamond1", ItemDiamond1);
        Geekplay.Instance.SubscribeOnPurshace("ItemDiamond2", ItemDiamond2);
        Geekplay.Instance.SubscribeOnPurshace("ItemDiamond3", ItemDiamond3);
        Geekplay.Instance.SubscribeOnPurshace("ItemDiamond4", ItemDiamond4);
        Geekplay.Instance.SubscribeOnPurshace("ItemDiamond5", ItemDiamond5);
        Geekplay.Instance.SubscribeOnPurshace("ItemSlapAndDiamond", ItemSlapAndDiamond);
    }
    public void RealBuy(string tag)
    {
        Geekplay.Instance.RealBuyItem(tag);
    }

    public void ItemSlap1()
    {
        Geekplay.Instance.PlayerData.money += 100;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
    public void ItemSlap2()
    {
        Geekplay.Instance.PlayerData.money += 1000;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
    public void ItemSlap3()
    {
        Geekplay.Instance.PlayerData.money += 5000;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
    public void ItemSlap4()
    {
        Geekplay.Instance.PlayerData.money += 10000;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
    public void ItemSlap5()
    {
        Geekplay.Instance.PlayerData.money += 25000;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
    public void ItemDiamond1()
    {
        Geekplay.Instance.PlayerData.DiamondMoney += 10;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
    public void ItemDiamond2()
    {
        Geekplay.Instance.PlayerData.DiamondMoney += 50;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
    public void ItemDiamond3()
    {
        Geekplay.Instance.PlayerData.DiamondMoney += 100;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
    public void ItemDiamond4()
    {
        Geekplay.Instance.PlayerData.DiamondMoney += 150;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
    public void ItemDiamond5()
    {
        Geekplay.Instance.PlayerData.DiamondMoney += 250;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
    public void ItemSlapAndDiamond()
    {
        Geekplay.Instance.PlayerData.DiamondMoney += 200;
        Geekplay.Instance.PlayerData.money += 10000;
        eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
    }
}
