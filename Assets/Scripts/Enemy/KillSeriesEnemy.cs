using System;
using TMPro;
using UnityEngine;

public class KillSeriesEnemy : Enemy
{
    [SerializeField] private float timeToNextSlap;
    [SerializeField] private float timeRamainingToNextSlap;
    [SerializeField] private TextMeshProUGUI timeText;

    protected override void Update()
    {
        base.Update();

        timeRamainingToNextSlap -= Time.deltaTime;
        timeText.text = Convert.ToInt32(timeRamainingToNextSlap).ToString();

        if (timeRamainingToNextSlap < 0)
        {
            Death();
        }
    }

    protected override void OnSuccesAttack()
    {
        base.OnSuccesAttack();
        timeRamainingToNextSlap = timeToNextSlap;
    }
}
