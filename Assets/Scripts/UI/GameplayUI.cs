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
    }

    private void OnRevive()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnPlayerDeath(int deadCount)
    {
        if (deadCount < 2)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            LosePanel.gameObject.SetActive(true);
            LosePanel.SetSlapCount(Convert.ToInt32(moneyText.text));

        }else if(deadCount >= 2)
        {
            Geekplay.Instance.PlayerData.money += Convert.ToInt32(moneyText.text);
            SceneLoader sceneLoader = new SceneLoader(this);
            sceneLoader.LoadScene(0);
        }
    }

    private void OnChangeMoney(int money)
    {
        moneyText.text = money.ToString();
    }


}
