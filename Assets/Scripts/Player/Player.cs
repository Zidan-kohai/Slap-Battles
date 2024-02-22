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
    [SerializeField] protected EventManager eventManager;
    [SerializeField] private Animator animator;
    [SerializeField] private GameObject modelHandler;
    [SerializeField] private SlapPower slapPower;

    [Space(10), Header("Slap")]
    [SerializeField] private int slapToGive;
    [SerializeField] protected int stolenSlaps;

    private bool isDead = false;
    private int deadCounter;
    [SerializeField] private bool isImmortall;

    public bool SetImmortall { get => isImmortall;  set => isImmortall = value;  }

    protected void Start()
    {
        isImmortall = false;
        stolenSlaps = 0;
        rb = GetComponent<Rigidbody>();
        walkController = GetComponent<AdvancedWalkerController>();

        if(Geekplay.Instance.currentMode != Modes.Hub)
            eventManager.SubscribeOnPlayerRevive(Revive);

        healthbar.fillAmount = (health / maxHealth);
    }
    public override void GetDamage(float damagePower, Vector3 direction, out bool isDeath, out int stoledSlap)
    {
        if(isImmortall)
        {
            isDeath = false;
            stoledSlap = 0;

            return;
        }
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

        slapPower.enabled = false;
        deadCounter++;
        isDead = true;
        walkController.enabled = false;
        animator.SetTrigger("Death");
        eventManager.InvokePlayerDeathEvents(deadCounter, stolenSlaps);
    }
    public void Revive()
    {
        isDead = false;
        slapPower.enabled = true;
        walkController.enabled = true;
        animator.SetTrigger("Revive");
        health = maxHealth;

        healthbar.fillAmount = (health / maxHealth);
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

    //public void Disable()
    //{
    //    isImmortall = true;
    //    walkController.enabled = false;
    //    modelHandler.SetActive(false);
    //}

    //internal void Show()
    //{
    //    isImmortall = false;
    //    walkController.enabled = true;
    //    modelHandler.SetActive(true);
    //}
}
