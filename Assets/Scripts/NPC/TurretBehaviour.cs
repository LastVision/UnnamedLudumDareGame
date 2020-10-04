using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : EnemyBehaviour
{
    [SerializeField]
    public float aggroRadius = 10.0f;
    [SerializeField]
    public float fireRate = 1.0f;
    [SerializeField]
    public float damage = 1.0f;
    protected override void OnInit()
    {
        canPatrol = false;
    }
    protected override void OnUpdateIdleState()
    {
        float currentTime = (Time.fixedTime - stateTime);
        float playerDist = GetPlayerDist();

        if(playerDist < aggroRadius && currentTime > 1.0f)
        {
            ChangeState(STATE.AGGRO, Time.fixedTime);
            return;
        }
        //TODO: add scanning room mode
        LookAtTarget();
    }

    protected override void OnUpdateAggroState()
    {
        float currentTime = (Time.fixedTime - stateTime);
        float playerDist = GetPlayerDist();

        if(playerDist > aggroRadius && currentTime > 1.0f)
        {
            ChangeState(STATE.IDLE, Time.fixedTime);
            return;
        }

        targetPoint = GetPlayerCoordinate();
        //TODO: fire at player
        LookAtTarget();
    }

    protected override void OnDeath()
    {
        Debug.Log("I AM A DEAD TURRET!");
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    private void LookAtTarget()
    {
        GameObject child = transform.GetChild(1).gameObject;
        
        child.transform.LookAt(2 * child.transform.position - targetPoint);
    }
}
