using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public partial class Portal : MonoBehaviour
{
    public int SceneIndex;
    public Modes Mode;
    public int cost;
    public bool isBuyed;
    public GameObject lockPanel;
    public BoxCollider collider;
    public HubEventManager eventManager;
    private void Start()
    {
        if(Geekplay.Instance.PlayerData.BuyedModes.Contains(Mode))
        {
            OpenPortal();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer != 7) return;
        SceneLoader sceneLoader = new SceneLoader(this);

        Geekplay.Instance.currentMode = Mode;
        sceneLoader.LoadScene(SceneIndex);
    }


    public void TryBuy()
    {
        if(isBuyed)
        {
            Debug.Log("Already Buyed");
        }
        else if(Geekplay.Instance.PlayerData.money > cost)
        {
            Geekplay.Instance.PlayerData.money -= cost;
            eventManager.InvokeChangeMoneyEvents(Geekplay.Instance.PlayerData.money, Geekplay.Instance.PlayerData.DiamondMoney);
            Geekplay.Instance.PlayerData.BuyedModes.Add(Mode);

            OpenPortal();
        }
        else
        {
            Debug.Log("We Don`t have money");
        }
    }

    private void OpenPortal()
    {
        isBuyed = true;
        lockPanel.SetActive(false);
        collider.isTrigger = true;
    }
}