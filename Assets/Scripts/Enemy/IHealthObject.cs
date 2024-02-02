using UnityEngine;

public class IHealthObject : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;

    public float CurrentHealth { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public virtual void GetDamage(float damagePower, Vector3 direction, out bool isDeath)
    {
        isDeath = false;
    }

    public virtual void Death()
    {
    }

}