using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using TMPro;
using UnityEngine;

public class KillSeriesPlayerAttack : PlayerAttack
{
    [SerializeField] private float timeToNextSlap;
    [SerializeField] private float timeRamainingToNextSlap;
    [SerializeField] private TextMeshProUGUI timeText;

    protected override void Update()
    {
        base.Update();

        timeRamainingToNextSlap -= Time.deltaTime;
        timeText.text = Convert.ToInt32(timeRamainingToNextSlap).ToString();

        if(timeRamainingToNextSlap < 0)
        { 
            player.Death();
        }    
    }

    protected override void OnSuccesAttack()
    {
        base.OnSuccesAttack();
        timeRamainingToNextSlap = timeToNextSlap;
    }
}
