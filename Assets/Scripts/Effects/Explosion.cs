using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Explosion : MonoBehaviour
{
    public float ExplosionRadius = 1f;
    public float ExplosionDamage = 1f;
    public float LifeTime = 1f;
    void Start()
    {
        GetComponent<SphereCollider>().radius = ExplosionRadius;
        Destroy(this, LifeTime);
    }

    void OnTriggerEnter(Collider collider)
    {
        var healthComponent = collider.GetComponent<Health>();
        if (healthComponent)
        {
            healthComponent.Damage(ExplosionDamage);
        }
    }
}
