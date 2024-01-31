using System;
using System.Security.Permissions;
using TMPro;
using UnityEngine;
using UnityEngine.Scripting;

public class HealtBuffSystem : MonoBehaviour
{
    [SerializeField] private IHealthObject healthObject;

    private float headColorBuff;
    private float hairColorBuff;
    private float bodyColorBuff;
    private float armColorBuff;
    private float legColorBuff;
    private float footColorBuff;

    private float capBuff;
    private float accessoryBuff;
    private float manHairBuff;
    private float womanHairBuff;

    public float GetMaxHealthObject {  get => healthObject.MaxHealth; }


    private bool manGenderFlag = false;

    private void Start()
    {

        GetSaved();
    }

    private void GetSaved()
    {
        manGenderFlag = Geekplay.Instance.PlayerData.isGenderMan;
        headColorBuff = Geekplay.Instance.PlayerData.HeadColorBuff;
        hairColorBuff = Geekplay.Instance.PlayerData.HairColorBuff;
        bodyColorBuff = Geekplay.Instance.PlayerData.BodyColorBuff;
        armColorBuff = Geekplay.Instance.PlayerData.ArmColorBuff;
        legColorBuff = Geekplay.Instance.PlayerData.LegColorBuff;
        footColorBuff = Geekplay.Instance.PlayerData.FootColorBuff;
        capBuff = Geekplay.Instance.PlayerData.CapBuff;
        accessoryBuff = Geekplay.Instance.PlayerData.AccessoryBuff;
        manHairBuff = Geekplay.Instance.PlayerData.ManHairBuff;
        womanHairBuff = Geekplay.Instance.PlayerData.WomanHairBuff;

        healthObject.MaxHealth += headColorBuff + hairColorBuff + bodyColorBuff + armColorBuff + legColorBuff + footColorBuff + capBuff + accessoryBuff;

        if (manGenderFlag)
        {
            healthObject.MaxHealth += manHairBuff;
        }
        else
        {
            healthObject.MaxHealth += womanHairBuff;
        }
    }

    public void AddBuff(HealtBuffType type, float buffPower, TextMeshProUGUI buffText)
    {
        switch(type)
        {
            case HealtBuffType.HeadColor:
                ChangeBuff(ref headColorBuff, ref Geekplay.Instance.PlayerData.HeadColorBuff, buffPower);
                break;
            case HealtBuffType.HairColor:
                ChangeBuff(ref hairColorBuff, ref Geekplay.Instance.PlayerData.HairColorBuff, buffPower);
                break;
            case HealtBuffType.BodyColor:  
                ChangeBuff(ref bodyColorBuff, ref Geekplay.Instance.PlayerData.BodyColorBuff, buffPower);
                break;
            case HealtBuffType.armColor:
                ChangeBuff(ref armColorBuff, ref Geekplay.Instance.PlayerData.ArmColorBuff, buffPower);
                break;
            case HealtBuffType.legColor:
                ChangeBuff(ref legColorBuff, ref Geekplay.Instance.PlayerData.LegColorBuff, buffPower);
                break;
            case HealtBuffType.footColor:
                ChangeBuff(ref footColorBuff, ref Geekplay.Instance.PlayerData.FootColorBuff, buffPower);
                break;
            case HealtBuffType.cap:
                ChangeBuff(ref capBuff, ref Geekplay.Instance.PlayerData.CapBuff, buffPower);
                break;
            case HealtBuffType.accessory:
                ChangeBuff(ref accessoryBuff, ref Geekplay.Instance.PlayerData.AccessoryBuff, buffPower);
                break;
            case HealtBuffType.hair:
                if (Geekplay.Instance.PlayerData.isGenderMan)
                {
                    ChangeBuff(ref manHairBuff, ref Geekplay.Instance.PlayerData.ManHairBuff, buffPower);
                }
                else
                {
                    ChangeBuff(ref womanHairBuff, ref Geekplay.Instance.PlayerData.WomanHairBuff, buffPower);
                }

                break;
        }

        if(buffText != null)
        {
            buffText.text = healthObject.MaxHealth.ToString();
        }
    }

    public float CompareBuff(HealtBuffType type, float buffPower)
    {
        switch (type)
        {
            case HealtBuffType.HeadColor:
                return buffPower - headColorBuff;
            case HealtBuffType.HairColor:
                return buffPower - hairColorBuff;
            case HealtBuffType.BodyColor:
                return buffPower - bodyColorBuff;
            case HealtBuffType.armColor:
                return buffPower - armColorBuff;
            case HealtBuffType.legColor:
                return buffPower - legColorBuff;
            case HealtBuffType.footColor:
                return buffPower - footColorBuff;
            case HealtBuffType.cap:
                return buffPower - capBuff;
            case HealtBuffType.accessory:
                return buffPower - accessoryBuff;
            case HealtBuffType.hair:
                if (Geekplay.Instance.PlayerData.isGenderMan)
                {
                    return buffPower - manHairBuff;
                }
                else
                {
                    return buffPower - womanHairBuff;
                }
        }

        return 0;
    }

    private void ChangeBuff(ref float lastBuff, ref float savedBuff, float newBuff)
    {
        healthObject.MaxHealth -= lastBuff;
        healthObject.MaxHealth += newBuff;

        lastBuff = newBuff;

        savedBuff = newBuff;
    }

    public void OnSwitchGender()
    {
        if (manGenderFlag == Geekplay.Instance.PlayerData.isGenderMan) return;

        if(!manGenderFlag)
        {
            healthObject.MaxHealth -= womanHairBuff;
            healthObject.MaxHealth += manHairBuff;
            manGenderFlag = true;
        }
        else
        {
            healthObject.MaxHealth -= manHairBuff;
            healthObject.MaxHealth += womanHairBuff;
            manGenderFlag = false;
        }
    }


    public enum HealtBuffType
    {
        HeadColor,
        HairColor,
        BodyColor, 
        armColor, 
        legColor, 
        footColor,
        cap,
        accessory,
        hair
    }
}
