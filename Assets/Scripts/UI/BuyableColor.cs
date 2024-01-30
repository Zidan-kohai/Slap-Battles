using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class BuyableColor : Buyable
{
    [SerializeField] public int indexOfColor;
    [SerializeField] private Image image;
    [SerializeField] private Button buttonSelf;
    [SerializeField] private BodyPart bodyPart;
    public Color GetColor => image.color;
    public BodyPart GetBodyType { get { return bodyPart; } }
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