using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaticTranslator : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [Multiline] public string ru;
    [Multiline] public string en;
    [Multiline] public string tr;

    private void Start()
    {
        if(Geekplay.Instance.language == "ru")
        {
            text.text = ru;
        }
        else if(Geekplay.Instance.language == "en")
        {
            text.text = en;
        }
        else if(Geekplay.Instance.language == "tr")
        {
            text.text = tr;
        }
    }
}
