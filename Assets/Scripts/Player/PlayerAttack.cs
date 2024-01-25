using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private const string ISATTACK = "IsAttack";

    private Slap slap;

    [SerializeField] private float timeToNextAttack;
    [SerializeField] private bool canAttack = true;

    [Header("Slap")]
    [SerializeField] private Transform slapHandlerTransform;

    [Header("Attack")]
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private int attackDistanese;


    [Header("Components")]
    [SerializeField] private EventManager eventManager;
    private void Start()
    {
        slap = GetComponentInChildren<Slap>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canAttack)
        {

            if(Physics.Raycast(slapHandlerTransform.position, transform.forward, attackDistanese, enemyLayer))
            {
                Debug.Log(true);
                Geekplay.Instance.PlayerData.money++;
                eventManager.InvokeActionsOnChangeMoney(Geekplay.Instance.PlayerData.money);
            }
            StartCoroutine(Attack());
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            slap.SuperAttack();
        }
    }

    public IEnumerator Attack()
    {
        canAttack = false;
        slap.Attack();
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
        Gizmos.DrawRay(slapHandlerTransform.position, transform.forward * attackDistanese);
    }
}
