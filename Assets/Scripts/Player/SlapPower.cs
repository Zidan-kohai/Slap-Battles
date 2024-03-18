using CMF;  
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlapPower : MonoBehaviour
{
    [SerializeField] private Timer timer;

    [SerializeField] private AdvancedWalkerController playerWalkController;
    [SerializeField] private SmoothPosition cameraSmoothPosition;
    [SerializeField] private SmoothPosition playerSmoothPosition;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private Player player;
    [SerializeField] private Slap slap;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject playerModelHandler;
    private float startSpeed;
    private float startPower;
    public void ChangeSlap(Slap slap) => this.slap = slap;

    private bool isPowerActivated;

    public bool HasSuperPower {  get => slap.GetSlapPowerType() != SlapPowerType.Standart; }

    [Header("Wall Power")]
    [SerializeField] private GameObject wallGameobject;
    [SerializeField] private List<Collider> playerCollider;

    [Header("Sleeply Power")]
    [SerializeField] private float sleeplyPowerSphereRadius;


    [Header("Lego")]
    [SerializeField] private GameObject legoSphere;
    [SerializeField] private Transform legoSpherePosition;

    [Header("Snowy")]
    [SerializeField] private float snowyPowerSphereRadius;
    [SerializeField] private float freezingFactor;
    private Collider[] freezedEnemies;

    [Header("Teleport")]
    [SerializeField] private Collider teleportArea;
    [Header("Time")]
    [SerializeField] private Vector3 activatePosition;

    [Header("Shooker")]
    [SerializeField] private float radiusSphereOfShookerPower;

    [Header("Pusher")]
    [SerializeField] private Transform wallPusherStartPosition;
    [SerializeField] private GameObject wallPusher;

    [Header("Gold")]
    [SerializeField] private ColorSwitcher colorSwitcher;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !isPowerActivated)
        {
            UsePower();
        }
    }

    public void UsePower()
    {
        switch (slap.GetSlapPowerType())
        {
            case SlapPowerType.Wall:
                WallPowerActivate();
                break;
            case SlapPowerType.Sleepy:
                SleeplyPowerActivate();
                break;
            case SlapPowerType.Lego:
                LegoPowerActivate();
                break;
            case SlapPowerType.Snowy:
                SnowyPowerActivate();
                break;
            case SlapPowerType.Teleport:
                StartCoroutine(TeleportPowerActivate());
                break;
            case SlapPowerType.Time:
                ActivateTimePower();
                break;
            case SlapPowerType.Shocker:
                ActivateShookerPower();
                break;
            case SlapPowerType.Pusher:
                PusherPowerActivate();
                break;
            case SlapPowerType.Magnet:
                ActivateMagnetPower();
                break;
            case SlapPowerType.Accelerator:
                AcceleratorPowerActive();
                break;
            case SlapPowerType.Gold:
                GoldPowerActivate();
                break;
        }
    }

    #region Wall
    private void WallPowerActivate()
    {
        isPowerActivated = true;
        playerWalkController.enabled = false;
        wallGameobject.SetActive(true);
        playerModelHandler.SetActive(false);
        player.SetImmortall = true;
        cameraSmoothPosition.target = wallGameobject.transform;
        foreach(Collider c in playerCollider)
            c.enabled = false;

        wallGameobject.transform.position = player.transform.position + new Vector3(0,3,0);

        StartCoroutine(DiactivatePower(slap.rollBackTime, WallPowerDisactivate));
    }

    private void WallPowerDisactivate()
    {
        isPowerActivated = false;
        playerWalkController.enabled = true;
        wallGameobject.SetActive(false);
        playerModelHandler.SetActive(true);
        cameraSmoothPosition.target = player.transform;
        player.SetImmortall = false;

        foreach (Collider c in playerCollider)
            c.enabled = true;

        player.transform.position = wallGameobject.transform.position;
    }

    #endregion

    #region sleeply

    private void SleeplyPowerActivate()
    {
        Collider[] colls = Physics.OverlapSphere(player.transform.position, sleeplyPowerSphereRadius, enemyLayer);

        foreach (var item in colls)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            enemy.GetDamage(slap.AttackPower, (item.transform.position - player.transform.position).normalized, out bool isDeath, out int gettedSlap);
            player.SetStolenSlaps(gettedSlap);
            eventManager.InvokeChangeMoneyEvents(gettedSlap);

            enemy.Sleep(slap.rollBackTime);
        }

        isPowerActivated = true;

        StartCoroutine(DiactivatePower(slap.rollBackTime, SleeplyPowerDiactivate));
    }

    private void SleeplyPowerDiactivate()
    {
        isPowerActivated = false;
    }

    #endregion

    #region Lego

    private void LegoPowerActivate()
    {
        isPowerActivated = true;

        legoSphere.SetActive(true);

        legoSphere.transform.position = legoSpherePosition.transform.position;
        legoSphere.transform.parent = null;

        StartCoroutine(DiactivatePower(slap.rollBackTime, LegoPowerDisactivate));
    }

    private void LegoPowerDisactivate()
    {
        isPowerActivated = false;
        legoSphere.SetActive(false);
    }

    #endregion

    #region Snowy

    private void SnowyPowerActivate()
    {
        freezedEnemies = Physics.OverlapSphere(player.transform.position, snowyPowerSphereRadius, enemyLayer);

        foreach (var item in freezedEnemies)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            enemy.GetNavMeshAgent.speed /= freezingFactor; 
            enemy.SnowyActivate();

        }

        StartCoroutine(DiactivatePower(slap.rollBackTime, SnowyPowerDisactivate));
    }

    private void SnowyPowerDisactivate()
    {
        foreach (var item in freezedEnemies)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            enemy.GetNavMeshAgent.speed *= freezingFactor;
            enemy.SnowyDiactivate();
        }
    }

    #endregion

    #region Teleport 
    private IEnumerator TeleportPowerActivate()
    {
        isPowerActivated = true;
        playerWalkController.enabled = false;
        yield return new WaitForSeconds(0.6f);

        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        player.transform.position = RandomPointInBounds(teleportArea.bounds);
        StartCoroutine(DiactivatePower(slap.rollBackTime, TeleportDiactivate));

        yield return new WaitForSeconds(0.3f);
        playerWalkController.enabled = true;
    }

    private void TeleportDiactivate()
    {
        isPowerActivated = false;
    }
    public Vector3 RandomPointInBounds(Bounds bounds)
    {
        return new Vector3(
            UnityEngine.Random.Range(bounds.min.x, bounds.max.x),
            UnityEngine.Random.Range(bounds.min.y, bounds.max.y),
            UnityEngine.Random.Range(bounds.min.z, bounds.max.z)
        );
    }
    #endregion

    #region Time
    private void ActivateTimePower()
    {
        activatePosition = player.transform.position;
        isPowerActivated = true;

        StartCoroutine(DiactivatePower(slap.rollBackTime, DiactivateTimePower()));
        Debug.Log($"time power {activatePosition}");

    }

    private IEnumerator DiactivateTimePower()
    {
        playerWalkController.enabled = false;
        yield return new WaitForSeconds(0.6f);
        isPowerActivated = false;
        player.transform.position = activatePosition;
        Debug.Log($"time power {activatePosition}");

        yield return new WaitForSeconds(0.3f);
        playerWalkController.enabled = true;

    }
    #endregion

    #region Shooker

    private void ActivateShookerPower()
    {
        isPowerActivated = true;
        Collider[] colls = Physics.OverlapSphere(player.transform.position, radiusSphereOfShookerPower, enemyLayer);

        foreach (var item in colls)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            enemy.GetDamage(slap.AttackPower, (item.transform.position - player.transform.position).normalized, out bool isDeath, out int gettedSlap);
            player.SetStolenSlaps(gettedSlap);
            eventManager.InvokeChangeMoneyEvents(gettedSlap);

            enemy.ShookerDamageActivate();
        }
        Debug.Log(colls.Length);
        StartCoroutine(DiactivatePower(slap.rollBackTime, DiactivateShookerPower));
    }

    private void DiactivateShookerPower()
    {
        isPowerActivated = false;
        Debug.Log("DiactvatePower");
    }

    #endregion

    #region Pusher

    private void PusherPowerActivate()
    {
        isPowerActivated = true;

        wallPusher.transform.position = wallPusherStartPosition.position;
        wallPusher.transform.parent = null;
        wallPusher.transform.forward = playerModelHandler.transform.forward;
        wallPusher.SetActive(true);
        StartCoroutine(DiactivatePower(slap.rollBackTime, PusherPowerDiactivate));
    }

    private void PusherPowerDiactivate()
    {
        isPowerActivated = false;
        wallPusher.SetActive(false);
    }

    #endregion

    #region Magnet

    private void ActivateMagnetPower()
    {
        isPowerActivated = true;

        Collider[] colls = Physics.OverlapSphere(player.transform.position, 1000f, enemyLayer);

        float distance = Mathf.Infinity;
        Vector3 TeleportPosition = Vector3.zero;

        foreach (var item in colls)
        {
            float dis = (player.transform.position - item.transform.position).magnitude;

            if(dis < distance)
            {
                TeleportPosition = item.transform.position + new Vector3(2, 0, 3);
                distance = dis;
            }
        }

        player.transform.position = TeleportPosition;

        StartCoroutine(DiactivatePower(slap.rollBackTime, DiactivateMagnetPower));
    }

    private void DiactivateMagnetPower()
    {
        isPowerActivated = false;
    }

    #endregion

    #region Accelerator

    private void AcceleratorPowerActive()
    {
        isPowerActivated = true;
        startSpeed = playerWalkController.movementSpeed;

        playerWalkController.movementSpeed = startSpeed * 2;

        StartCoroutine(DiactivatePower(slap.rollBackTime, DiactivaetAcceleratorPower));
    }

    private void DiactivaetAcceleratorPower()
    {
        isPowerActivated = false;
        playerWalkController.movementSpeed = startSpeed;
    }

    #endregion

    #region Gold

    private void GoldPowerActivate()
    {
        isPowerActivated = true;
        startPower = slap.AttackPower;
        startSpeed = playerWalkController.movementSpeed;
        colorSwitcher.GoldPowerActivated();
        slap.AttackPower *= 1.5f;
        playerWalkController.movementSpeed = startSpeed * 2;

        StartCoroutine(DiactivatePower(slap.rollBackTime, DiactivateGoldPower));
    }

    private void DiactivateGoldPower()
    {
        isPowerActivated = false;
        playerWalkController.movementSpeed = startSpeed;
        slap.AttackPower = startPower;
        colorSwitcher.GoldPowerDiactivated();
    }

    #endregion

    private IEnumerator DiactivatePower(float waitTime, Action action)
    {
        timer?.Run(slap.rollBackTime);
        yield return new WaitForSeconds(waitTime);

        action?.Invoke();
    }
    private IEnumerator DiactivatePower(float waitTime, IEnumerator coroutine)
    {
        timer?.Run(slap.rollBackTime);
        yield return new WaitForSeconds(waitTime);

        StartCoroutine(coroutine);
    }

    private void OnDestroy()
    {
        if(slap.GetSlapPowerType() == SlapPowerType.Gold)
        {
            colorSwitcher?.GoldPowerDiactivated();
        }
    }

}