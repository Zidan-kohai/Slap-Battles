using CMF;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlapPower : MonoBehaviour
{
    [SerializeField] private Slap slap;
    [SerializeField] private AdvancedWalkerController playerWalkController;
    [SerializeField] private Player player;
    [SerializeField] private SmoothPosition cameraSmoothPosition;
    public void ChangeSlap(Slap slap) => this.slap = slap;


    [Header("Wall Power")]
    [SerializeField] private GameObject wallGameobject;
    [SerializeField] private GameObject playerModelHandler;
    [SerializeField] private List<Collider> playerCollider;
    [SerializeField] private float timeToDisactivateWallPower;

    private bool isPowerActivated;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && !isPowerActivated)
        {
            switch(slap.GetSlapPowerType())
            {
                case SlapPowerType.wall:
                    WallPowerActivate();
                        break;
            }
        }
    }

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


    private IEnumerator DisactivatePower(float waitTime, Action action)
    {
        yield return new WaitForSeconds(waitTime);

        action?.Invoke();
    }

}
