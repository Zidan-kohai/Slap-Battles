using UnityEngine;

public class HubSpawner : Spawner
{
    private void Start()
    {
        PortalSpawn();

        EnemySpawn();

        PlayerSpawn();
    }

    protected override void PlayerSpawn()
    {
        Instantiate(player, PlayerSpawnPoints[Random.Range(0, PlayerSpawnPoints.Count)].position, Quaternion.identity);
    }

    protected override void EnemySpawn()
    {
        foreach (var enemy in enemies)
        {
            Instantiate(enemy, EnemySpawnPoints[Random.Range(0, PlayerSpawnPoints.Count)].position, Quaternion.identity);
        }
    }

    protected override void PortalSpawn()
    {
        base.PortalSpawn();
    }
}
