using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class WallPusher : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;

    void Update()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == 6)
        {
            collision.transform.Translate(collision.transform.position - transform.forward);
        }
    }
}
