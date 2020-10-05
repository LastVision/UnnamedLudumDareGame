using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    [SerializeField]
    public GameObject Type;
    [SerializeField]
    public List<GameObject> PatrolPoints = new List<GameObject>();

    private GameObject spawnedNpc;
    private bool hasSpawnedNpc = false;
    public void Spawn()
    {
        if(hasSpawnedNpc)
        {
            Debug.Log("Npc already is spawned");
            return;
        }
        if(Type.GetComponent<EnemyBehaviour>() == null)
        {
            Debug.Log("This isn't an enemy it does not have the behaviour script!");
            return;
        }

        spawnedNpc = Instantiate(Type, transform);
        var behaviourScript = spawnedNpc.GetComponent<EnemyBehaviour>();
      
        behaviourScript.SetPatrolPoints(PatrolPoints);

        hasSpawnedNpc = true;
    }

    public void Reset()
    {
        if(spawnedNpc)
        {
            Destroy(spawnedNpc);
        }
        hasSpawnedNpc = false;
    }

    public bool HasSpawned()
    {
        return hasSpawnedNpc;
    }
}
