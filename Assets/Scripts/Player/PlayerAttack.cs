using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Slap slap;

    [SerializeField] private float timeToNextAttack;
    [SerializeField] private bool canAttack = true;

    private void Start()
    {
        slap = GetComponentInChildren<Slap>();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && canAttack)
        {
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
        slap = newSlap;
    }
}
