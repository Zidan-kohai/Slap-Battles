using System;
using System.Collections.Generic;

[Serializable]
public class PlayerData
{
	public int money;

    public List<int> BuyedHearColors = new List<int>();
    public List<int> BuyedHeadColors = new List<int>();
    public List<int> BuyedBodyColors = new List<int>();
    public List<int> BuyedLegColors = new List<int>();
    public List<int> BuyedFootColors = new List<int>();
    public List<int> BuyedArmColors = new List<int>();


    public int CurrentHearColorIndex;
    public int CurrentHeadColorIndex;
    public int CurrentBodyColorIndex;
    public int CurrentLegColorIndex;
    public int CurrentFootColorIndex;
    public int CurrentArmColorIndex;
    /////InApps//////
    public string lastBuy;
}