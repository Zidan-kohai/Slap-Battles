using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
//как нибудь надо сесть и отрефакторить это говно
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : IHealthObject
{
    public NavMeshAgent GetNavMeshAgent => navMeshAgent;

    [Header("Components")]
    [SerializeField] protected NavMeshAgent navMeshAgent;
    [SerializeField] protected Collider collider;
    [SerializeField] private Rigidbody rb;
    [SerializeField] protected Image healthbar;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject Canvas;
    
    [SerializeField] private float speed;
    [SerializeField] private int damagePower;
    [SerializeField] protected float distanseToAttack;
    [SerializeField] protected bool CanWalk;
    [SerializeField] protected bool isSleeping;
    [SerializeField] private float timeNextToAttack;
    [SerializeField] protected bool canAttack;

    [SerializeField] protected IHealthObject enemy;
    [SerializeField] protected Vector3 target;
    [SerializeField] private LayerMask enemyLayer;

    [SerializeField] protected EventManager eventManager;
    [SerializeField] protected int slapToGive;
    [SerializeField] protected int stolenSlaps;

    [SerializeField] private Transform MaxPosition, MinPosition;
    protected bool canGetDamage = false;
    [SerializeField] protected float sightAngle = 65;


    [Header("Audio")]
    [SerializeField] private AudioSource slapAudio;
    [SerializeField] private AudioSource deathAudio;
    public void ChangeEnemy(IHealthObject target) =>  enemy = target;
    protected void OnEnable()
    {
        stolenSlaps = 1;
        rb.isKinematic = true;
        CanWalk = true;
        canAttack = true;
        isDead = false;
        canGetDamage = true;
        speed = navMeshAgent.speed;

        GetNearnestEnemyAsTarget();
        Move(target);

        eventManager.SubscribeOnEnemyDeath(OnEnemyDeath);
        StartCoroutine(WaitTimeBeforeAttackIntoStart());

        navMeshAgent.updateRotation = false;
    }

    private IEnumerator WaitTimeBeforeAttackIntoStart()
    {
        canAttack = false;
        yield return new WaitForSeconds(timeNextToAttack);
        canAttack = true;
    }
    protected IEnumerator WaitBeforeAttack()
    {
        canAttack = false;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.6f);
        slapAudio.Play();
        yield return new WaitForSeconds(0.05f);
        Attack();
        yield return new WaitForSeconds(timeNextToAttack);
        canAttack = true;
    }
    protected virtual void Update()
    {
        if (!CanWalk || isDead || isSleeping) return;
        if (!navMeshAgent.isOnNavMesh) 
        {
            rb.isKinematic = false;
            return;
        }

        rb.isKinematic = true;

        if(enemy != null && !enemy.IsDead)
        {
            target = enemy.transform.position;
            Move(target);
            InstantlyTurn(target);

            if ((target - transform.position).magnitude < distanseToAttack && canAttack && IsInSight())
            {
                StartCoroutine(WaitBeforeAttack());
            }
        }

        else
        {
            GetNearnestEnemyAsTarget();
            Move(target);
        }

        animator.SetFloat("HorizontalSpeed", navMeshAgent.speed);
    }
    private void InstantlyTurn(Vector3 destination)
    {
        //When on target -> dont rotate!
        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        direction.y = 0;

        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * navMeshAgent.angularSpeed);
    }
    protected bool IsInSight()
    {
        Vector3 to = (enemy.transform.position - transform.position).normalized;
        Vector3 from = transform.forward;
        float angle = Vector3.Angle(from, to);

        return  angle < sightAngle;
    }

    protected virtual void Attack()
    {
        if ((target - transform.position).magnitude > distanseToAttack || !IsInSight()) return;

        enemy.GetDamage(damagePower, (target - transform.position).normalized, out bool isDeath, out int gettedSlap);
        OnSuccesAttack();


        if(isDeath)
        {
            enemy = null;
            GetNearnestEnemyAsTarget();
        }
        stolenSlaps += gettedSlap;

    }

    protected virtual void OnSuccesAttack()
    {

    }

    protected void Move(Vector3 targetPosition)
    {
        navMeshAgent.SetDestination(targetPosition);
    }

    protected void GetNearnestEnemyAsTarget()
    {
        Collider[] colls = Physics.OverlapSphere(transform.position, 1000f, enemyLayer);

        float minDistanse = Mathf.Infinity;
        enemy = null;

        foreach(Collider coll in colls)
        {
            float distanse = Vector3.Distance(transform.position, coll.transform.position);
            if(distanse < minDistanse)
            {
                IHealthObject healthObject = coll.GetComponent<IHealthObject>();
                if (healthObject != this)
                {
                    minDistanse = distanse;
                    enemy = healthObject;
                    target = enemy.transform.position;
                }
            }
        }

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
            StartCoroutine(GetDamageAnimation(direction));
        }
        gettedSlap = slapToGive;
        isDeath = health <= 0;
    }

    public override void GetDamageWithoutRebound(float damagePower, out bool isDeath, out int gettedSlap)
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

        if (navMeshAgent.hasPath)
            navMeshAgent.ResetPath();

        if (health <= 0)
        {
            Death();
        }
        else
        {
            OnEndAnimations();
        }
        gettedSlap = slapToGive;
        isDeath = health <= 0;
    }
    protected void OnEndAnimations()
    {
        GetNearnestEnemyAsTarget();
        canGetDamage = true;
    }
    
    public IEnumerator GetDamageAnimation(Vector3 direction)
    {
        navMeshAgent.enabled = false;
        rb.isKinematic = false;
        CanWalk = false;
        direction.y = 1f;
        rb.AddForce(direction * 30000);

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
        isSleeping = false;
        navMeshAgent.speed = speed;
        Canvas.SetActive(true);
    }

    public override void Death(bool playDeathAnimation = true)
    {
        if (isDead) return;
        navMeshAgent.enabled = false;
        isDead = true;

        if (playDeathAnimation)
        {
            animator.SetTrigger("Death");
            Canvas.SetActive(false);
            deathAudio.Play();
        }

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
                GetNearnestEnemyAsTarget(); 
                Move(target);
            }
        }
    }

    public void Sleep(float timeToWakeUp)
    {
        if (isSleeping) return;
        animator.SetTrigger("Sleep");
        navMeshAgent.enabled = false;
        //speed = navMeshAgent.speed;
        //navMeshAgent.speed = 0;
        isSleeping = true;
        rb.isKinematic = true;
        rb.velocity = Vector3.zero;

        StartCoroutine(WakeUp(timeToWakeUp));
    }


    private IEnumerator WakeUp(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        if (!isDead)
        {
            animator.SetTrigger("Revive");
            navMeshAgent.enabled = true;
        }
            //navMeshAgent.speed = speed;
            isSleeping = false;
    }


    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out IHealthObject healthObject))
        {
            enemy = healthObject;
        }
    }
}
