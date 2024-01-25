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

    [Header("Animation")]
    [SerializeField] private Animator animator;
    private void Start()
    {
        slap = GetComponentInChildren<Slap>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canAttack)
        {
            animator.SetTrigger(ISATTACK);
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
}
