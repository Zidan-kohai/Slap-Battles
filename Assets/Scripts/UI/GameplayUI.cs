using System;
using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private LosePanel LosePanel;
    private void Start()
    {
        eventManager.SubscribeOnChangeMoney(OnChangeMoney);
        eventManager.SubscribeOnPlayerDeath(OnPlayerDeath);
        eventManager.SubscribeOnPlayerRevive(OnRevive);

        moneyText.text = Geekplay.Instance.PlayerData.money.ToString();
    }

    private void OnRevive()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnPlayerDeath(int deadCount, int stolenSlaps)
    {
        LosePanel.SetSlapCount(stolenSlaps);

        if (deadCount < 2)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            LosePanel.gameObject.SetActive(true);;

        }else if(deadCount >= 2)
        {
            LosePanel.AddEarnedMoney();
            SceneLoader sceneLoader = new SceneLoader(this);
            sceneLoader.LoadScene(0);
        }
    }

    private void OnChangeMoney(int money)
    {
        Geekplay.Instance.PlayerData.money += money;
        moneyText.text = Geekplay.Instance.PlayerData.money.ToString();
    }


}
