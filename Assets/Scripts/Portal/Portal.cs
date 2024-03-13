using CMF;
using DG.Tweening;
using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public partial class Portal : MonoBehaviour
{
    public int SceneIndex;
    public Modes Mode;
    public int Cost;
    public bool IsBuyed;
    public GameObject LockPanel;
    public BoxCollider Collider;
    public HubEventManager EventManager;
    public BuyPortalPanel BuyPortal;

    public CostType costType;
    public GameObject SlapIcon;
    public GameObject DiamondIcon;

    public ShopEnterAndExit inAppShop;

    [SerializeField] private AdvancedWalkerController playerMover;
    [SerializeField] private CameraController cameraController;

    [SerializeField] private TextMeshProUGUI costText;
    private void Start()
    {
        if(Geekplay.Instance.PlayerData.BuyedModes.Contains(Mode) || IsBuyed)
        {
            OpenPortal();
        }

        if(costType == CostType.Money)
        {
            SlapIcon.SetActive(true);
            DiamondIcon.SetActive(false);
        }
        else
        {
            SlapIcon.SetActive(false);
            DiamondIcon.SetActive(true);
        }

        if(Geekplay.Instance.language == "ru")
        {
            costText.text = $"купить за {Cost}";
        }
        if (Geekplay.Instance.language == "en")
        {
            costText.text = $"buy for {Cost}";
        }
        if (Geekplay.Instance.language == "tr")
        {
            costText.text = $"satin al {Cost}";
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        else if (!IsBuyed)
        {
            BuyPortal.InitializePanel(Cost, TryBuy, costType);
            return;
        }

        LoadMode();
    }

    private void LoadMode()
    {
        Geekplay.Instance.currentMode = Mode;
        Geekplay.Instance.LoadScene(SceneIndex);
    }

    public void TryBuy()
    {
        if(IsBuyed)
        {
            Debug.Log("Already Buyed");
        }
        else if (costType == CostType.Money)
        {
            if (Geekplay.Instance.PlayerData.money >= Cost)
            {
                Geekplay.Instance.PlayerData.money -= Cost;
                EventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
                Geekplay.Instance.PlayerData.BuyedModes.Add(Mode);

                playerMover.enabled = true;
                cameraController.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                OpenPortal();
                LoadMode();
            }
            else
            {
                inAppShop.OpenShop();
            }
        }
        else if (costType == CostType.Diamond)
        {
            if (Geekplay.Instance.PlayerData.DiamondMoney >= Cost)
            {
                Geekplay.Instance.PlayerData.DiamondMoney -= Cost;
                EventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
                Geekplay.Instance.PlayerData.BuyedModes.Add(Mode);

                playerMover.enabled = true;
                cameraController.enabled = true;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;

                OpenPortal();
                LoadMode();
            }
            else
            {
                inAppShop.OpenShop();
            }
        }
    }

    private void OpenPortal()
    {
        IsBuyed = true;
        LockPanel.SetActive(false);
    }

    public enum CostType
    {
        Money,
        Diamond
    }
}