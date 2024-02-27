using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private const string ISATTACK = "IsAttack";

    [SerializeField] private Slap slap;

    [SerializeField] private float timeToNextAttack;
    [SerializeField] private bool canAttack = true;

    [Header("Slap")]
    [SerializeField] private Transform slapHandlerTransform;
    [SerializeField] private Transform slapRaycaster;
    
    [Header("Attack")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int attackDistanese;

    [Header("Components")]
    [SerializeField] protected Player player;
    [SerializeField] protected EventManager eventManager;

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
            Attack();
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

    protected virtual void Attack()
    {
        RaycastHit hit;

        if (Physics.SphereCast(slapRaycaster.position, 0.7f, slapRaycaster.forward, out hit, attackDistanese, enemyLayer))
        {
            slapAudio.Play();
            hit.collider.gameObject.GetComponent<Enemy>().GetDamage(slap.AttackPower, slapRaycaster.forward, out bool isDeath, out int GettedSlap);

            if(slap.GetSlapPowerType() == SlapPowerType.DoubleSlap)
            {
                GettedSlap *= 2;
            }

            player.SetStolenSlaps(GettedSlap);
            eventManager.InvokeChangeMoneyEvents(GettedSlap);
            OnSuccesAttack();
        }
        StartCoroutine(AttackAnimation());
    }

    protected virtual void OnSuccesAttack()
    {

    }

    public IEnumerator AttackAnimation()
    {
        canAttack = false;
        //slap.Attack();
        yield return new WaitForSeconds(timeToNextAttack);
        canAttack = true;
    }

    public void ChangeSlap(Slap newSlap)
    {
        slap = newSlap;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(slapRaycaster.position, slapRaycaster.forward * attackDistanese);
        Gizmos.DrawSphere(slapRaycaster.position, 0.7f);
    }

    private void OnDisable()
    {
        slap.gameObject.SetActive(false);
    }
}
