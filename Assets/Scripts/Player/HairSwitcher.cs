using System.Collections.Generic;
using UnityEngine;

public class HairSwitcher : MonoBehaviour
{
    [SerializeField] private List<GameObject> manHairs;
    [SerializeField] private List<GameObject> womanHairs;


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

}
