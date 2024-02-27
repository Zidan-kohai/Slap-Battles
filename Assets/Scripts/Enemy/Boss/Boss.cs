using UnityEngine;

public class Boss : Enemy
{
    public override void Death(bool playDeathAnimation = true)
    {
        gameObject.SetActive(false);
        eventManager.InvokeActionsOnBossDeath();
    }

    public override void GetDamage(float damagePower, Vector3 direction, out bool isDeath, out int gettedSlap)
    {
        if (isDead)
        {
            isDeath = true;
            gettedSlap = 0;
            return;
        }
        canGetDamage = false;
        health -= damagePower;
        healthbar.fillAmount = (health / maxHealth) < 0 ? 0 : health / maxHealth;

        if (navMeshAgent.hasPath)
            navMeshAgent.ResetPath();

        if (health <= 0)
        {
            Death();
        }
        else
        {
            StartCoroutine(GetDamageAnimation(direction));
        }
        gettedSlap = slapToGive;
        isDeath = health <= 0;
    }
}