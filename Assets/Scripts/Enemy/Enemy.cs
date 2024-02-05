using System.Collections;
using System.Diagnostics.Tracing;
using TMPro;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : IHealthObject
{
    [Header("Components")]
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Image healthbar;
    [SerializeField] protected Animator animator;
    [SerializeField] private TextMeshProUGUI nameText;
    
    [SerializeField] private float speed;
    [SerializeField] private int damagePower;
    [SerializeField] protected float distanseToAttack;
    [SerializeField] protected bool CanWalk;
    [SerializeField] private float timeNextToAttack;
    [SerializeField] protected bool canAttack;

    [SerializeField] private IHealthObject enemy;
    [SerializeField] private Vector3 target;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private EventManager eventManager;
    [SerializeField] private int slapToGive;
    [SerializeField] protected int stolenSlaps;


    [SerializeField] private Transform MaxPosition, MinPosition;

    protected void Start()
    {
        stolenSlaps = 1;
        rb.isKinematic = true;
        CanWalk = true;
        canAttack = true;

        GetRandomTarget();
        Move(target);

        eventManager.SubscribeOnEnemyDeath(OnEnemyDeath);
        
        nameText.text = Helper.GetRandomName();
    }

    protected virtual void Update()
    {
        if (!CanWalk) return;
        if (!navMeshAgent.isOnNavMesh) Death();

        if(enemy != null)
        {
            target = enemy.transform.position;
            Move(target);


            if ((target - transform.position).magnitude < distanseToAttack && canAttack)
            {
                Attack();
            }
        }

        else if((target - transform.position).magnitude < 3f)
        {
            GetRandomTarget();
            Move(target);
        }

        animator.SetFloat("HorizontalSpeed", navMeshAgent.speed);
    }

    protected virtual void Attack()
    {
        enemy.GetDamage(damagePower, (target - transform.position).normalized, out bool isDeath, out int gettedSlap);
        OnSuccesAttack();

        StartCoroutine(AttackAnimation());

        if(isDeath)
        {
            enemy = null;
            GetRandomTarget();
        }
        stolenSlaps += gettedSlap;
    }

    protected virtual void OnSuccesAttack()
    {

    }

    public IEnumerator AttackAnimation()
    {
        canAttack = false;
        yield return new WaitForSeconds(timeNextToAttack);
        canAttack = true;
    }

    protected void Move(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }

    protected void GetRandomTarget()
    {
        target = new Vector3(Random.Range(MinPosition.position.x, MaxPosition.position.x),
            0,
            Random.Range(MinPosition.position.z, MaxPosition.position.z));
    }

    public override void GetDamage(float damagePower, Vector3 direction, out bool isDeath, out int gettedSlap)
    {
        health -= damagePower;
        healthbar.fillAmount = (health / maxHealth) < 0 ? 0 : health / maxHealth;

        if(navMeshAgent.hasPath)
            navMeshAgent.ResetPath();

        if (health <= 0)
        {
            Death();
        }
        else
        {
            StartCoroutine(GetDamageAnimation(direction, damagePower));
        }
        gettedSlap = slapToGive;
        isDeath = health <= 0;
    }

    protected void OnEndAnimations()
    {
        GetRandomTarget();
    }
    
    public IEnumerator GetDamageAnimation(Vector3 direction, float damagePower)
    {
        navMeshAgent.enabled = false;
        rb.isKinematic = false;
        CanWalk = false;
        direction.y = 0.8f;
        rb.AddForce(direction * damagePower * 25);

        yield return new WaitForSeconds(2);

        rb.isKinematic = true;
        navMeshAgent.enabled = true;
        CanWalk = true;

        OnEndAnimations();
    }

    public void Revive()
    {
        health = maxHealth;
        healthbar.fillAmount = health /  maxHealth;
    }

    public override void Death()
    {
        gameObject.SetActive(false);
        eventManager.InvokeActionsOnEnemyDeath(this);
    }

    private void OnEnemyDeath(Enemy enemy)
    {
        if(this.enemy != null && this.enemy.TryGetComponent(out Enemy myEnemy))
        {
            if (enemy == myEnemy)
            {
                this.enemy = null;
                GetRandomTarget(); 
                Move(target);
            }
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IHealthObject>(out IHealthObject healthObject))
        {
            enemy = healthObject;
        }
    }
}
