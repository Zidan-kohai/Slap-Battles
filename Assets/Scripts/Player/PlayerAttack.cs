using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private DiamondUIGettedShow UIDiamond;

    private const string ISATTACK = "IsAttack";

    [SerializeField] private Slap slap;

    [SerializeField] private float timeToNextAttack;
    [SerializeField] private bool canAttack = true;

    [Header("Slap")]
    [SerializeField] private Transform slapHandlerTransform;
    [SerializeField] private Transform slapRaycaster;
    [SerializeField] private int slapCount;
    
    [Header("Attack")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private float attackDistanese;

    [Header("Components")]
    [SerializeField] protected Player player;
    [SerializeField] protected EventManager eventManager;
    [SerializeField] protected Animator animator;

    [Header("Audio")]
    [SerializeField] protected AudioSource slapAudio;
    private void OnEnable()
    {
        slap.gameObject.SetActive(true);
    }

    private void Start()
    {
        slap = GetComponentInChildren<Slap>();
    }


    protected virtual void Update()
    {
        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(WaitBeforeAttack());
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            slap.SuperAttack();
        }
    }

    public void ChangeEventManager(EventManager eventManager)
    {
        this.eventManager = eventManager;
        Debug.Log(eventManager);
    }
    private IEnumerator WaitBeforeAttack()
    {
        canAttack = false;
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.6f);
        slapAudio.Play();
        yield return new WaitForSeconds(0.05f);
        Attack();


        yield return new WaitForSeconds(timeToNextAttack);
        canAttack = true;

    }

    public virtual void Attack()
    {
        RaycastHit hit;

        if (Physics.SphereCast(slapRaycaster.position - slapRaycaster.forward * 0.2f, 0.5f, slapRaycaster.forward, out hit, attackDistanese, enemyLayer, QueryTriggerInteraction.Ignore))
        {
            slapCount++;
            float attackPower = slap.AttackPower;

            if (Geekplay.Instance.BuffIncreasePower)
                attackPower *= 2; 

            hit.collider.gameObject.GetComponent<Enemy>().GetDamage(attackPower, slapRaycaster.forward, out bool isDeath, out int GettedSlap);

            if(slap.GetSlapPowerType() == SlapPowerType.DoubleSlap || Geekplay.Instance.BuffDoubleSlap)
            {
                GettedSlap *= 2;
            }

            player.SetStolenSlaps(GettedSlap);
            eventManager.InvokeChangeMoneyEvents(GettedSlap);
            OnSuccesAttack();

            if(slapCount % 20 == 0 && Geekplay.Instance.currentMode != Modes.Boss)
            {
                UIDiamond.Run();
                eventManager.InvokeChangeDiamondEvents(1);
            }
        }
    }

    protected virtual void OnSuccesAttack()
    {

    }

    public void ChangeSlap(Slap newSlap)
    {
        slap = newSlap;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawRay(slapRaycaster.position - slapRaycaster.forward * 0.2f, slapRaycaster.forward * attackDistanese);

        Gizmos.DrawWireSphere(slapRaycaster.position - slapRaycaster.forward * 0.2f, 0.5f);
        Gizmos.DrawWireSphere(slapRaycaster.position + slapRaycaster.forward * attackDistanese - slapRaycaster.forward * 0.2f, 0.5f);
    }

    private void OnDisable()
    {
        slap.gameObject.SetActive(false);
    }
}
