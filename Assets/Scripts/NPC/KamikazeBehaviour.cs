﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeBehaviour : EnemyBehaviour
{
    [SerializeField]
    public float aggroRadius = 10.0f;
    [SerializeField]
    public float attackRadius = 2.0f;
    [SerializeField]
    public float explosionRadius = 2.0f;
    [SerializeField]
    public float explosionDamage = 1.0f;
    [SerializeField]
    public float movementSpeed = 3.0f;
    [SerializeField]
    public GameObject explosionParticleGameObject;

    private Vector3 velocity = Vector3.zero;
    private float idlePointDistance = 5.0f;
    private ParticleSystem explosionParticle;
    protected override void OnInit()
    {
        explosionParticle = explosionParticleGameObject.GetComponent<ParticleSystem>();
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

        if(ShouldGetNewPatrolTargetPoint())
        {
            targetPoint = GetNextPatrolTargetPoint();
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

    protected override void OnDeath()
    {
        Debug.Log("I AM A DEAD DRONE!");
        GetComponentInChildren<MeshRenderer>().enabled = false;
    }

    private void MoveToTargetPoint()
    {
        velocity = ((targetPoint - transform.position).normalized) * movementSpeed;

        transform.LookAt(targetPoint);
        transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
    }
    private void Explode()
    {
        if(explosionParticle)
        {
            explosionParticle.Play();
            Kill();
        }
        else
        {
            Debug.Log("Particles didn't work");
        }
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
