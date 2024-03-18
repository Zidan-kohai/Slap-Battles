using CMF;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoyalModeController : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;

    [SerializeField] private AdvancedWalkerController walkerController;
    [SerializeField] private Player player;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<Transform> enemySpawnPoints;
    [SerializeField] private List<Transform> playerSpawnPoints;


    [SerializeField] private int LivedEnemyCount;
    public int EnemyCount {  get => enemies.Count + 1; }


    [SerializeField] public List<Reward> placeRewards;


    public Action Win;


    [SerializeField] protected GameObject LeftColumn;
    [SerializeField] protected GameObject RightColumn;
    private void Start()
    {
        if(Geekplay.Instance.mobile)
        {
            enemies.RemoveRange(10, 5);
        }

        ArrangeTransforms();
        TurnOnGameObjects();

        eventManager.SubscribeOnEnemyDeath(OnEnemyDeath);
        eventManager.SubscribeOnPlayerRevive(OnPlayerRevive);

        if (Geekplay.Instance.mobile)
        {
            LeftColumn.SetActive(false);
            RightColumn.SetActive(false);
        }
    }

    private void OnPlayerRevive()
    {
        StartCoroutine(ReviveCoroutine());
    }

    private IEnumerator ReviveCoroutine()
    {
        walkerController.enabled = false;
        yield return new WaitForSeconds(0.6f);

        walkerController.transform.position = GetRandomPositionForPlayer();

        yield return new WaitForSeconds(0.3f);
        walkerController.enabled = true;
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