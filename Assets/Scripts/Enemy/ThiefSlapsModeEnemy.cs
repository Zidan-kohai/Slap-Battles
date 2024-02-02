using System;
using TMPro;
using UnityEngine;

public class ThiefSlapsModeEnemy : Enemy
{
    [SerializeField] private TextMeshProUGUI slapCounterText;
    [SerializeField] private int minStoleChance;
    [SerializeField] private int maxStoleChance;

    private void Start()
    {
        base.Start();

        slapCounterText.text = stolenSlaps.ToString();
    }

    protected override void Attack()
    {
        base.Attack();

        slapCounterText.text = stolenSlaps.ToString();
    }

    public override void GetDamage(float damagePower, Vector3 direction, out bool isDeath, out int gettedSlap)
    {
        base.GetDamage(damagePower, direction, out isDeath, out gettedSlap);

        gettedSlap = StoleSlaps();
    }

    public int StoleSlaps()
    {
        int percent = UnityEngine.Random.Range(minStoleChance, maxStoleChance);

        float flag = (percent * stolenSlaps) / 100f;
        int stoleSlapCount = Mathf.CeilToInt(flag);

        stolenSlaps -= stoleSlapCount;

        slapCounterText.text = stolenSlaps.ToString();
        return stoleSlapCount;
    }
}