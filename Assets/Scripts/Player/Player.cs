using CMF;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : IHealthObject
{
    [Header("Components")]
    [SerializeField] private AdvancedWalkerController walkController;
    [SerializeField] private Mover playerMover;
    [SerializeField] private Image healthbar;
    [SerializeField] private Rigidbody rb;

    [SerializeField] private float health;
    [SerializeField] private float maxHealth;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        walkController = GetComponent<AdvancedWalkerController>();
        playerMover = GetComponent<Mover>();
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
        //rb.isKinematic = false;
        walkController.enabled = false;
        //playerMover.enabled = false;
        direction.y = 0.8f;
        rb.AddForce(direction * damagePower * 25);

        yield return new WaitForSeconds(2);

        //rb.isKinematic = true;
        walkController.enabled = true;
        //playerMover.enabled = true;

        OnEndAnimations();
    }

    private void OnEndAnimations()
    {

    }
}
