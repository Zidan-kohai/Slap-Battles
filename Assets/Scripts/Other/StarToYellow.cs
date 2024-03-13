using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarToYellow : MonoBehaviour
{
	[SerializeField] private Image[] stars;
	[SerializeField] private Color yellowColor;

    public void StartClick(int index)
    {
    	for (int i = 0; i <= index; i++)
    	{
    		stars[i].color = yellowColor;
    	}
    }
}
