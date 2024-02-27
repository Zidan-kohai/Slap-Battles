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
    public NavMeshAgent GetNavMeshAgent => navMeshAgent;

    [Header("Components")]
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Collider collider;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Image healthbar;
    [SerializeField] protected Animator animator;
    
    [SerializeField] private float speed;
    [SerializeField] private int damagePower;
    [SerializeField] protected float distanseToAttack;
    [SerializeField] protected bool CanWalk;
    [SerializeField] private float timeNextToAttack;
    [SerializeField] protected bool canAttack;

    [SerializeField] private IHealthObject enemy;
    [SerializeField] private Vector3 target;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] protected EventManager eventManager;
    [SerializeField] private int slapToGive;
    [SerializeField] protected int stolenSlaps;

    [SerializeField] private Transform MaxPosition, MinPosition;
    protected bool isDead = false;
    protected bool canGetDamage = false;


    [Header("Audio")]
    [SerializeField] private AudioSource slapAudio;
    public void ChangeEnemy(IHealthObject target) =>  enemy = target;
    protected void Start()
    {
        stolenSlaps = 1;
        rb.isKinematic = true;
        CanWalk = true;
        canAttack = true;
        isDead = false;

        GetRandomTarget();
        Move(target);

        eventManager.SubscribeOnEnemyDeath(OnEnemyDeath);
        StartCoroutine(WaitTimeBeforeAttackIntoStart());
    }

    private IEnumerator WaitTimeBeforeAttackIntoStart()
    {
        canAttack = false;
        yield return new WaitForSeconds(timeNextToAttack);
        canAttack = true;
    }

    protected virtual void Update()
    {
        if (!CanWalk || isDead) return;
        if (!navMeshAgent.isOnNavMesh) Derath();

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

        StartCoroutine(WaitTimeBeforeAttackIntoStart());

        if(isDeath)
        {
            enemy = null;
            GetRandomTarget();
        }
        stolenSlaps += gettedSlap;


        slapAudio.Play();
    }

    protected virtual void OnSuccesAttack()
    {

    }

    protected void Move(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }

    protected void GetRandomTarget()
    {
        target = new Vector3(Random.Range(MinPosition.position.x, MaxPosition.position.x),
            transform.position.y,
            Random.Range(MinPosition.position.z, MaxPosition.position.z));
    }

    public override void GetDamage(float damagePower, Vector3 direction, out bool isDeath, out int gettedSlap)
    {
        if (isDead)
        {
            isDeath = true;
            gettedSlap = 0;
            return;
        }
        if (!canGetDamage)
        {
            isDeath = false;
            gettedSlap = 0;
            return;
        }
        canGetDamage = false;
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
        canGetDamage = true;
    }
    
    public IEnumerator GetDamageAnimation(Vector3 direction, float damagePower)
    {
        navMeshAgent.enabled = false;
        rb.isKinematic = false;
        CanWalk = false;
        direction.y = 0.8f;
        rb.AddForce(direction * damagePower * 2500);

        yield return new WaitForSeconds(2);

        if (!isDead)
        {
            rb.isKinematic = true;
            navMeshAgent.enabled = true;
            CanWalk = true;

            OnEndAnimations();
        }
    }

    public virtual void Revive()
    {
        animator.SetTrigger("Revive");
        health = maxHealth;
        healthbar.fillAmount = health / maxHealth;
        gameObject.SetActive(true);
        navMeshAgent.enabled = true;
        //collider.enabled = true;
        isDead = false;
        canGetDamage = true;
    }

    public override void Death(bool playDeathAnimation = true)
    {
        if (isDead) return;
        navMeshAgent.enabled = false;
        isDead = true;

        if (playDeathAnimation)
            animator.SetTrigger("Death");

        eventManager.InvokeActionsOnEnemyDeath(this);
        StartCoroutine(DisableGameObject(7));

    }

    private IEnumerator DisableGameObject(float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
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

    public void Sleep(float timeToWakeUp)
    {
        //Enable sleep animation
        navMeshAgent.enabled = true;

        StartCoroutine(WakeUp(timeToWakeUp));
    }


    private IEnumerator WakeUp(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);


        //Disable sleep animation

        navMeshAgent.enabled = true;
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IHealthObject>(out IHealthObject healthObject))
        {
            enemy = healthObject;
        }
    }
}
