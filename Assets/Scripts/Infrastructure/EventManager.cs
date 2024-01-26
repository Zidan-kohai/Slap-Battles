using JetBrains.Annotations;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour 
{
    private Action<int> changeMoney;

    private Action PlayerDeath;
    
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


    public void SubscribeOnPlayerDeath(Action action)
    {
        PlayerDeath += action;
    }

    public void UnsubscribeOnPlayerDeath(Action action)
    {
        PlayerDeath -= action;
    }

    public void InvokeActionsOnPlayerDeath()
    {
        PlayerDeath?.Invoke();
    }
}
