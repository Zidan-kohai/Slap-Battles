using CMF;
using System.Collections.Generic;
using UnityEngine;

public class ShopEnterAndExit : MonoBehaviour
{
    [SerializeField] private AdvancedWalkerController playerMover;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject cinemashine;
    [SerializeField] private GameObject shopObject;
    [SerializeField] private GameObject MobileInput;

    [SerializeField] List<GameObject> GOToDisableWhenOpenShop;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        playerMover.enabled = false;
        cameraController.enabled = false;
        cinemashine.SetActive(true);
        shopObject.SetActive(true);
        MobileInput.SetActive(false);

        foreach (GameObject go in GOToDisableWhenOpenShop)
        {
            go.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseShop()
    {
        playerMover.enabled = true;
        cameraController.enabled = true;

        cinemashine.SetActive(false);
        shopObject.SetActive(false);

        foreach (GameObject go in GOToDisableWhenOpenShop)
        {
            go.SetActive(true);
        }

        if (Geekplay.Instance.mobile)
            MobileInput.SetActive(true);
    }
}
