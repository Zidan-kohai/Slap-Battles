using UnityEngine;

public class IHealthObject : MonoBehaviour
{
    [SerializeField] protected float currenthealth;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float randomLeftHealth, randomRightHealth;
    protected bool isDead = false;


    public bool IsDead => isDead;
    public float CurrentHealth { get => currenthealth; set => currenthealth = value; }
    public float MaxHealth { get => maxHealth; set => maxHealth = value; }

    public virtual void GetDamage(float damagePower, Vector3 direction, out bool isDeath, out int gettedSlap)
    {
        gettedSlap = 1;
        isDeath = false;
    }

    public virtual void Death(bool PlayDeathAnimation)
    {
    }
    public virtual void GetDamageWithoutRebound(float damagePower, out bool isDeath, out int gettedSlap, bool playAuchAudio = false) 
    {
        isDeath = false;
        gettedSlap = 1;
    }
}