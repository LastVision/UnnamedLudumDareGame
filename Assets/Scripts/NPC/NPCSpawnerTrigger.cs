using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawnerTrigger : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> spawners = new List<GameObject>();

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach(GameObject spawner in spawners)
            {
                NPCSpawner spawnScript = spawner.GetComponent<NPCSpawner>();
                if(spawnScript)
                {
                    if(spawnScript.HasSpawned() == false)
                    {
                        spawnScript.Spawn();
                    }
                }
                else
                {
                    Debug.Log("This object don't have a spawner script. Object: " + spawner.name);
                }
            }
        }
    }
}
