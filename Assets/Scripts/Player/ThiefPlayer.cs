using System;
using TMPro;
using UnityEngine;

public class ThiefPlayer : Player
{
    [SerializeField] private TextMeshProUGUI slapCounterText;
    [SerializeField] private int minStoleChance;
    [SerializeField] private int maxStoleChance;

    private void Start()
    {
        base.Start();

        slapCounterText.text = stolenSlaps.ToString();
    }

    public override void GetDamage(float damagePower, Vector3 direction, out bool isDeath, out int stoledSlap)
    {
        base.GetDamageWithoutRebound(damagePower,  out isDeath, out stoledSlap);

        stoledSlap = StoleSlaps();

        eventManager.InvokeChangeMoneyEvents(-stoledSlap);
    }
    public override void SetStolenSlaps(int value)
    {
        base.SetStolenSlaps(value);

        slapCounterText.text = stolenSlaps.ToString();

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
