using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class SlapSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject manSlapsHandler;
    [SerializeField] private GameObject womanSlapsHandler;

    [SerializeField] private List<GameObject> manSlaps;
    [SerializeField] private List<GameObject> womanSlaps;

    [Header("Links")]
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private SlapPower power;
    [SerializeField] private TextMeshProUGUI powerDescription;
    //[SerializeField] private Image powerRollbackTime;

    private void Start()
    {
        Slap slap = SwitchSlap(Geekplay.Instance.PlayerData.currentSlap);
        playerAttack.ChangeSlap(slap);
        power.ChangeSlap(slap);
    }

    public Slap SwitchSlap(int index)
    {
        Slap chosedSlap = null;
        for (int i = 0; i < manSlaps.Count; i++)
        {
            manSlaps[i].SetActive(false);
            if (index == i)
            {
                manSlaps[i].SetActive(true);
                chosedSlap = manSlaps[i].GetComponent<Slap>();
            }
        }
        for (int i = 0; i < womanSlaps.Count; i++)
        {
            womanSlaps[i].SetActive(false);
            if (index == i)
            {
                womanSlaps[i].SetActive(true);
                chosedSlap = womanSlaps[i].GetComponent<Slap>();
            }
        }

        powerDescription.text = chosedSlap.descriptionOfSuperPower;

        return chosedSlap;
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
