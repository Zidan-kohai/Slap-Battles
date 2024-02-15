using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BuyableCap : Buyable
{
    [SerializeField] private int indexOfCap;
    [SerializeField] private Button buttonSelf;

    [SerializeField] private GameObject SelectedIcon;

    public float HealthBuff;

    public int GetIndexOfCap { get { return indexOfCap; } }
    public void SubscribeOnClick(UnityAction action)
    {
        buttonSelf.onClick.AddListener(action);
    }

    public void Select()
    {
        //SelectedIcon.SetActive(true);
        SelectedIcon = Resources.Load<GameObject>("SelectedIcon");

        SelectedIcon = Instantiate(SelectedIcon, transform);
    }

    public void Unselect()
    {
        //SelectedIcon.SetActive(false);
        Destroy(SelectedIcon);
    }
}
