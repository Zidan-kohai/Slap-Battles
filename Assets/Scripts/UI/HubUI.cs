using TMPro;
using UnityEngine;

public class HubUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slapCountText;
    [SerializeField] private TextMeshProUGUI DiamondCountText;

    private void Start()
    {
        slapCountText.text = Geekplay.Instance.PlayerData.money.ToString();
        DiamondCountText.text = Geekplay.Instance.PlayerData.DiamondMoney.ToString();
    }
}
