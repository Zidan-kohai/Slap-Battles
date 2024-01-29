using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerOnShop : MonoBehaviour
{
    public GameObject Object;
    public Vector3 deltaRotation;

    public Quaternion originRotation;

    private void Start()
    {
        originRotation = Object.transform.rotation;
    }
    private void Update()
    {
        Object.transform.Rotate(deltaRotation);
    }

    private void OnDisable()
    {
        Object.transform.rotation = originRotation;
    }
}
