using System.Collections.Generic;
using UnityEngine;

public class CategoriesChoose : MonoBehaviour
{
    [SerializeField] private List<GameObject> categoriesGameObject;
    
    public void TurnOn(int index)
    {
        for(int i = 0; i < categoriesGameObject.Count; i++)
        {
            categoriesGameObject[i].SetActive(false);

            if (index == i)
            {
                categoriesGameObject[i].SetActive(true);
            }
        }
    }
}
