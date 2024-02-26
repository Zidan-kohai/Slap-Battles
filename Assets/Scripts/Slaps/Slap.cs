using UnityEngine;
using UnityEngine.UIElements;

public class Slap : MonoBehaviour
{
    [SerializeField] private SlapPowerType type;
    [SerializeField] private float attackPower;

    public SlapPowerType GetSlapPowerType() { return type; }
    public float AttackPower { get { return attackPower; } set { attackPower = value; } }


    public void Attack()
    {
    }

    public virtual void SuperAttack()
    {

    }
}
