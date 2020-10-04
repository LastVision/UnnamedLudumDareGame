using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Rocket : MonoBehaviour
{
    public float Speed = 1f;
    public float ExplosionRadius = 1f;
    public float ExplosionDamage = 1f;
    public GameObject ExplosionObject;
    public GameObject ExplosionParticleObject;
    
    void Start()
    {
        var rb = transform.gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Speed;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!collider.gameObject.GetComponent<Weapon_Base>())
        {
            Explode();
        }
    }

    private void Explode()
    {
        var go = Instantiate (ExplosionObject, transform.position, Quaternion.identity) as GameObject;
        var explosion = go.GetComponent<Explosion>();
        explosion.LifeTime = 0.5f;
        explosion.ExplosionDamage = ExplosionDamage;
        explosion.ExplosionRadius = ExplosionRadius;

        var explosionParticle = Instantiate (ExplosionParticleObject, transform.position, Quaternion.identity) as GameObject;
        explosionParticle.GetComponent<ParticleSystem>().Play();
        Destroy (explosionParticle , 3);

        Destroy(gameObject);
    }
}
