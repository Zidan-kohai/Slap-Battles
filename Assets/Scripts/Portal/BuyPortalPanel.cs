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

    public GameObject SlapIcon;
    public GameObject DiamondIcon;
    public void InitializePanel(int cost, UnityAction action, Portal.CostType costType)
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        gameObject.SetActive(true);
        walkeController.enabled = false;
        cameraInput.enabled = false;

        text.text = $"купить портал за {cost}";

        buyButton.onClick?.RemoveAllListeners();
        buyButton.onClick.AddListener(action);

        switch(costType)
        {
            case Portal.CostType.Money:
                SlapIcon.SetActive(true);
                DiamondIcon.SetActive(false);
                break;
            case Portal.CostType.Diamond:
                DiamondIcon.SetActive(true);
                SlapIcon.SetActive(false);
                break;
        }
    }
}
