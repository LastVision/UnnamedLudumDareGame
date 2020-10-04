using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo_Rocket : MonoBehaviour
{
    public float speed = 1f;
    public float exposionRadius = 1f;
    public GameObject explosionParticleGameObject;
    
    void Start()
    {
        var rb = transform.gameObject.GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
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
        
        var allEnemies = FindObjectsOfType<EnemyBehaviour>();
        foreach (EnemyBehaviour enemy in allEnemies)
        {
            if ((enemy.transform.position - transform.position).sqrMagnitude <= exposionRadius * exposionRadius) 
            {
                enemy.Kill();
            }
        }

        GameObject explosionParticle = Instantiate (explosionParticleGameObject, transform.position, Quaternion.identity) as GameObject;
        explosionParticle.GetComponent<ParticleSystem>().Play();
        Destroy (explosionParticle , 3);

        Destroy(gameObject);
    }
}
