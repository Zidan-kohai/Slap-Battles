using UnityEngine;

public class BossModeEnemy : Enemy
{
    //To do enemy must attack whenever there is a chance
    [Range(0, 1000)]
    private int ChanceToAttackBoss;

    protected override void Update()
    {
        base.Update();


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
