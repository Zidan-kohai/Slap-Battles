using System.Collections.Generic;
using UnityEngine;

public class Helper
{
    public static List<string> ruPlayerName = new List<string>()
    {
            "�����",
            "����",
            "����",
            "����",
            "����",
            "������",
            "�����",
            "�������",
    };

    public static List<string> enPlayerName = new List<string>()
    {
            "Anton",
            "Max",
            "Misha",
            "Tolya",
            "Vlad",
            "Andrey",
            "Bear",
            "Alexei",
    };
    public static List<string> trPlayerName = new List<string>()
    {
            "Adak",
            "Adzhena",
            "Gelistan",
            "Gizem",
            "Karasa",
            "Kivilcim",
            "Tuna",
            "Eke",
    };

    public static string GetRandomName()
    {
        if(Geekplay.Instance.language == "ru")
        {
            return ruPlayerName[Random.Range(0, ruPlayerName.Count)];
        }
        else if(Geekplay.Instance.language == "en")
        {
            return enPlayerName[Random.Range(0, enPlayerName.Count)];
        }
        else
        {
            return trPlayerName[Random.Range(0, trPlayerName.Count)];
        }
    }
}
