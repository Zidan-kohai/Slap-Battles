using JetBrains.Annotations;
using System.Collections;
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

        Vector3 direction = (target - transform.position).normalized;
        Move(direction);
        //transform.forward = new Vector3(direction.x, 0f, direction.z);
    }


    private void Move(Vector3 direction)
    {
        navMeshAgent.Move(direction * speed * Time.deltaTime);
    }

    private void GetRandomTarget()
    {
        target = new Vector3(Random.Range(transform.position.x - 10, transform.position.x + 10),
            transform.position.y,
            Random.Range(transform.position.x - 10, transform.position.x + 10));
    }

    public void GetDamage(int damagePower, Vector3 direction)
    {
        health -= damagePower;
        healthbar.fillAmount = (health / maxHealth);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            GetDamageAnimation(direction);
        }
    }

    
    public IEnumerator GetDamageAnimation(Vector3 direction)
    {
        rb.isKinematic = false;
        navMeshAgent.enabled = false;
        rb.AddForce(direction * 10);

        yield return new WaitForSeconds(2);

        rb.isKinematic = true;
        navMeshAgent.enabled = true;
    }
}
