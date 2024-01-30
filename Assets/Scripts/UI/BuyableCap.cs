using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyableCap : Buyable
{
    [SerializeField] private int indexOfCap;
    [SerializeField] private Button buttonSelf;

    public int GetIndexOfCap { get { return indexOfCap; } }
    public void SubscribeOnClick(UnityAction action)
    {
        buttonSelf.onClick.AddListener(action);
    }
}
