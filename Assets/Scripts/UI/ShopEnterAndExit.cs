using CMF;
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
    [SerializeField] private GraphicRaycaster raycaster;

    [SerializeField] private GameObject MobileInput;
    [SerializeField] List<GameObject> GOToDisableWhenOpenShop;

    [SerializeField] private Animator animator;

    public float WaitForEndAnimation;
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

        animator.SetTrigger("Open");
        raycaster.enabled = false;
        StartCoroutine(Waiter(WaitForEndAnimation, OnOpenEnd));
    }

    private void OnOpenEnd()
    {
        raycaster.enabled = true;
    }


    public void CloseShop()
    {
        animator.SetTrigger("Close");
        raycaster.enabled = false;
        StartCoroutine(Waiter(WaitForEndAnimation, OnCloseEnd));
    }

    public void OnCloseEnd()
    {
        playerMover.enabled = true;
        cameraController.enabled = true;
        raycaster.enabled = true;

        cinemashine.SetActive(false);
        shopObject.SetActive(false);

        foreach (GameObject go in GOToDisableWhenOpenShop)
        {
            go.SetActive(true);
        }

        if (Geekplay.Instance.mobile)
            MobileInput.SetActive(true);
    }
    private IEnumerator Waiter(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);
        action?.Invoke();
    }
}
