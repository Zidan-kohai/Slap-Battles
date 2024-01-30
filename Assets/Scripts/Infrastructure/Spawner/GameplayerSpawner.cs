using UnityEngine;

public class GameplayerSpawner : Spawner
{
    [SerializeField] private EventManager eventManager;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        PortalSpawn();

        EnemySpawn();

        //PlayerSpawn();
    }

    protected override void PlayerSpawn()
    {
        Player player = Instantiate(this.player, PlayerSpawnPoints[Random.Range(0, PlayerSpawnPoints.Count)].position, Quaternion.identity);

        PlayerAttack playerAttack = player.GetComponent<PlayerAttack>();
        playerAttack.enabled = true;

        playerAttack.ChangeEventManager(eventManager);
        
    }

    protected override void EnemySpawn()
    {
        base.EnemySpawn();
    }

    protected override void PortalSpawn()
    {
        base.PortalSpawn(); 
    }
}
