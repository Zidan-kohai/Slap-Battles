﻿
using UnityEngine;

public class Buyable : MonoBehaviour
{
    [SerializeField] private int cost;
    [SerializeField] private bool isBuyed;
    public bool GetIsBuyed {  get { return isBuyed; } }
    public int GetCost {  get { return cost; } }

    public bool TryBuy(int money)
    {
        if (money - cost < 0)
            return false;

        return true;
    }

    public void Buy(int money)
    {
        money -= cost;
        Geekplay.Instance.PlayerData.money = money;
        isBuyed = true;

        Geekplay.Instance.Save();
    }

    public void Buyed()
    {
        isBuyed = true;
    }
}