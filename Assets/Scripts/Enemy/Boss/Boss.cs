using UnityEngine;

public class Boss : Enemy
{
    [SerializeField] private Quaternion currentRotation;
    [SerializeField] private float rotationSpeed;

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
    protected override void Update()
    {
        base.Update();

        if(target != null)
        {

        }

        Rotate();
    }

    private void Rotate()
    {
        Vector3 forward = (enemy.transform.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(forward, Vector3.up);

        currentRotation = Quaternion.Slerp(currentRotation, targetRotation, rotationSpeed * Time.deltaTime);

        Vector3 euler = currentRotation.eulerAngles;

        transform.rotation = Quaternion.Euler(0, euler.y, 0);
    }
}