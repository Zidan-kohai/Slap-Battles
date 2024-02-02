using TMPro;
using UnityEngine;

public class BattleRoyalModeUIController : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private BattleRoyalModeController modeController;

    [SerializeField] private TextMeshProUGUI enemyCountText;


    private void Start()
    {
        enemyCountText.text = modeController.EnemyCount.ToString();

        eventManager.SubscribeOnEnemyDeath(OnEnemyDeath);
    }

    private void OnEnemyDeath(Enemy  enemyObj)
    {
        enemyCountText.text = modeController.EnemyCount.ToString();
    }
}
