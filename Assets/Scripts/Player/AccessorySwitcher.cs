using System.Collections.Generic;
using UnityEngine;

public class AccessorySwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> manAccessories;
    [SerializeField] private List<GameObject> womanAccessories;

    public void SwitchAccessory(int index)
    {
        for (int i = 0; i < manAccessories.Count; i++)
        {
            manAccessories[i].SetActive(false);
            if (index == i)
            {
                manAccessories[i].SetActive(true);
            }
        }
        for (int i = 0; i < womanAccessories.Count; i++)
        {
            womanAccessories[i].SetActive(false);
            if (index == i)
            {
                womanAccessories[i].SetActive(true);
            }
        }
    }

    public void SwitchAndBuyAccessory(int index)
    {
        for (int i = 0; i < manAccessories.Count; i++)
        {
            manAccessories[i].SetActive(false);
            if (index == i)
            {
                manAccessories[i].SetActive(true);
            }
        }

        for (int i = 0; i < womanAccessories.Count; i++)
        {
            womanAccessories[i].SetActive(false);
            if (index == i)
            {
                womanAccessories[i].SetActive(true);
            }
        }

        Geekplay.Instance.PlayerData.BuyedAccessory.Add(index);

        
        Geekplay.Instance.PlayerData.currentAccessory = index;
    }
    public void SaveAndSwitchAccessory(int index)
    {
        SwitchAccessory(index);

        Geekplay.Instance.PlayerData.currentAccessory = index;
    }
}
