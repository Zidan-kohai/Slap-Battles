using UnityEngine;

public class Slap : MonoBehaviour
{
    [SerializeField] private SlapPowerType type;
    [SerializeField] private float attackPower;
    public float rollBackTime;
    public string descriptionOfSuperPower;
    public SlapPowerType GetSlapPowerType() { return type; }
    public float AttackPower { get { return attackPower; } set { attackPower = value; } }


    public void Attack()
    {
    }

    public virtual void SuperAttack()
    {

    }
}
