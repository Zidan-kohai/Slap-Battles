using JetBrains.Annotations;
using System.Collections.Generic;
using UnityEngine;

public class BattleRoyalModeController : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;

    [SerializeField] private Player player;
    [SerializeField] private List<Enemy> enemies;
    [SerializeField] private List<Transform> spawnPoints;


    [SerializeField] private int LivedEnemyCount;
    public int EnemyCount {  get => enemies.Count + 1; }

    private void Start()
    {
        ArrangeTransforms();
        TurnOnGameObjects();

        eventManager.SubscribeOnEnemyDeath(OnEnemyDeath);
    }
   
    private void OnEnemyDeath(Enemy enemyObj)
    {
        enemies.Remove(enemyObj);
    }

    private void ArrangeTransforms()
    {
        player.transform.position = GetRandomPosition();

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.position = GetRandomPosition();
        }
    }
    private Vector3 GetRandomPosition()
    {
        return spawnPoints[Random.Range(0, spawnPoints.Count)].position;
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
