using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    public int RaycastCount = 10;
    public float MaxAngle = 60f;
    public float RaycastDistance = 2f;
    public Vector3 Delta;

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
        if(Input.GetMouseButtonDown(0) && canAttack && !Geekplay.Instance.mobile)
        {
            StartCoroutine(WaitBeforeAttack());
        }

        else if (Input.GetKeyDown(KeyCode.E))
        {
            slap.SuperAttack();
        }
    }
    public void MobileAttack()
    {
        if(canAttack)
            StartCoroutine(WaitBeforeAttack());
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
        yield return new WaitForSeconds(0.4f);
        Attack();


        yield return new WaitForSeconds(timeToNextAttack);
        canAttack = true;

    }

    public virtual void Attack()
    {
        CastRaycasts();
    }
    private void CastRaycasts()
    {
        float startAngle = -MaxAngle / 2;

        float angleStep = MaxAngle / (RaycastCount - 1);

        List<Enemy> slappedenemies = new List<Enemy>(); 

        for (int i = 0; i < RaycastCount; i++)
        {
            float currentAngle = startAngle + angleStep * i;

            Vector3 raycastDirection = Quaternion.Euler(0, currentAngle, 0) * slapRaycaster.forward;

            RaycastHit hit;
            if (Physics.SphereCast(slapRaycaster.position + Delta, 0.5f,raycastDirection, out hit, RaycastDistance, enemyLayer, QueryTriggerInteraction.Ignore))
            {
                Debug.Log("Raycast hit at angle " + currentAngle + " degrees. Hit object: " + hit.collider.gameObject.name);

                Enemy enemy = hit.collider.GetComponent<Enemy>();


                if(!slappedenemies.Contains(enemy))
                {
                    slappedenemies.Add(enemy);
                    OnFindEnemyWhenAttack(enemy);
                    slapAudio.Play();
                }
            }
            else
            {
                Debug.Log("Raycast at angle " + currentAngle + " degrees didn't hit anything");
            }
        }
    }

    private void OnFindEnemyWhenAttack(Enemy enemy)
    {
        slapCount++;
        float attackPower = slap.AttackPower;

        if (Geekplay.Instance.BuffIncreasePower)
            attackPower *= 2;

        enemy.GetDamage(attackPower, slapRaycaster.forward, out bool isDeath, out int GettedSlap);

        if (slap.GetSlapPowerType() == SlapPowerType.DoubleSlap || Geekplay.Instance.BuffDoubleSlap)
        {
            GettedSlap *= 2;
        }

        Geekplay.Instance.PlayerData.LeaderboardSlap += GettedSlap;

        player.SetStolenSlaps(GettedSlap);
        eventManager.InvokeChangeMoneyEvents(GettedSlap);
        OnSuccesAttack();

        if (slapCount % 30 == 0 && Geekplay.Instance.currentMode != Modes.Boss)
        {
            UIDiamond.Run();
            eventManager.InvokeChangeDiamondEvents(1);
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
        // �������� � ����, ������� ������������� �������� ������������� ����
        float startAngle = -MaxAngle / 2;

        // ��������� ��� ��������� ���� ��� ������� ��������
        float angleStep = MaxAngle / (RaycastCount - 1);

        for (int i = 0; i < RaycastCount; i++)
        {
            // ��������� ���� ��� �������� ��������
            float currentAngle = startAngle + angleStep * i;

            // ��������� ����������� ��� ������ �������� �� ������ ����
            Vector3 raycastDirection = Quaternion.Euler(0, currentAngle, 0) * slapRaycaster.forward;
            Debug.DrawRay(slapRaycaster.position + Delta, raycastDirection * RaycastDistance);
        }
    }

    private void OnDisable()
    {
        slap.gameObject.SetActive(false);
    }
}
