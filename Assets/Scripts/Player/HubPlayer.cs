using UnityEngine;

public class HubPlayer : MonoBehaviour
{
    public Transform raycastPoint;

    private void Start()
    {
        Geekplay.Instance.BuffAcceleration = false;
        Geekplay.Instance.BuffDoubleSlap = false;
        Geekplay.Instance.BuffIncreaseHP = false;
        Geekplay.Instance.BuffIncreasePower = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            Raycast();
        }
    }

    private void Raycast()
    {
        Debug.Log("Raycasting");
        Ray ray = new Ray(raycastPoint.position, raycastPoint.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, 25))
        {
            Debug.Log("Hit something");
            if(hit.collider.gameObject.TryGetComponent(out Portal portal))
            {
                portal.TryBuy();
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(raycastPoint.position, raycastPoint.forward * 10);
    }
}
