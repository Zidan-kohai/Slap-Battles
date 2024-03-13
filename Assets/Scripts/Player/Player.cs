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
    [SerializeField] private GameObject playerCanvas;

    [Header("Audio")]
    [SerializeField] protected AudioSource deathAudio;
    [SerializeField] protected AudioSource fallingAudio;


    [Space(10), Header("Slap")]
    [SerializeField] private int slapToGive;
    [SerializeField] protected int stolenSlaps;

    private bool isDead = false;
    private int deadCounter;
    private float movementSpeed;
    private float jumpSpeed;
    [SerializeField] private bool isImmortall;

    public bool SetImmortall { get => isImmortall;  set => isImmortall = value;  }

    protected void Start()
    {
        isImmortall = false;
        stolenSlaps = 0;
        rb = GetComponent<Rigidbody>();
        walkController = GetComponent<AdvancedWalkerController>();
        jumpSpeed = walkController.jumpSpeed;
        if (Geekplay.Instance.currentMode != Modes.Hub)
            eventManager.SubscribeOnPlayerRevive(Revive);


        if(Geekplay.Instance.BuffAcceleration)
        {
            walkController.movementSpeed *= 2;
        }
        movementSpeed = walkController.movementSpeed;

        if(Geekplay.Instance.BuffIncreaseHP)
        {
            maxHealth *= 2;
            currenthealth = maxHealth;
        }
        healthbar.fillAmount = (currenthealth / maxHealth);
    }
    public override void GetDamage(float damagePower, Vector3 direction, out bool isDeath, out int stoledSlap)
    {
        if(isImmortall)
        {
            isDeath = false;
            stoledSlap = 0;

            return;
        }
        currenthealth -= damagePower;
        healthbar.fillAmount = (currenthealth / maxHealth);

        if (currenthealth <= 0)
        {
            Death();
        }
        else
        {
            StartCoroutine(GetDamageAnimation(direction));
        }
        stoledSlap = slapToGive;
        isDeath =  currenthealth <= 0;
    }

    public override void GetDamageWithoutRebound(float damagePower, out bool isDeath, out int gettedSlap)
    {
        if (isImmortall)
        {
            isDeath = false;
            gettedSlap = 0;

            return;
        }
        currenthealth -= damagePower;
        healthbar.fillAmount = (currenthealth / maxHealth);

        if (currenthealth <= 0)
        {
            Death();
        }
        else
        {
            OnEndAnimations();
        }
        gettedSlap = slapToGive;
        isDeath = currenthealth <= 0;
    }

    public override void Death(bool playDeathAnimation = true)
    {
        if (isDead) return;

        slapPower.enabled = false;
        deadCounter++;
        isDead = true;
        walkController.movementSpeed = 0;
        walkController.jumpSpeed = 0;
        if (playDeathAnimation)
        {
            animator.SetTrigger("Death");
            playerCanvas.SetActive(false);
            deathAudio.Play();
        }
        else
        {
            fallingAudio.Play();
        }


        eventManager.InvokePlayerDeathEvents(deadCounter, stolenSlaps);
    }
    public void Revive()
    {
        isDead = false;
        slapPower.enabled = true;
        walkController.movementSpeed = movementSpeed;
        walkController.jumpSpeed = jumpSpeed;
        animator.SetTrigger("Revive");
        currenthealth = maxHealth;
        healthbar.fillAmount = (currenthealth / maxHealth);
    }
    public IEnumerator GetDamageAnimation(Vector3 direction)
    {
        walkController.enabled = false;
        direction.y = 0.2f;
        rb.AddForce(direction * 1000);

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
