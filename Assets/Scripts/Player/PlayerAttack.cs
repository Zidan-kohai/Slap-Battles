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
        Ray ray = new Ray(slapRaycaster.position, slapRaycaster.forward);
        if (Physics.Raycast(ray, out hit, attackDistanese, enemyLayer))
        {
            hit.collider.gameObject.GetComponent<Enemy>().GetDamage(slap.AttackPower, ray.direction, out bool isDeath, out int GettedSlap);

            player.SetStolenSlaps(GettedSlap);
            eventManager.InvokeChangeMoneyEvents(player.GetStolenSlaps());
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
        Destroy(slapHandlerTransform.GetChild(0));

        Instantiate(slap.gameObject, slapHandlerTransform);

        slap = newSlap;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(slapRaycaster.position, slapRaycaster.forward * attackDistanese);
    }

    private void OnDisable()
    {
        slap.gameObject.SetActive(false);
    }
}
