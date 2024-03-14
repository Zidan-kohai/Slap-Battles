using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class BuyableColor : Buyable
{
    public int indexOfColor;
    [SerializeField] private Image image;
    [SerializeField] private Button buttonSelf;
    [SerializeField] private BodyPart bodyPart;

    private GameObject SelectedIcon;

    public Color GetColor => image.color;
    public BodyPart GetBodyType { get { return bodyPart; } }


    [HideInInspector]
    public float HealthBuff;

    //private void Awake()
    //{ 
    //    image = GetComponent<Image>();
    //    buttonSelf = GetComponent<Button>();
    //}


    public void SubscribeOnClick(UnityAction action)
    {
        buttonSelf.onClick.AddListener(action);
    }

    public void Unselect()
    {
        Destroy(SelectedIcon);
    }

}