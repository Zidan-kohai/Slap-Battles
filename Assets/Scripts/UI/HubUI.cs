using JetBrains.Annotations;
using TMPro;
using UnityEngine;

public class HubUI : MonoBehaviour
{
    [SerializeField] private HubEventManager eventManager;

    [SerializeField] private TextMeshProUGUI slapCountText;
    [SerializeField] private TextMeshProUGUI diamondCountText;

    private void Start()
    {
        slapCountText.text = Geekplay.Instance.PlayerData.money.ToString();
        diamondCountText.text = Geekplay.Instance.PlayerData.DiamondMoney.ToString();

        eventManager.SubscribeOnChangeMoney(ChangeMoney);
    }

    private void ChangeMoney(int slapCount, int diamondCount)
    {
        slapCountText.text = slapCount.ToString();
        diamondCountText.text = diamondCount.ToString();
    }
}
