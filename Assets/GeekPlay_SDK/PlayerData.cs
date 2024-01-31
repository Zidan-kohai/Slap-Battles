using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerData
{
	public int money;

    public List<int> BuyedHeadColors = new List<int>();
    public List<int> BuyedHairColors = new List<int>();
    public List<int> BuyedBodyColors = new List<int>();
    public List<int> BuyedLegColors = new List<int>();
    public List<int> BuyedFootColors = new List<int>();
    public List<int> BuyedArmColors = new List<int>();


    public int CurrentHeadColorIndex;
    public int CurrentHairColorIndex;
    public int CurrentBodyColorIndex;
    public int CurrentLegColorIndex;
    public int CurrentFootColorIndex;
    public int CurrentArmColorIndex;

    public bool isGenderMan;

    public List<int> BuyedManHairs = new List<int>();
    public List<int> BuyedWomanHairs = new List<int>();

    public int currentManHair;
    public int currentWomanHair;


    public List<int> BuyedAccessory = new List<int>();

    public int currentAccessory;


    public List<int> BuyedCaps = new List<int>();

    public int currentCap;

    public List<int> BuyedSlaps = new List<int>();

    public int currentSlap;

    /////InApps//////
    public string lastBuy;
}