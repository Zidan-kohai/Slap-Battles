using System.Collections.Generic;
using UnityEngine;

public class CapSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> manCaps;
    [SerializeField] private List<GameObject> womanCaps;


    private void Start()
    {
        SwitchCap(Geekplay.Instance.PlayerData.currentCap);
    }

    public void SwitchCap(int index)
    {
        for (int i = 0; i < manCaps.Count; i++)
        {
            manCaps[i].SetActive(false);
            if (index == i)
            {
                manCaps[i].SetActive(true);
            }
        }
        for (int i = 0; i < womanCaps.Count; i++)
        {
            womanCaps[i].SetActive(false);
            if (index == i)
            {
                womanCaps[i].SetActive(true);
            }
        }
    }

    public void SwitchAndBuyCap(int index)
    {
        for (int i = 0; i < manCaps.Count; i++)
        {
            manCaps[i].SetActive(false);
            if (index == i)
            {
                manCaps[i].SetActive(true);
            }
        }

        for (int i = 0; i < womanCaps.Count; i++)
        {
            womanCaps[i].SetActive(false);
            if (index == i)
            {
                womanCaps[i].SetActive(true);
            }
        }

        Geekplay.Instance.PlayerData.BuyedCaps.Add(index);


        Geekplay.Instance.PlayerData.currentCap = index;
    }
    public void SaveAndSwitchCap(int index)
    {
        SwitchCap(index);

        Geekplay.Instance.PlayerData.currentCap = index;
    }   
}
