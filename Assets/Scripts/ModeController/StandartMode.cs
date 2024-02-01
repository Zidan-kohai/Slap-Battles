using System;
using System.Collections.Generic;
using UnityEngine;

public class StandartMode : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private List<Enemy> enemies;

    [SerializeField] private List<Transform> spawnPoints;

    public bool flag;
    private void Start()
    {
        ArrangeTransforms();
        TurnOnGameObjects();
    }

    private void ArrangeTransforms()
    {
        player.transform.position = GetRandomPosition();

        for (int i = 0; i < enemies.Count; i++)
        {
            enemies[i].transform.position = GetRandomPosition();
        }
    }

    public void Update()
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
                enemies[i].transform.position = GetRandomPosition();
                enemies[i].Revive();
                enemies[i].gameObject.SetActive(true);
                return;
            }
        }
    }

    private Vector3 GetRandomPosition()
    {
        return spawnPoints[UnityEngine.Random.Range(0, spawnPoints.Count)].position;
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
