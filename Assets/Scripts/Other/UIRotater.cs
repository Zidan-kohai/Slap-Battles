using UnityEngine;

public class UIRotater : MonoBehaviour
{
    public Transform targetToRotate;
    public Transform uiHandler;

    void Update()
    {
        Rotate();       
    }

    private void Rotate()
    {
        Vector3 forward = (targetToRotate.position - uiHandler.transform.position).normalized;
        Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);
        Vector3 euler = rotation.eulerAngles;
        uiHandler.transform.rotation = Quaternion.Euler(0, euler.y, 0);
    }
}
