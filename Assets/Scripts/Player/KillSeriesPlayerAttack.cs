using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class KillSeriesPlayerAttack : PlayerAttack
{
    [SerializeField] private float timeToNextSlap;
    [SerializeField] private float timeRamainingToNextSlap;
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private Color textColor;
    [SerializeField] private Sequence sequence;

    private void Start()
    {
        eventManager.SubscribeOnPlayerRevive(OnRevive);
        sequence = DOTween.Sequence();
    }

    protected override void Update()
    {
        base.Update();

        timeRamainingToNextSlap -= Time.deltaTime;
        timeText.text = Convert.ToInt32(timeRamainingToNextSlap).ToString();

        if(timeRamainingToNextSlap < 5 && !sequence.active)
        {
            timeText.color = Color.red;
            sequence = DOTween.Sequence().Append(timeText.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f).SetEase(Ease.Linear).OnComplete(
                () => 
                { 
                    timeText.transform.DOScale(new Vector3(1.3f, 1.3f, 1.3f), 0.5f); 
                }).SetLoops(-1)).SetEase(Ease.Linear).OnKill(() =>
                {
                    timeText.transform.localScale = new Vector3(1, 1, 1);
                    timeText.color = Color.white;
                });
        }
        if(timeRamainingToNextSlap < 0)
        { 
            player.Death();
        }    
    }

    protected override void OnSuccesAttack()
    {
        base.OnSuccesAttack();
        timeRamainingToNextSlap = timeToNextSlap;
        sequence.Kill();
    }

    private void OnRevive()
    {
        timeRamainingToNextSlap = timeToNextSlap;
    }
}
