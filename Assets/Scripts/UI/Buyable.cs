
using UnityEngine;

public class Buyable : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private bool isBuyed;

    public TypeOfCost costType;

    public bool GetIsBuyed {  get { return isBuyed; } }
    public int GetCost {  get { return cost; } }

    public bool TryBuy()
    {
        if (costType == TypeOfCost.money)
        {
            if (Geekplay.Instance.PlayerData.money - cost < 0)
                return false;
        }
        else if(costType == TypeOfCost.diamond)
        {
            if (Geekplay.Instance.PlayerData.DiamondMoney - cost < 0)
                return false;
        }

        return true;
    }

    public void Buy()
    {
        if (costType == TypeOfCost.money)
        {
            Geekplay.Instance.PlayerData.money -= cost;
        }
        else if (costType == TypeOfCost.diamond)
        {
            Geekplay.Instance.PlayerData.DiamondMoney -= cost;
        }

        isBuyed = true;
        Geekplay.Instance.Save();
    }

    public void Buyed()
    {
        isBuyed = true;
    }

    public enum TypeOfCost
    {
        money,
        diamond,
    }
}