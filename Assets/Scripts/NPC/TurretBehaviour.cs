using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBehaviour : EnemyBehaviour
{
    [SerializeField]
    public float aggroRadius = 10.0f;
    [SerializeField]
    public float attacksPerSecond = 1.0f;

    [SerializeField]
    public float damage = 1.0f;

    private float timeSinceAttack = 0f; 
    void Update ()
    {
        timeSinceAttack += Time.deltaTime;
    }
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
        LookAtTarget();

        if (timeSinceAttack > (1f / attacksPerSecond))
        {
            Attack();
        }
    }
    protected override void OnDeath()
    {
        var child = transform.GetChild(0);
        if (child)
        {
            Destroy(transform.GetChild(0).gameObject);
        }

        child = transform.GetChild(1);
        if (child)
        {
            Destroy(transform.GetChild(1).gameObject);
        }

        gameObject.GetComponent<CapsuleCollider>().enabled = false; 

        base.OnDeath();
    }
    private void LookAtTarget()
    {
        GameObject child = transform.GetChild(1).gameObject;
        
        child.transform.LookAt(2 * child.transform.position - targetPoint);
    }

    private void Attack()
    {
        GameObject child = transform.GetChild(1).gameObject;
        
        RaycastHit hit;
        int layerMaskAll = ~0;

        if (Physics.Raycast(child.transform.position, -child.transform.forward, out hit, 20, layerMaskAll))
        {
            var healthComponent = hit.transform.gameObject.GetComponent<Health>();
            if (healthComponent)
            {
                healthComponent.Damage(damage);
            }
        }

        timeSinceAttack = 0f;
    }
}
