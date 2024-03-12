using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    public Player player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent<IHealthObject>(out IHealthObject healthObject))
        {
            healthObject.Death(false);
        }
        else if(other.gameObject.layer == 7)
        {
            player.Death();
        }
    }
}
