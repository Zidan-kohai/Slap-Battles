using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
	public int money;
	public int DiamondMoney;
	public int LeaderboardSlap = 0;

    public List<int> BuyedLeatherColors = new List<int>();
    public List<int> BuyedHairColors = new List<int>();
    public List<int> BuyedBodyColors = new List<int>();
    public List<int> BuyedLegColors = new List<int>();
    public List<int> BuyedFootColors = new List<int>();


    public int CurrentLeatherColorIndex;
    public int CurrentHairColorIndex;
    public int CurrentBodyColorIndex;
    public int CurrentLegColorIndex;
    public int CurrentFootColorIndex;

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

    public List<Modes> BuyedModes = new List<Modes>() { Modes.Hub, Modes.Standart};

    public List<int> codes = new List<int>();

    /////InApps//////
    public string lastBuy;

    public float HeadColorBuff;
    public float HairColorBuff;
    public float BodyColorBuff;
    public float LegColorBuff;
    public float FootColorBuff;

    public float CapBuff;
    public float AccessoryBuff;
    public float ManHairBuff;
    public float WomanHairBuff;


    public bool IsVolumeOn = true;
    public bool review;

    public int GamemodeCounter;
}