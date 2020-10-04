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
    public List<AudioClip> explosionSounds = new List<AudioClip>();
    
    void Start()
    {
        var rb = transform.gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * Speed;
    }

    void OnTriggerEnter(Collider collider)
    {
        var tag = collider.gameObject.tag;
        switch (tag)
        {
            case "SolidEnvironment":
            case "Enemy":
            case "Player":
            Explode();
            return;
        }

        var parent = collider.gameObject.transform.parent;
        if (parent)
        {
            switch (parent.tag)
            {
                case "SolidEnvironment":
                case "Enemy":
                case "Player":;
                Explode();
                break;
            }
        }
    }

    private void Explode()
    {
        gameObject.GetComponent<AudioSource>().PlayOneShot(explosionSounds[Random.Range(0, explosionSounds.Count - 1)]);

        var go = Instantiate (ExplosionObject, transform.position, Quaternion.identity) as GameObject;
        var explosion = go.GetComponent<Explosion>();
        explosion.LifeTime = 0.5f;
        explosion.ExplosionDamage = ExplosionDamage;
        explosion.ExplosionRadius = ExplosionRadius;

        var explosionParticle = Instantiate (ExplosionParticleObject, transform.position, Quaternion.identity) as GameObject;
        explosionParticle.GetComponent<ParticleSystem>().Play();
        Destroy (explosionParticle , 3);
        
        Destroy(gameObject, 2);
    }

}
