using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StandartMode : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private List<Enemy> enemies;

    [SerializeField] private List<Transform> enemySpawnPoints;
    [SerializeField] private List<Transform> playerSpawnPoints;

    [SerializeField] private EventManager eventManager;

    public bool flag;
    protected void Start()
    {
        ArrangeTransforms();
        TurnOnGameObjects();

        eventManager.SubscribeOnBossDeath(OnBossDead);
    }

    private void ArrangeTransforms()
    {
        player.transform.position = GetRandomPositionForPlayer();

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.position = GetRandomPositionForEnemy();
            enemies[i].Revive();
        }
    }

    protected void Update()
    {
        int rand = UnityEngine.Random.Range(0, 1000);


        if (rand == 1 || flag)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        flag = false;
        for (int i = 0; i < enemies.Count; i++)
        {
            if (!enemies[i].gameObject.activeSelf)
            {
                enemies[i].transform.position = GetRandomPositionForEnemy();
                enemies[i].Revive();
                return;
            }
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

    private void OnBossDead()
    {
        SceneLoader sceneLoader = new SceneLoader(this);
        sceneLoader.LoadScene(0);
    }
}
