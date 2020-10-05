using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ECondition
{
    Pistol,
    Shotgun,
    RocketLauncher,
    PoI1,
    PoI2,
    Poi3
}

public class BackseatGamerEventTrigger : MonoBehaviour
{
    public bool CostsStrikes = true;
    public GameObject DoorToLock;
    public ECondition Condition;
    public List<AudioClip> StrikeSounds;
    private bool HasMetCondition = false;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Player")
        {
            return;
        }

        if (HasMetCondition)
        {
            Destroy(gameObject, 2);
        }
        else
        {
            if (CostsStrikes)
            {
                collider.gameObject.GetComponent<Strikes>().ReceiveStrike(StrikeSounds[Random.Range(0, StrikeSounds.Count - 1)]);
            }
            if (DoorToLock)
            {
                DoorToLock.GetComponent<Door>().Lock();
            }
        }
    }
    public void ConditionMet(ECondition condition)
    {
        if (Condition == condition)
        {
            HasMetCondition = true;
            if (DoorToLock)
            {
                DoorToLock.GetComponent<Door>().Unlock();
            }
        }
    }
}
