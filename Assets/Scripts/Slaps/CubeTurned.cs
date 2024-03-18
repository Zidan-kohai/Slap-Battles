using UnityEngine;

public class CubeTurned : MonoBehaviour
{
    public Rigidbody rb;

    private void OnEnable()
    {
        rb.useGravity = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == 10)
        {
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
        }
    }
}
