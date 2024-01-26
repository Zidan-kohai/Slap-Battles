using UnityEngine;
using UnityEngine.UIElements;

public class Slap : MonoBehaviour
{
    private Animator animator;

    [SerializeField] private int attackPower;

    public int AttackPower { get { return attackPower; } }


    public void Attack()
    {
    }

    public virtual void SuperAttack()
    {

    }
}
