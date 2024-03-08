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

    //To Do Correct  
    [SerializeField] private BattleModeLosePanel winPanel;
    [SerializeField] private TextMeshProUGUI winPanelHeaderText;


    private void Start()
    {
        enemyCountText.text = modeController.EnemyCount.ToString();
        eventManager.SubscribeOnEnemyDeath(OnEnemyDeath);
        eventManager.SubscribeOnPlayerDeath(OnPlayerDeath);
        modeController.Win += OnPlayerWin;
    }

    private void OnPlayerDeath(int deadCount, int stolenSlaps)
    {
        int place = modeController.EnemyCount - 1;

        if (deadCount < 2)
        {
            placeText.text = enemyCountText.text;
            placeSlapRewardText.text = (Convert.ToInt32(placeSlapRewardText.text) + modeController.placeRewards[place].SlapCount).ToString();
            placeDiamondRewardText.text = (Convert.ToInt32(placeDiamondRewardText.text) + modeController.placeRewards[place].DiamondCount).ToString();
        }
    }

    private void OnEnemyDeath(Enemy  enemyObj)
    {
        enemyCountText.text = modeController.EnemyCount.ToString();
    }

    private void OnPlayerWin()
    {
        int place = 1;
        winPanel.DisableRewardButton();
        
        winPanel.gameObject.SetActive(true);
        
        winPanelHeaderText.text = "Победа";

        placeText.text = place.ToString();
        placeSlapRewardText.text = modeController.placeRewards[place - 1].SlapCount.ToString();
        placeDiamondRewardText.text = modeController.placeRewards[place - 1].DiamondCount.ToString();
    }
}
