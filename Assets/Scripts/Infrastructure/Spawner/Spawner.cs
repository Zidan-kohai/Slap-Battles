using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] protected List<PortalOptions> portalsOption;

    [SerializeField] protected List<Enemy> enemies;

    [SerializeField] protected Player player;

    [Header("Spawn Options")]
    [SerializeField] protected List<Transform> PortalSpawnPoints;
    [SerializeField] protected List<Transform> EnemySpawnPoints;
    [SerializeField] protected List<Transform> PlayerSpawnPoints;

    private void Start()
    {
        PortalSpawn();

        EnemySpawn();

        PlayerSpawn();
    }

    protected virtual void PlayerSpawn()
    {
        Instantiate(player, PlayerSpawnPoints[UnityEngine.Random.Range(0, PlayerSpawnPoints.Count)].position, Quaternion.identity);
    }

    protected virtual void EnemySpawn()
    {
        foreach (var enemy in enemies)
        {
            Instantiate(enemy, EnemySpawnPoints[UnityEngine.Random.Range(0, PlayerSpawnPoints.Count)].position, Quaternion.identity);
        }
    }

    protected virtual void PortalSpawn()
    {
        foreach (var portalOptions in portalsOption)
        {
            Portal portal = Instantiate(portalOptions.Portal, portalOptions.SpawnPosition.position, portalOptions.SpawnPosition.localRotation);
            portal.SceneIndex = portalOptions.SceneIndex;
        }
    }

    [Serializable]
    public class PortalOptions
    {
        public Portal Portal;
        public int SceneIndex;
        public Transform SpawnPosition;
    }
}


