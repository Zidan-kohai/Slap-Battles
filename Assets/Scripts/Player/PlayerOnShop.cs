using CMF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnShop : MonoBehaviour
{
    public GameObject Object;
    public Vector3 deltaRotation;
    public Quaternion originRotation;
    public CameraMouseInput Swipe;

    public bool NeedRotateSelf = false;

    private void OnEnable()
    {
        originRotation = Object.transform.rotation;
    }

    private void Update()
    {
        if (!Geekplay.Instance.mobile && !Input.GetMouseButton(0))
        {
            if(NeedRotateSelf)
            {
                Object.transform.Rotate(deltaRotation);
            }
            else
            {
                return;
            }
        }
        else if(Geekplay.Instance.mobile && Swipe.GetHorizontalCameraInput() == 0)
        {
            if(NeedRotateSelf)
            {
                Object.transform.Rotate(deltaRotation);
            }
            else
            {
                return;
            }
        }
        else
        {
            Object.transform.Rotate(deltaRotation * Swipe.GetHorizontalCameraInput());
        }

    }



    private void OnDisable()
    {
        Object.transform.rotation = originRotation;
    }
}
