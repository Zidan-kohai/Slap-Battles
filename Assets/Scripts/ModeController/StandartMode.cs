using CMF;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandartMode : MonoBehaviour
{
    [SerializeField] private AdvancedWalkerController walkerController;
    [SerializeField] private Player player;
    [SerializeField] private List<Enemy> enemies;

    [SerializeField] private List<Transform> enemySpawnPoints;
    [SerializeField] private List<Transform> playerSpawnPoints;

    [SerializeField] private EventManager eventManager;

    public bool flag;

    [SerializeField] protected GameObject LeftColumn;
    [SerializeField] protected GameObject RightColumn;

    public int enemyToSpawn;
    protected void Start()
    {
        ArrangeTransforms();
        TurnOnGameObjects();

        if(Geekplay.Instance.currentMode != Modes.Boss)
            ArrangeTargetForEnemy();

        eventManager.SubscribeOnPlayerRevive(OnPlayerRevive);


        if (Geekplay.Instance.mobile)
        {
            LeftColumn.SetActive(false);
            RightColumn.SetActive(false);
            enemyToSpawn = 10;
        }
        else
        {
            enemyToSpawn = enemies.Count;
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
    private void ArrangeTargetForEnemy()
    {
        for(int i = 0; i < enemyToSpawn; i++)
        {
            float minDistance = Mathf.Infinity;
            IHealthObject target = null;
            float distance = 0;

            distance = (enemies[i].transform.position - player.transform.position).magnitude;

            if (minDistance > distance)
            {
                minDistance = distance;
                target = player;
            }

            for (int j = 0; j < enemyToSpawn; j++)
            {
                if (i == j) continue;

                distance = (enemies[i].transform.position - enemies[j].transform.position).magnitude;

                if (minDistance > distance)
                {
                    minDistance = distance;
                    target = enemies[j];
                }

            }


            enemies[i].ChangeEnemy(target);
        }
    }

    private void ArrangeTransforms()
    {
        player.transform.position = GetRandomPositionForPlayer();
        

        for (int i = 0; i < enemyToSpawn; i++)
        {
            enemies[i].transform.position = GetRandomPositionForEnemy();
            enemies[i].Revive();
        }
    }

    protected void Update()
    {
        int rand = UnityEngine.Random.Range(0, 1000);


        if (rand > 1 && rand < 50 || flag)
        {
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        flag = false;

        for (int i = 0; i < enemyToSpawn; i++)
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

        for (int i = 0; i < enemyToSpawn; i++)
        {
            enemies[i].gameObject.SetActive(true);
        }
    }
}
