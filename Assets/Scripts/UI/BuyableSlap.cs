using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyableSlap : Buyable
{
    [SerializeField] private int indexOfAccessory;
    [SerializeField] private Button buttonSelf;

    public int GetIndexOfSlap { get { return indexOfAccessory; } }
    public void SubscribeOnClick(UnityAction action)
    {
        buttonSelf.onClick.AddListener(action);
    }
}
