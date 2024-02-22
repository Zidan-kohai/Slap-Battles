using CMF;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private IEnumerator DisactivatePower(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);

        action?.Invoke();
    }

}
