using TMPro;
using UnityEngine;

public class GameplayUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private EventManager eventManager;
    private void Start()
    {
        eventManager.SubscribeOnChangeMoney(ChangeMoney);

        moneyText.text = Geekplay.Instance.PlayerData.money.ToString();
    }

    private void ChangeMoney(int money)
    {
        moneyText.text = money.ToString();
    }


}
