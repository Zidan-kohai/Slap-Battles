using UnityEngine;
using UnityEngine.UIElements;

public class Slap : MonoBehaviour
{
    [SerializeField] private SlapPowerType type;
    [SerializeField] private int attackPower;

    public SlapPowerType GetSlapPowerType() { return type; }
    public int AttackPower { get { return attackPower; } }


    public void Attack()
    {
    }

    public virtual void SuperAttack()
    {

    }
}
