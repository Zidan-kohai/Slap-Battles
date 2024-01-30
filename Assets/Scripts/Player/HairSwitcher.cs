using System.Collections.Generic;
using UnityEngine;
//TO DO REFACTORING
public class HairSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> manHairs;
    [SerializeField] private List<GameObject> womanHairs;

    private void Start()
    {
        SwitchHair(Geekplay.Instance.PlayerData.currentWomanHair, false);
        SwitchHair(Geekplay.Instance.PlayerData.currentManHair, true);
    }

    public void SwitchHair(int index)
    {
        if (Geekplay.Instance.PlayerData.isGenderMan)
        {
            for (int i = 0; i < manHairs.Count; i++)
            {
                manHairs[i].SetActive(false);
                if (index == i)
                {
                    manHairs[i].SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < womanHairs.Count; i++)
            {
                womanHairs[i].SetActive(false);
                if (index == i)
                {
                    womanHairs[i].SetActive(true);
                }
            }
        }
    }

    public void SwitchHair(int index, bool isMan)
    {
        if (isMan)
        {
            for (int i = 0; i < manHairs.Count; i++)
            {
                manHairs[i].SetActive(false);
                if (index == i)
                {
                    manHairs[i].SetActive(true);
                }
            }
        }
        else
        {
            for (int i = 0; i < womanHairs.Count; i++)
            {
                womanHairs[i].SetActive(false);
                if (index == i)
                {
                    womanHairs[i].SetActive(true);
                }
            }
        }
    }

    public void SwitchAndBuyHair(int index, bool isMan)
    {
        if (isMan)
        {
            for (int i = 0; i < manHairs.Count; i++)
            {
                manHairs[i].SetActive(false);
                if (index == i)
                {
                    manHairs[i].SetActive(true);
                }
            }
            Geekplay.Instance.PlayerData.BuyedManHairs.Add(index);
        }
        else
        {
            for (int i = 0; i < womanHairs.Count; i++)
            {
                womanHairs[i].SetActive(false);
                if (index == i)
                {
                    womanHairs[i].SetActive(true);
                }
            }

            Geekplay.Instance.PlayerData.BuyedWomanHairs.Add(index);
        }


        if (isMan)
        {
            Geekplay.Instance.PlayerData.currentManHair = index;
        }
        else
        {
            Geekplay.Instance.PlayerData.currentWomanHair = index;
        }
    }
    public void ChangeHair(int index, bool isMan)
    {
        SwitchHair(index, isMan);
        if(isMan)
        {
            Geekplay.Instance.PlayerData.currentManHair = index;
        }
        else
        {
            Geekplay.Instance.PlayerData.currentWomanHair = index;
        }
    }


}
