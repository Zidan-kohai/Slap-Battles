using CMF;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyPortalPanel : MonoBehaviour
{
    public TextMeshProUGUI text;
    public Button buyButton;
    public AdvancedWalkerController walkeController;
    public CameraController cameraInput;
    public void InitializePanel(int cost, UnityAction action)
    {
        gameObject.SetActive(true);
        walkeController.enabled = false;
        cameraInput.enabled = false;

        text.text = $"купить портал за {cost}";

        buyButton.onClick?.RemoveAllListeners();
        buyButton.onClick.AddListener(action);
    }
}
