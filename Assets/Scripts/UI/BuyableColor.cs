using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class BuyableColor : Buyable
{
    [SerializeField] public int indexOfColor;
    [SerializeField] private Image image;
    [SerializeField] private Button buttonSelf;
    public Color GetColor => image.color;
    private void Start()
    { 
        image = GetComponent<Image>();
        buttonSelf = GetComponent<Button>();
    }


    public void SubscribeOnClick(UnityAction action)
    {
        buttonSelf.onClick.AddListener(action);
    }



}