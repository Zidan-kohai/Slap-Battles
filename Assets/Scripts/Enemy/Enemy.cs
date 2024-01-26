using JetBrains.Annotations;
using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Rigidbody))]
public class Enemy : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Image healthbar;
    
    [SerializeField] private float speed;
    [SerializeField] private int attackPower;
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    [SerializeField] private GameObject enemy;
    [SerializeField] private Vector3 target;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;

        GetRandomTarget();
    }

    private void Update()
    {
        if(enemy == null && (target - transform.position).magnitude < 1f)
        {
            GetRandomTarget();
        }
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

        Move(target);
    }

    public void GetDamage(int damagePower, Vector3 direction)
    {
        health -= damagePower;
        healthbar.fillAmount = (health / maxHealth);

        if(navMeshAgent.hasPath)
            navMeshAgent.ResetPath();

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(GetDamageAnimation(direction, damagePower));
        }
    }

    private void OnEndAnimations()
    {
        GetRandomTarget();
    }
    
    public IEnumerator GetDamageAnimation(Vector3 direction, int damagePower)
    {
        navMeshAgent.enabled = false;
        rb.isKinematic = false;
        direction.y = 0.8f;
        rb.AddForce(direction * damagePower * 25);

        yield return new WaitForSeconds(2);

        rb.isKinematic = true;
        navMeshAgent.enabled = true;

        OnEndAnimations();
    }
}
