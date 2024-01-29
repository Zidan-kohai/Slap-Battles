using TMPro;
using UnityEngine;

public class HubUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI slapCountText;

    private void Start()
    {
        slapCountText.text = Geekplay.Instance.PlayerData.money.ToString();
    }
}
