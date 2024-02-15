using CMF;
using JetBrains.Annotations;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour 
{
    private Action<int> changeMoney;
    private Action playerDeath;
    private Action<Enemy> enemyDeath;

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
        playerDeath += action;
    }

    public void UnsubscribeOnPlayerDeath(Action action)
    {
        playerDeath -= action;
    }

    public void InvokeActionsOnPlayerDeath()
    {
        playerDeath?.Invoke();
    }


    public void SubscribeOnEnemyDeath(Action<Enemy> action)
    {
        enemyDeath += action;
    }

    public void UnsubscribeOnEnemyDeath(Action<Enemy> action)
    {
        enemyDeath -= action;
    }

    public void InvokeActionsOnEnemyDeath(Enemy enemyObject)
    {
        enemyDeath?.Invoke(enemyObject);
    }
}
