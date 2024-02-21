using CMF;
using JetBrains.Annotations;
using System;
using UnityEngine;

public class EventManager : MonoBehaviour 
{
    private Action<int> changeMoney;
    private Action<int,int> playerDeath;
    private Action playerRevive;
    private Action<Enemy> enemyDeath;

    #region ChangeMoney

    public void SubscribeOnChangeMoney(Action<int> action)
    {
        changeMoney += action;
    }
    public void UnsubscribeOnChangeMoney(Action<int> action)
    {
        changeMoney -= action;
    }
    public void InvokeChangeMoneyEvents(int money)
    {
        changeMoney?.Invoke(money);
    }

    #endregion

    #region PlayerDeath

    public void SubscribeOnPlayerDeath(Action<int, int> action)
    {
        playerDeath += action;
    }
    public void UnsubscribeOnPlayerDeath(Action<int, int> action)
    {
        playerDeath -= action;
    }
    public void InvokePlayerDeathEvents(int deadCount, int stolen)
    {
        playerDeath?.Invoke(deadCount, stolen);
    }

    #endregion

    #region EnemyDeath

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

    #endregion

    #region PlayerRevive

    public void SubscribeOnPlayerRevive(Action action)
    {
        playerRevive += action;
    }
    public void UnsubscribeOnPlayerRevive(Action action)
    {
        playerRevive -= action;
    }
    public void InvokePlayerReviveEvents()
    {
        playerRevive?.Invoke();
    }

    #endregion
}
