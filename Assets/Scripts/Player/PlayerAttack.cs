using System.Collections;
using UnityEngine;

public class PlayerAttack : Player
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
    [SerializeField] protected EventManager eventManager;

    [SerializeField] private int moneyRewardOnSlap;

    private void OnEnable()
    {
        slap.gameObject.SetActive(true);
    }

    private void Start()
    {
        slap = GetComponentInChildren<Slap>();
    }


    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            RaycastHit hit;
            Ray ray = new Ray(slapRaycaster.position, slapRaycaster.forward);
            if (Physics.Raycast(ray, out hit, attackDistanese, enemyLayer))
            {
                Debug.Log(true);
                eventManager.InvokeActionsOnChangeMoney(moneyRewardOnSlap);

                hit.collider.gameObject.GetComponent<Enemy>().GetDamage(slap.AttackPower, ray.direction, out bool isDeath);
            }
            StartCoroutine(Attack());
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


    public IEnumerator Attack()
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
