using CMF;  
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SlapPower : MonoBehaviour
{
    [SerializeField] private AdvancedWalkerController playerWalkController;
    [SerializeField] private SmoothPosition cameraSmoothPosition;
    [SerializeField] private EventManager eventManager;
    [SerializeField] private Player player;
    [SerializeField] private Slap slap;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameObject playerModelHandler;
    private float startSpeed;
    private float startPower;
    public void ChangeSlap(Slap slap) => this.slap = slap;

    private bool isPowerActivated;

    [Header("Wall Power")]
    [SerializeField] private GameObject wallGameobject;
    [SerializeField] private List<Collider> playerCollider;
    [SerializeField] private float timeToDisactivateWallPower;

    [Header("Sleeply Power")]
    [SerializeField] private float timeToEnemySleep;
    [SerializeField] private float sleeplyPowerSphereRadius;


    [Header("Lego")]
    [SerializeField] private float timeToDisactivateLegoPower;
    [SerializeField] private GameObject legoSphere;
    [SerializeField] private Transform legoSpherePosition;

    [Header("Snowy")]
    [SerializeField] private float timeToDisactivateSnowyPower;
    [SerializeField] private float snowyPowerSphereRadius;
    [SerializeField] private float freezingFactor;
    private Collider[] freezedEnemies;

    [Header("Time")]
    [SerializeField] private Vector3 activatePosition;
    [SerializeField] private float timeToDiactivateTimePower;

    [Header("Shooker")]
    [SerializeField] private float radiusSphereOfShookerPower;
    [SerializeField] private float timeToDiactivateShookerPower;

    [Header("Pusher")]
    [SerializeField] private Transform wallPusherStartPosition;
    [SerializeField] private GameObject wallPusher;
    [SerializeField] private float timeToDiactivatePusherPower;

    [Header("Magnet")]
    [SerializeField] private float timeToDiactivateMagnetPower;


    [Header("Accelerator")]
    [SerializeField] private float timeToDiactivateAcceleratorPower;

    [Header("Gold")]
    [SerializeField] private float timeToDiactivateGoldPower;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !isPowerActivated)
        {
            switch(slap.GetSlapPowerType())
            {
                case SlapPowerType.Wall:
                    WallPowerActivate();
                     break;
                case SlapPowerType.Sleepy:
                    SleepLyPowerActivate();
                     break;
                case SlapPowerType.Lego:
                    LegoPowerActivate();
                     break;
                case SlapPowerType.Snowy:
                    SnowyPowerActivate();
                     break;
                case SlapPowerType.Teleport:
                    TeleportPowerActivate();    
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

        wallGameobject.transform.position = player.transform.position;

        StartCoroutine(DiactivatePower(timeToDisactivateWallPower, WallPowerDisactivate));
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

    private void SleepLyPowerActivate()
    {
        Collider[] colls = Physics.OverlapSphere(player.transform.position, sleeplyPowerSphereRadius, enemyLayer);

        foreach (var item in colls)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            enemy.GetDamage(slap.AttackPower, (item.transform.position - player.transform.position).normalized, out bool isDeath, out int gettedSlap);
            player.SetStolenSlaps(gettedSlap);
            eventManager.InvokeChangeMoneyEvents(gettedSlap);

            enemy.Sleep(timeToEnemySleep);
        }
    }

    #endregion

    #region Lego

    private void LegoPowerActivate()
    {
        isPowerActivated = true;

        legoSphere.SetActive(true);

        legoSphere.transform.position = legoSpherePosition.transform.position;
        legoSphere.transform.parent = null;

        StartCoroutine(DiactivatePower(timeToDisactivateLegoPower, LegoPowerDisactivate));
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

            enemy.Sleep(timeToEnemySleep);
        }

        StartCoroutine(DiactivatePower(timeToDisactivateSnowyPower, SnowyPowerDisactivate));
    }

    private void SnowyPowerDisactivate()
    {
        foreach (var item in freezedEnemies)
        {
            Enemy enemy = item.GetComponent<Enemy>();
            enemy.GetNavMeshAgent.speed *= freezingFactor;

            enemy.Sleep(timeToEnemySleep);
        }
    }

    #endregion

    #region Teleport 
    private void TeleportPowerActivate()
    {
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        if (navMeshData.vertices.Length == 0)
        {
            Debug.LogError("NavMesh not baked");
            return;
        }

        SpawnRandomPoint(navMeshData);
    }
    private void SpawnRandomPoint(NavMeshTriangulation navMeshData)
    {
        // Передаем случайные индексы из массива треугольников навмеша
        int randomTriangleIndex = UnityEngine.Random.Range(0, navMeshData.indices.Length / 3);
        Vector3 randomPoint = GetRandomPointInTriangle(randomTriangleIndex, navMeshData);
        player.transform.position = randomPoint;
    }
    private Vector3 GetRandomPointInTriangle(int triangleIndex, NavMeshTriangulation navMeshData)
    {
        // Выбираем три вершины для заданного треугольника
        Vector3 v1 = navMeshData.vertices[navMeshData.indices[triangleIndex * 3 + 0]];
        Vector3 v2 = navMeshData.vertices[navMeshData.indices[triangleIndex * 3 + 1]];
        Vector3 v3 = navMeshData.vertices[navMeshData.indices[triangleIndex * 3 + 2]];

        // Генерируем случайные веса для нахождения случайной точки внутри треугольника
        float r1 = UnityEngine.Random.Range(0f, 1f);
        float r2 = UnityEngine.Random.Range(0f, 1f);

        // Учитываем, что сумма весов не должна превышать 1
        if (r1 + r2 > 1)
        {
            r1 = 1 - r1;
            r2 = 1 - r2;
        }

        // Рассчитываем случайную точку внутри треугольника
        return v1 + r1 * (v2 - v1) + r2 * (v3 - v1);
    }

    #endregion

    #region Time
    private void ActivateTimePower()
    {
        activatePosition = player.transform.position;
        isPowerActivated = true;
        StartCoroutine(DiactivatePower(timeToDiactivateTimePower, DiactivateTimePower));
        Debug.Log($"time power {activatePosition}");
    }

    private void DiactivateTimePower()
    {
        isPowerActivated = false;
        player.transform.position = activatePosition;
        Debug.Log($"time power {activatePosition}");
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

            enemy.Sleep(timeToEnemySleep);
        }
        Debug.Log(colls.Length);
        StartCoroutine(DiactivatePower(timeToDiactivateShookerPower, DiactivateShookerPower));
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
        StartCoroutine(DiactivatePower(timeToDiactivatePusherPower, PusherPowerDiactivate));
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

        StartCoroutine(DiactivatePower(timeToDiactivateMagnetPower, DiactivateMagnetPower));
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

        StartCoroutine(DiactivatePower(timeToDiactivateAcceleratorPower, DiactivaetAcceleratorPower));
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

        slap.AttackPower *= 1.5f;
        playerWalkController.movementSpeed = startSpeed * 2;

        StartCoroutine(DiactivatePower(timeToDiactivateGoldPower, DiactivateGoldPower));
    }

    private void DiactivateGoldPower()
    {
        isPowerActivated = false;
        playerWalkController.movementSpeed = startSpeed;
        slap.AttackPower = startPower;
    }

    #endregion
    private IEnumerator DiactivatePower(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);

        action?.Invoke();
    }

}