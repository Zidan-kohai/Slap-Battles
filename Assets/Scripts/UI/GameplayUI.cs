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
        eventManager.SubscribeOnChangeMoney(ChangeMoney);
        eventManager.SubscribeOnPlayerDeath(OnPlayerDeath);
    }

    private void OnPlayerDeath()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        LosePanel.gameObject.SetActive(true);
        LosePanel.SetSlapCount(Convert.ToInt32(moneyText.text));
    }

    private void ChangeMoney(int money)
    {
        moneyText.text = money.ToString();
    }


}
