using CMF;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopEnterAndExit : MonoBehaviour
{
    [SerializeField] private AdvancedWalkerController playerMover;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private GameObject cinemashine;
    [SerializeField] private GameObject shopObject;
    [SerializeField] private RectTransform shopItemHandler;
    [SerializeField] private GraphicRaycaster raycaster;

    [SerializeField] private GameObject MobileInput;
    [SerializeField] List<GameObject> GOToDisableWhenOpenShop;

    [SerializeField] private CameraMouseInput shopCameraMouseInput;
    [SerializeField] private SwipeDetector swipeDetector;

    public float animateDuration;
    public float YOpenPosition;
    public float YClosePosition;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 7)
        {
            OpenShop();
        }
    }

    public void OpenShop()
    {
        if(swipeDetector != null)
            shopCameraMouseInput.mobileSwipeDetector = swipeDetector;

        playerMover.enabled = false;
        cameraController.enabled = false;
        cinemashine.SetActive(true);
        shopObject.SetActive(true);
        MobileInput.SetActive(false);
        raycaster.enabled = false;

        foreach (GameObject go in GOToDisableWhenOpenShop)
        {
            go.SetActive(false);
        }

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;


        shopItemHandler.DOAnchorPosY(YOpenPosition, animateDuration).OnComplete(OnOpenEnd);
    }

    private void OnOpenEnd()
    {
        raycaster.enabled = true;
    }


    public void CloseShop()
    {
        raycaster.enabled = false;
        playerMover.enabled = true;
        cameraController.enabled = true;
        cinemashine.SetActive(false);

        if (Geekplay.Instance.mobile)
            MobileInput.SetActive(true);

        shopItemHandler.DOAnchorPosY(YClosePosition, animateDuration).OnComplete(OnCloseEnd);
    }

    private void OnCloseEnd()
    {
        shopObject.SetActive(false);

        foreach (GameObject go in GOToDisableWhenOpenShop)
        {
            go.SetActive(true);
        }

    }
}
