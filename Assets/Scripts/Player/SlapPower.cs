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
    public void ChangeSlap(Slap slap) => this.slap = slap;

    private bool isPowerActivated;

    [Header("Wall Power")]
    [SerializeField] private GameObject wallGameobject;
    [SerializeField] private GameObject playerModelHandler;
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

        StartCoroutine(DisactivatePower(timeToDisactivateWallPower, WallPowerDisactivate));
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
        Collider[] coll = Physics.OverlapSphere(player.transform.position, sleeplyPowerSphereRadius, enemyLayer);

        foreach (var item in coll)
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

        StartCoroutine(DisactivatePower(timeToDisactivateLegoPower, LegoPowerDisactivate));
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

        StartCoroutine(DisactivatePower(timeToDisactivateSnowyPower, SnowyPowerDisactivate));
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
    private IEnumerator DisactivatePower(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);

        action?.Invoke();
    }

}