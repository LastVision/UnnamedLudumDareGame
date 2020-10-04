using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ECondition
{
    Pistol,
    Shotgun,
    RocketLauncher
}

public enum EBackseaterState
{
    HasFailed,
    HasSucceeded,
    Neither
}

public class BackseatGamerEventTrigger : MonoBehaviour
{
    public bool CostsStrikes = true;
    public GameObject DoorToLock;
    public ECondition Condition;
    private bool HasMetCondition = false;
    private EBackseaterState State = EBackseaterState.Neither;
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
            DoorToLock.GetComponent<Door>().Unlock();
        }
    }
}
