using JetBrains.Annotations;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour 
{
    private Action<int> changeMoney;

    public void SubscribeOnChangeMoney(Action<int> action)
    {
        changeMoney += action;
    }

    public void UnsubscribeOnChangeMoney(Action<int> action)
    {
        changeMoney -= action;
    }

    public void InvokeActionsOnChangeMoney(int money)
    {
        changeMoney?.Invoke(money);
    }
}
