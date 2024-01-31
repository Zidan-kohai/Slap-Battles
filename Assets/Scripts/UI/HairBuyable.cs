using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class HairBuyable : Buyable
{
    [SerializeField] private int indexOfHair;
    [SerializeField] private Button buttonSelf;

    public float HealthBuff;

    public int GetIndexOfhair {  get { return indexOfHair; } }
    public void SubscribeOnClick(UnityAction action)
    {
        buttonSelf.onClick.AddListener(action);
    }
}
