using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeBehaviour : EnemyBehaviour
{
    [SerializeField]
    public float aggroRadius = 30.0f;
    [SerializeField]
    public float attackRadius = 1.0f;
    [SerializeField]
    public float explosionRadius = 2.0f;
    [SerializeField]
    public float explosionDamage = 1.0f;
    [SerializeField]
    public float movementSpeed = 1.0f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 targetPoint = Vector3.zero;
    protected override void OnInit()
    {
    }
    protected override void OnUpdateIdleState()
    {
        float currentTime = (Time.fixedTime - stateTime);
        float playerDist = GetPlayerDist();
        //print("Idle: " + (currentTime) + " player distance " + playerDist);
        if(playerDist < aggroRadius && currentTime > 1.0f)
        {
            ChangeState(STATE.AGGRO, Time.fixedTime);
            return;
        }
        //TODO: add random wandering behaviour
    }

    protected override void OnUpdateAggroState()
    {
        float currentTime = (Time.fixedTime - stateTime);
        float playerDist = GetPlayerDist();
        //print("Aggro: " + (currentTime) + " player distance " + playerDist);
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
            velocity = ((targetPoint - transform.position).normalized) * movementSpeed;

            transform.LookAt(targetPoint);
            transform.Translate(velocity * Time.fixedDeltaTime, Space.World);
        }
    }

    private void Explode()
    {
        print("Boom");
    }
}
