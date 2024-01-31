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
    private float hairBuff;

    public float GetMaxHealthObject {  get => healthObject.MaxHealth; }

    public void AddBuff(HealtBuffType type, float buffPower, TextMeshProUGUI buffText)
    {
        switch(type)
        {
            case HealtBuffType.HeadColor:
                ChangeBuff(ref headColorBuff, buffPower);
                break;
            case HealtBuffType.HairColor:
                ChangeBuff(ref hairColorBuff, buffPower);
                break;
            case HealtBuffType.BodyColor:  
                ChangeBuff(ref bodyColorBuff, buffPower);
                break;
            case HealtBuffType.armColor:
                ChangeBuff(ref armColorBuff, buffPower);
                break;
            case HealtBuffType.legColor:
                ChangeBuff(ref legColorBuff, buffPower);
                break;
            case HealtBuffType.footColor:
                ChangeBuff(ref footColorBuff, buffPower);
                break;
            case HealtBuffType.cap:
                ChangeBuff(ref capBuff, buffPower);
                break;
            case HealtBuffType.accessory:
                ChangeBuff(ref accessoryBuff, buffPower);
                break;
            case HealtBuffType.hair:
                ChangeBuff(ref hairBuff, buffPower);
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
                return buffPower - hairBuff;
        }

        return 0;
    }

    private void ChangeBuff(ref float lastBuff, float newBuff)
    {
        healthObject.MaxHealth -= lastBuff;
        healthObject.MaxHealth += newBuff;

        lastBuff = newBuff;
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
