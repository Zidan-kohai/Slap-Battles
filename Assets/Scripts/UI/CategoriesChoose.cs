using System.Collections.Generic;
using UnityEngine;
//TO DO Some Refactoring
public class CategoriesChoose : MonoBehaviour
{
    [SerializeField] private List<GameObject> categoriesGameObject;
    [SerializeField] private GameObject manHair;
    [SerializeField] private GameObject womanHair;
    [SerializeField] private GameObject buyButton;
    [SerializeField] private SkineShopController shopController;
    private void OnEnable()
    {
        TurnOn(0);   
    }
    public void TurnOn(int index)
    {
        buyButton.SetActive(false);
        shopController.ResetToWeared();

        for (int i = 0; i < categoriesGameObject.Count; i++)
        {
            categoriesGameObject[i].SetActive(false);

            if (index == i)
            {
                categoriesGameObject[i].SetActive(true);
            }
        }

        if(index == 1)
        {
            if(Geekplay.Instance.PlayerData.isGenderMan)
            {
                manHair.SetActive(true);
                womanHair.SetActive(false);
            }
            else
            {
                manHair.SetActive(false);
                womanHair.SetActive(true);
            }
        }
    }
}
