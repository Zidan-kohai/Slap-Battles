using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Slap : MonoBehaviour
{
    private const string IsAttacking = "IsAttacking";
    private Animator animator;

    [SerializeField] private int attackPower;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        animator.SetTrigger(IsAttacking);
    }

    public virtual void SuperAttack()
    {

    }
}
