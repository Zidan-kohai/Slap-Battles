using UnityEngine;
using UnityEngine.AI;

public class BossModeEnemy : Enemy
{
    //To do enemy must attack whenever there is a chance
    [Range(0, 1000)]
    private int ChanceToAttackBoss;

    [SerializeField] private Transform bossTransform;

    protected override void Update()
    {
        if (!CanWalk) return;
        if (!navMeshAgent.isOnNavMesh) Death();

        if ((bossTransform.position - transform.position).magnitude < distanseToAttack && canAttack)
        {
            Attack();
        }
        else
        {
            navMeshAgent.SetDestination(bossTransform.position);
        }


        animator.SetFloat("HorizontalSpeed", navMeshAgent.speed);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        
    }

    protected override void OnSuccesAttack()
    {
        base.OnSuccesAttack();

        GetRandomTarget();
    }
}
