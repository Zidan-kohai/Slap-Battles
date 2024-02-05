using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyableAccessory : Buyable
{
    [SerializeField] private int indexOfAccessory;
    [SerializeField] private Button buttonSelf;

    [SerializeField] private GameObject SelectedIcon;

    public float HealthBuff;

    public int GetIndexOfAccessory { get { return indexOfAccessory; } }
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
