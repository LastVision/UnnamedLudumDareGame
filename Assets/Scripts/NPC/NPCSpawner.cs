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
    void Spawn()
    {
        if(hasSpawnedNpc)
        {
            Debug.Log("Npc already is spawned");
            return;
        }

        spawnedNpc = Instantiate(Type, transform);
        //TODO: Set PatrolPoints to the NPC
        hasSpawnedNpc = true;
    }

    void Reset()
    {
        if(spawnedNpc)
        {
            Destroy(spawnedNpc);
        }
        hasSpawnedNpc = false;
    }

    void Start()
    {
        Spawn();
    }
}
