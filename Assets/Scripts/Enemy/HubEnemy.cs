using UnityEngine;
using UnityEngine.AI;

public class HubEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Animator animator;
    [SerializeField] private float distanceToWalkNextPosition;
    [SerializeField] private Transform MaxPosition, MinPosition;
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
        target = new Vector3(Random.Range(MinPosition.position.x, MaxPosition.position.x), 0, Random.Range(MinPosition.position.z, MaxPosition.position.z));
    }
}
