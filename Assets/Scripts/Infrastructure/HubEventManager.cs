using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HubEventManager : MonoBehaviour
{
    private Action<int, int> changeMoney;

    public void SubscribeOnChangeMoney(Action<int, int> action)
    {
        changeMoney += action;
    }
    public void UnsubscribeOnChangeMoney(Action<int, int> action)
    {
        changeMoney -= action;
    }
    public void InvokeChangeMoneyEvents(int slapCount, int diamondCount)
    {
        changeMoney?.Invoke(slapCount, diamondCount);
    }
}
