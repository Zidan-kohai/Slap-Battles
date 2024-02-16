using CMF;
using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Player : IHealthObject
{
    [Header("Components")]
    [SerializeField] private AdvancedWalkerController walkController;
    [SerializeField] private Image healthbar;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private Animator animator;

    [Space(10), Header("Slap")]
    [SerializeField] private int slapToGive;
    [SerializeField] protected int stolenSlaps;

    private bool isDead = false;

    protected void Start()
    {
        stolenSlaps = 0;
        rb = GetComponent<Rigidbody>();
        walkController = GetComponent<AdvancedWalkerController>();
    }

    public override void GetDamage(float damagePower, Vector3 direction, out bool isDeath, out int stoledSlap)
    {
        health -= damagePower;
        healthbar.fillAmount = (health / maxHealth);

        if (health <= 0)
        {
            Death();
        }
        else
        {
            StartCoroutine(GetDamageAnimation(direction, damagePower));
        }
        stoledSlap = slapToGive;
        isDeath =  health <= 0;
    }
    public override void Death()
    {
        if (isDead) return;

        isDead = true;
        //SceneLoader sceneLoader = new SceneLoader();

        //sceneLoader.LoadScene(0, null);
        walkController.enabled = false;
        animator.SetTrigger("Death");
        eventManager.InvokeActionsOnPlayerDeath();
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

    public virtual void SetStolenSlaps(int value) => stolenSlaps += value;
    public virtual int GetStolenSlaps() => stolenSlaps;

    private void OnEndAnimations()
    {

    }
}
