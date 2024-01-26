using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<IHealthObject>(out IHealthObject healthObject))
        {
            healthObject.Death();
        }
    }
}
