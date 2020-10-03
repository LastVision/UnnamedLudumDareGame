﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeBehaviour : EnemyBehaviour
{
    [SerializeField]
    public float aggroRadius = 10.0f;
    [SerializeField]
    public float attackRadius = 1.0f;
    [SerializeField]
    public float explosionRadius = 2.0f;
    [SerializeField]
    public float explosionDamage = 1.0f;
    [SerializeField]
    public float movementSpeed = 3.0f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPoint = Vector3.zero;
    private float idlePointDistance = 5.0f;
    protected override void OnInit()
    {
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
        float dist = Vector3.Distance(targetPoint, transform.position);

        if(dist < attackRadius)
        {
            targetPoint = GetRandomPointInRadius(idlePointDistance);
        }
        MoveToTargetPoint();
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
        if(playerDist < attackRadius && currentTime > 0.5f)
        {
            Explode();
            return;
        }
        
        if(playerDist > attackRadius - 0.4f)
        {
            targetPoint = GetPlayerCoordinate();
            MoveToTargetPoint();
        }
    }

    private void MoveToTargetPoint()
    {
        velocity = ((targetPoint - transform.position).normalized) * movementSpeed;

        transform.LookAt(targetPoint);
        transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
    }
    private void Explode()
    {
        print("Boom");
    }

    private Vector3 GetRandomPointInRadius(float radius)
    {
        Vector3 point = Random.insideUnitSphere * radius;
        point.y = 0.0f;
        point += transform.position;
        //TODO: check if valid point(aka can you reach it without passing through walls)

        return point;
    }
}
