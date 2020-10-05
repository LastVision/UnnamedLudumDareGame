using System.Collections;
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
    public GameObject explosionObject;
    public List<AudioClip> explosionSounds = new List<AudioClip>();
    private Vector3 velocity = Vector3.zero;
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
            Kill();
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
        if (explosionSounds.Count > 0)
        {
            AudioSource.PlayClipAtPoint(explosionSounds[Random.Range(0, explosionSounds.Count - 1)], this.gameObject.transform.position);
        }

        if (explosionObject)
        {
            var go = Instantiate (explosionObject, transform.GetChild(0).position, Quaternion.identity) as GameObject;
            var explosion = go.GetComponent<Explosion>();
            explosion.LifeTime = 0.5f;
            explosion.ExplosionDamage = explosionDamage;
            explosion.ExplosionRadius = explosionRadius;
        }

        if (explosionParticleGameObject)
        {
            var explosionParticle = Instantiate (explosionParticleGameObject, transform.GetChild(0).position, Quaternion.identity) as GameObject;
            explosionParticle.GetComponent<ParticleSystem>().Play();
            Destroy (explosionParticle , 3);
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

    public override void Kill()
    {
        Explode();
        base.Kill();
    }
    protected override void OnDeath()
    {
        var child = transform.GetChild(0);
        if (child)
        {
            Destroy(transform.GetChild(0).gameObject);
        }
        gameObject.GetComponent<CapsuleCollider>().enabled = false; 
        gameObject.GetComponent<AudioSource>().enabled = false; 

        base.OnDeath();
    }

}
