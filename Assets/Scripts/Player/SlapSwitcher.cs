using System.Collections.Generic;
using UnityEngine;

public class SlapSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject manSlapsHandler;
    [SerializeField] private GameObject womanSlapsHandler;

    [SerializeField] private List<GameObject> manSlaps;
    [SerializeField] private List<GameObject> womanSlaps;


    private void Start()
    {
        SwitchSlap(Geekplay.Instance.PlayerData.currentSlap);
    }

    public void SwitchSlap(int index)
    {
        for (int i = 0; i < manSlaps.Count; i++)
        {
            manSlaps[i].SetActive(false);
            if (index == i)
            {
                manSlaps[i].SetActive(true);
            }
        }
        for (int i = 0; i < womanSlaps.Count; i++)
        {
            womanSlaps[i].SetActive(false);
            if (index == i)
            {
                womanSlaps[i].SetActive(true);
            }
        }
    }

    public void SwitchAndBuySlap(int index)
    {
        for (int i = 0; i < manSlaps.Count; i++)
        {
            manSlaps[i].SetActive(false);
            if (index == i)
            {
                manSlaps[i].SetActive(true);
            }
        }

        for (int i = 0; i < womanSlaps.Count; i++)
        {
            womanSlaps[i].SetActive(false);
            if (index == i)
            {
                womanSlaps[i].SetActive(true);
            }
        }

        Geekplay.Instance.PlayerData.BuyedSlaps.Add(index);


        Geekplay.Instance.PlayerData.currentSlap = index;
    }
    public void SaveAndSwitchSlap(int index)
    {
        SwitchSlap(index);

        Geekplay.Instance.PlayerData.currentSlap = index;
    }

    public void ShowSlaps()
    {
        manSlapsHandler.SetActive(true);
        womanSlapsHandler.SetActive(true);
    }
    public void DisableSlaps()
    {
        manSlapsHandler.SetActive(false);
        womanSlapsHandler.SetActive(false);
    }
}
