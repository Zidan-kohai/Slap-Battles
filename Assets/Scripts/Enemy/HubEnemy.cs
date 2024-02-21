using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HubEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    [SerializeField] private float distanceToWalkNextPosition;
    [SerializeField] private List<Transform> positions;
    [SerializeField] private Vector3 target;

    private void Start()
    {
        GetRandomPosition();
        Move();
    }
    private void Update()
    {
        if ((transform.position - target).magnitude < distanceToWalkNextPosition)
        {
            GetRandomPosition();
            Move();
        }
        animator.SetFloat("HorizontalSpeed", navMeshAgent.speed);
    }


    private void Move()
    {
        navMeshAgent.SetDestination(target);
    }


    private void GetRandomPosition()
    {
        target = positions[Random.Range(0, positions.Count)].position;
    }
}
