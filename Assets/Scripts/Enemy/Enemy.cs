using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : IHealthObject
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Image healthbar;
    [SerializeField] private Animator animator;
    
    [SerializeField] private float speed;
    [SerializeField] private int damagePower;
    [SerializeField] private float distanseToAttack;
    [SerializeField] private bool CanWalk;
    [SerializeField] private float timeNextToAttack;
    [SerializeField] private bool canAttack;

    [SerializeField] private IHealthObject enemy;
    [SerializeField] private Vector3 target;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] private EventManager eventManager;

    private void Start()
    {
        rb.isKinematic = true;
        CanWalk = true;
        canAttack = true;


        GetRandomTarget();
        Move(target);
    }

    private void Update()
    {
        if (!CanWalk) return;
        if (!navMeshAgent.isOnNavMesh) Destroy(gameObject);

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

    private void Attack()
    {
        enemy.GetDamage(damagePower, (target - transform.position).normalized, out bool isDeath);
        StartCoroutine(AttackAnimation());

        if(isDeath)
        {
            enemy = null;
            GetRandomTarget();
        }
    }

    public IEnumerator AttackAnimation()
    {
        canAttack = false;
        yield return new WaitForSeconds(timeNextToAttack);
        canAttack = true;
    }

    private void Move(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }

    private void GetRandomTarget()
    {
        target = new Vector3(Random.Range(transform.position.x - 10, transform.position.x + 10),
            transform.position.y,
            Random.Range(transform.position.x - 10, transform.position.x + 10));
    }

    public override void GetDamage(float damagePower, Vector3 direction, out bool isDeath)
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

        isDeath = health > 0;
    }
    private void OnEndAnimations()
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IHealthObject>(out IHealthObject healthObject))
        {
            enemy = healthObject;
        }
    }
}
