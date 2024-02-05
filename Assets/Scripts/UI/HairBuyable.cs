using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HairBuyable : Buyable
{
    [SerializeField] private int indexOfHair;
    [SerializeField] private Button buttonSelf;

    [SerializeField] private GameObject SelectedIcon;

    public float HealthBuff;

    public int GetIndexOfhair {  get { return indexOfHair; } }
    public void SubscribeOnClick(UnityAction action)
    {
        buttonSelf.onClick.AddListener(action);
    }

    public void Select()
    {
        SelectedIcon.SetActive(true);
    }

    public void Unselect()
    {
        SelectedIcon.SetActive(false);
    }
}
