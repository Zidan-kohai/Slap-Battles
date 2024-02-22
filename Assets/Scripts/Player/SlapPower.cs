using CMF;
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
    public void ChangeSlap(Slap slap) => this.slap = slap;

    private bool isPowerActivated;

    [Header("Wall Power")]
    [SerializeField] private GameObject wallGameobject;
    [SerializeField] private GameObject playerModelHandler;
    [SerializeField] private List<Collider> playerCollider;
    [SerializeField] private float timeToDisactivateWallPower;

    [Header("Sleeply Power")]
    [SerializeField] private float timeToEnemySleep;
    [SerializeField] private float sphereRadius;
    [SerializeField] private LayerMask enemyLayer;


    [Header("Lego")]
    [SerializeField] private float timeToDisactivateLegoPower;
    [SerializeField] private GameObject legoSphere;
    [SerializeField] private Transform legoSpherePosition;



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
                    SleepLyPowerActivated();
                        break;
                case SlapPowerType.Lego:
                    LegoPowerActivated();
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

    private void SleepLyPowerActivated()
    {
        Collider[] coll = Physics.OverlapSphere(player.transform.position, sphereRadius, enemyLayer);

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

    private void LegoPowerActivated()
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
    private IEnumerator DisactivatePower(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);

        action?.Invoke();
    }

}
