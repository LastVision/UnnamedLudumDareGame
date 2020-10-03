using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeBehaviour : EnemyBehaviour
{
    [SerializeField]
    public float aggroRadius = 3.0f;
    [SerializeField]
    public float attackRadius = 1.0f;
    [SerializeField]
    public float explosionRadius = 2.0f;
    [SerializeField]
    public float explosionDamage = 1.0f;
    [SerializeField]
    public float movementSpeed = 10.0f;

    private Vector2 velocity = Vector2.zero;
    private Vector2 targetPoint = Vector2.zero;

    protected override void OnInit()
    {

    }
    protected override void OnUpdateIdleState()
    {
        float playerDist = GetDistanceBetweenMeAndPlayer();
        if(playerDist < aggroRadius && stateTime - Time.fixedTime > 1.0f)
        {
            ChangeState(STATE.AGGRO, Time.fixedTime);
            return;
        }
    }

    protected override void OnUpdateAggroState()
    {
        float playerDist = GetDistanceBetweenMeAndPlayer();
        if(playerDist > aggroRadius && stateTime - Time.fixedTime > 1.0f)
        {
            ChangeState(STATE.IDLE, Time.fixedTime);
            return;
        }
        if(playerDist < attackRadius && stateTime - Time.fixedTime > 0.5f)
        {
            Explode();
            return;
        }
    }

    private void Explode()
    {
        print("Boom");
    }
}
