using UnityEngine;

public class LegoItem : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Enemy enemy = other.GetComponent<Enemy>();

            enemy.GetDamage(15f, (enemy.transform.position - transform.position).normalized, out bool isDeath, out int gettedSlap);
            player.SetStolenSlaps(gettedSlap);
        }
    }
}
