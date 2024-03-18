using UnityEngine;
using UnityEngine.AI;

public class WallPusher : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;

    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            if(collision.gameObject.TryGetComponent(out NavMeshAgent agent))
            {
                agent.enabled = false;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            if (collision.gameObject.TryGetComponent(out NavMeshAgent agent))
            {
                agent.enabled = true;
            }
        }
    }
}
