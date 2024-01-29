using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField] private List<GameObject> objectToScroll;
    [SerializeField] private int currentScrollIndex = 1;

    [Header("Visual")]
    [SerializeField] private TextMeshProUGUI currentScrollCount;
    [SerializeField] private TextMeshProUGUI maxScrollCount;
    private void OnValidate()
    {
        currentScrollCount.text = currentScrollIndex.ToString();
        maxScrollCount.text = objectToScroll.Count.ToString();
    }


    public void Right()
    {
        if (currentScrollIndex + 1 > objectToScroll.Count)
            return;

        currentScrollIndex++;


        objectToScroll[currentScrollIndex - 2].SetActive(false);
        objectToScroll[currentScrollIndex - 1].SetActive(true);

        if(currentScrollCount != null)
            currentScrollCount.text = currentScrollIndex.ToString();
    }

    public void Left()
    {
        if (currentScrollIndex - 1 < 1)
            return;

        currentScrollIndex--;


        objectToScroll[currentScrollIndex].SetActive(false);
        objectToScroll[currentScrollIndex - 1].SetActive(true);

        currentScrollCount.text = currentScrollIndex.ToString();
    }
}
