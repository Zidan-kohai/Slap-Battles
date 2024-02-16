using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleRoyalModeUIController : MonoBehaviour
{
    [SerializeField] private EventManager eventManager;
    [SerializeField] private BattleRoyalModeController modeController;

    [SerializeField] private TextMeshProUGUI enemyCountText;
    [SerializeField] private TextMeshProUGUI placeText;
    [SerializeField] private TextMeshProUGUI placeSlapRewardText;
    [SerializeField] private TextMeshProUGUI placeDiamondRewardText;

    [SerializeField] private List<Reward> placeRewards;

    private void Start()
    {
        enemyCountText.text = modeController.EnemyCount.ToString();
        eventManager.SubscribeOnEnemyDeath(OnEnemyDeath);
        eventManager.SubscribeOnPlayerDeath(OnPlayerDeath);
    }

    private void OnPlayerDeath(int deadCount)
    {
        int place = Convert.ToInt32(enemyCountText.text);

        if (deadCount < 2)
        {
            placeText.text = enemyCountText.text;
            placeSlapRewardText.text = placeRewards[place].SlapCount.ToString();
            placeDiamondRewardText.text = placeRewards[place].DiamondCount.ToString();
        }
    }

    private void OnEnemyDeath(Enemy  enemyObj)
    {
        enemyCountText.text = modeController.EnemyCount.ToString();
    }
}

[Serializable]
public class Reward
{
    public int SlapCount;
    public int DiamondCount;
}
