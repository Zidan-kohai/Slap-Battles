using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoyalModeController : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;

    [SerializeField] private Player player;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<Transform> enemySpawnPoints;
    [SerializeField] private List<Transform> playerSpawnPoints;


    [SerializeField] private int LivedEnemyCount;
    public int EnemyCount {  get => enemies.Count + 1; }


    [SerializeField] public List<Reward> placeRewards;


    public Action Win;
    
    private void Start()
    {
        ArrangeTransforms();
        TurnOnGameObjects();

        eventManager.SubscribeOnEnemyDeath(OnEnemyDeath);
        eventManager.SubscribeOnPlayerRevive(OnPlayerRevive);
    }

    private void OnPlayerRevive()
    {
        player.transform.position = GetRandomPositionForPlayer();
    }

    private void OnEnemyDeath(Enemy enemyObj)
    {
        enemies.Remove(enemyObj);

        if(enemies.Count == 0)
        {
            Win?.Invoke();
        }
    }

    private void ArrangeTransforms()
    {
        player.transform.position = GetRandomPositionForPlayer();

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.position = GetRandomPositionForEnemy();
        }
    }
    private Vector3 GetRandomPositionForEnemy()
    {
        return enemySpawnPoints[UnityEngine.Random.Range(0, enemySpawnPoints.Count)].position;
    }

    private Vector3 GetRandomPositionForPlayer()
    {
        return playerSpawnPoints[UnityEngine.Random.Range(0, playerSpawnPoints.Count)].position;
    }

    private void TurnOnGameObjects()
    {
        player.gameObject.SetActive(true);

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].gameObject.SetActive(true);
        }
    }
}


[Serializable]
public class Reward
{
    public int SlapCount;
    public int DiamondCount;
}