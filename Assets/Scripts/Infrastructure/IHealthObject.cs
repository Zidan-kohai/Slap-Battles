using UnityEngine;

public class IHealthObject : MonoBehaviour
{
    [SerializeField] protected float health;
    [SerializeField] protected float maxHealth;
    protected bool isDead = false;


    public bool IsDead => isDead;
    public float CurrentHealth { get => health; set => health = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public virtual void GetDamage(float damagePower, Vector3 direction, out bool isDeath, out int gettedSlap)
    {
        gettedSlap = 1;
        isDeath = false;
    }

    public virtual void Death(bool PlayDeathAnimation)
    {
    }

}