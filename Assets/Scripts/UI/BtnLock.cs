using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BtnLock : Button, IPointerDownHandler
{
    public override void OnPointerDown (PointerEventData eventData) 
	{
		base.OnPointerDown(eventData);
        onClick.Invoke();

        if (!Geekplay.Instance.mobile)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
	}
}
