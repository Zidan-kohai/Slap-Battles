using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyableAccessory : Buyable
{
    [SerializeField] private int indexOfAccessory;
    [SerializeField] private Button buttonSelf;

    public int GetIndexOfAccessory { get { return indexOfAccessory; } }
    public void SubscribeOnClick(UnityAction action)
    {
        buttonSelf.onClick.AddListener(action);
    }
}
