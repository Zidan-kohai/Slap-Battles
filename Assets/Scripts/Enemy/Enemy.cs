using CrazyGames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
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
    
    [SerializeField] private float startSpeed;
    [SerializeField] private int damagePower;
    [SerializeField] protected float distanseToAttack;
    [SerializeField] protected bool CanWalk;
    [SerializeField] protected bool isSleeping;
    [SerializeField] private float timeNextToAttack;
    [SerializeField] protected bool canAttack;
    [SerializeField] protected GameObject sleepText;

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

    [Header("SuperPower")]
    public Slap slap;
    public float timeToUseSuperPower;
    public float lastedTimeFromLastUseSuperPower;
    public bool isPowerActivated;
    public GameObject enemyModelHandler;

    [Header("Wall Power")]
    [SerializeField] private GameObject wallGameobject;
    [SerializeField] private List<Collider> playerCollider;

    [Header("Pusher")]
    [SerializeField] private Transform wallPusherStartPosition;
    [SerializeField] private GameObject wallPusher;

    [Header("Lego")]
    [SerializeField] private GameObject legoSphere;
    [SerializeField] private Transform legoSpherePosition;
    private bool Immortall;
    public bool isBoss = false;
    protected void OnEnable()
    {
        stolenSlaps = 1;
        rb.isKinematic = true;
        CanWalk = true;
        canAttack = true;
        isDead = false;
        canGetDamage = true;
        startSpeed = navMeshAgent.speed;

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
        if (!CanWalk || isDead || isSleeping || slap.GetSlapPowerType() == SlapPowerType.Wall && isPowerActivated) return;
        if (!navMeshAgent.isOnNavMesh) 
        {
            rb.isKinematic = false;
            return;
        }

        lastedTimeFromLastUseSuperPower += Time.deltaTime;

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
            InstantlyTurn(target);
        }
        animator.SetFloat("HorizontalSpeed", navMeshAgent.speed);
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

        if(lastedTimeFromLastUseSuperPower > timeToUseSuperPower && !isPowerActivated && !isBoss)
        {
            lastedTimeFromLastUseSuperPower = 0;
            switch(slap.GetSlapPowerType())
            {
                case SlapPowerType.Wall:
                    WallPowerActivate();
                    break;

                case SlapPowerType.Lego:
                    LegoPowerActivate();
                    break;

                case SlapPowerType.Pusher:
                    PusherPowerActivate();
                    break;

                case SlapPowerType.Accelerator:
                    AcceleratorPowerActive();
                    break;
            }
            return;
        }

        enemy.GetDamageWithoutRebound(damagePower, out bool isDeath, out int gettedSlap);
        OnSuccesAttack();


        if(isDeath)
        {
            enemy = null;
            GetNearnestEnemyAsTarget();
        }
        stolenSlaps += gettedSlap;

    }

    #region SuperPower

    #region Wall
    private void WallPowerActivate()
    {
        isPowerActivated = true;
        wallGameobject.SetActive(true);
        enemyModelHandler.SetActive(false);
        Immortall = true;
        canGetDamage = false;
        startSpeed = navMeshAgent.speed;
        navMeshAgent.speed = 0;
        navMeshAgent.velocity = Vector3.zero;

        Canvas.SetActive(false);

        foreach (Collider c in playerCollider)
            c.enabled = false;

        wallGameobject.transform.position = gameObject.transform.position;

        StartCoroutine(DiactivatePower(slap.rollBackTime, WallPowerDisactivate));
    }

    private void WallPowerDisactivate()
    {
        isPowerActivated = false;
        wallGameobject.SetActive(false);
        enemyModelHandler.SetActive(true);
        Immortall = false;
        canGetDamage = true;
        navMeshAgent.speed = startSpeed;
        navMeshAgent.isStopped = false;
        Canvas.SetActive(true);

        navMeshAgent.ResetPath();

        foreach (Collider c in playerCollider)
            c.enabled = true;

        gameObject.transform.position = wallGameobject.transform.position;
        GetNearnestEnemyAsTarget();
    }

    #endregion

    #region Pusher

    private void PusherPowerActivate()
    {
        isPowerActivated = true;

        wallPusher.transform.position = wallPusherStartPosition.position;
        wallPusher.transform.parent = null;
        wallPusher.transform.forward = enemyModelHandler.transform.forward;
        wallPusher.SetActive(true);
        StartCoroutine(DiactivatePower(slap.rollBackTime, PusherPowerDiactivate));
    }

    private void PusherPowerDiactivate()
    {
        isPowerActivated = false;
        wallPusher.SetActive(false);
    }
    #endregion

    #region Accelerator

    private void AcceleratorPowerActive()
    {
        isPowerActivated = true;
        startSpeed = navMeshAgent.speed;

        navMeshAgent.speed = startSpeed * 2;

        StartCoroutine(DiactivatePower(slap.rollBackTime, DiactivaetAcceleratorPower));
    }

    private void DiactivaetAcceleratorPower()
    {
        isPowerActivated = false;
        navMeshAgent.speed = startSpeed;
    }

    #endregion

    #region Lego

    private void LegoPowerActivate()
    {
        isPowerActivated = true;

        legoSphere.SetActive(true);

        legoSphere.transform.position = legoSpherePosition.transform.position;
        legoSphere.transform.parent = null;

        StartCoroutine(DiactivatePower(slap.rollBackTime, LegoPowerDisactivate));
    }

    private void LegoPowerDisactivate()
    {
        isPowerActivated = false;
        legoSphere.SetActive(false);
    }

    #endregion

    private IEnumerator DiactivatePower(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);

        action?.Invoke();
    }
    #endregion
    private void InstantlyTurn(Vector3 destination)
    {
        if ((destination - transform.position).magnitude < 0.1f) return;

        Vector3 direction = (destination - transform.position).normalized;
        direction.y = 0;
        Quaternion qDir = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, qDir, Time.deltaTime * 3);
    }
    protected virtual void OnSuccesAttack()
    {
        GetNearnestEnemyAsTarget();
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
                if(coll.TryGetComponent(out IHealthObject healthObject) && healthObject != this)
                {
                    minDistanse = distanse;
                    enemy = healthObject;
                    target = enemy.transform.position;
                }
            }
        }
    }

    public override void GetDamage(float damagePower, Vector3 direction, out bool isDeath, out int gettedSlap)
    {
        if (isDead || Immortall)
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

        rb.isKinematic = true;
        navMeshAgent.enabled = true;
        CanWalk = true;
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
        isDead = false;
        canGetDamage = true;
        isSleeping = false;
        navMeshAgent.speed = startSpeed;
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
        sleepText.SetActive(true);

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
            sleepText.SetActive(false);

        }
            //navMeshAgent.speed = speed;
            isSleeping = false;
    }

    public void SnowyActivate()
    {
        animator.SetBool("snowy", true);
    }

    public void SnowyDiactivate()
    {
        animator.SetBool("snowy", false);
    }

    public void ShookerDamageActivate()
    {
        animator.SetTrigger("Shooker");
    }
    //protected virtual void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.TryGetComponent(out IHealthObject healthObject))
    //    {
    //        enemy = healthObject;
    //    }
    //}
}


public enum BotState
{
    FindEnemy,
    HealtSmall,
}