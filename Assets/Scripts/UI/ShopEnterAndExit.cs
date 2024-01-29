using CMF;
using UnityEngine;

public class ShopEnterAndExit : MonoBehaviour
{
    [SerializeField] private AdvancedWalkerController playerMover;
    [SerializeField] private GameObject cinemashine;
    [SerializeField] private GameObject shopObject;

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
        cinemashine.SetActive(true);
        shopObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void CloseShop()
    {
        playerMover.enabled = true;
        cinemashine.SetActive(false);
        shopObject.SetActive(false);
    }
}
