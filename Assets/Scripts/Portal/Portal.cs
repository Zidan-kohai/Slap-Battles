using System;
using System.Runtime.CompilerServices;
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        else if (!IsBuyed)
        {
            BuyPortal.InitializePanel(Cost, TryBuy);
            return;
        }

        SceneLoader sceneLoader = new SceneLoader(this);

        Geekplay.Instance.currentMode = Mode;
        sceneLoader.LoadScene(SceneIndex);
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

                OpenPortal();
            }
        }
        else if (costType == CostType.Diamond)
        {
            if (Geekplay.Instance.PlayerData.DiamondMoney >= Cost)
            {
                Geekplay.Instance.PlayerData.DiamondMoney -= Cost;
                EventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
                Geekplay.Instance.PlayerData.BuyedModes.Add(Mode);

                OpenPortal();
            }
        }
        else
        {
            Debug.Log("We Don`t have money");
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