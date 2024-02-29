using CMF;
using System.Net.WebSockets;
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.SetActive(true);
        walkeController.enabled = false;
        cameraInput.enabled = false;

        text.text = $"купить портал за {cost}";

        buyButton.onClick?.RemoveAllListeners();
        buyButton.onClick.AddListener(action);
    }
}
