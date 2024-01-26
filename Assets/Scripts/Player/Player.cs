using CMF;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : IHealthObject
{
    [Header("Components")]
    [SerializeField] private AdvancedWalkerController walkController;
    [SerializeField] private Image healthbar;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        walkController = GetComponent<AdvancedWalkerController>();
    }

    public override void GetDamage(float damagePower, Vector3 direction)
    {
        health -= damagePower;
        healthbar.fillAmount = (health / maxHealth);


        if (health <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(GetDamageAnimation(direction, damagePower));
        }
    }

    public IEnumerator GetDamageAnimation(Vector3 direction, float damagePower)
    {
        walkController.enabled = false;
        direction.y = 0.2f;
        rb.AddForce(direction * damagePower * 70);

        yield return new WaitForSeconds(0.5f);

        walkController.enabled = true;

        OnEndAnimations();
    }

    private void OnEndAnimations()
    {

    }
}
