using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionMetTrigger : MonoBehaviour
{
    public ECondition TriggerConditionMet;
    void OnTriggerEnter(Collider collider)
    {
        if (collider.tag != "Player")
        {
            return;
        }

        var triggers = FindObjectsOfType<BackseatGamerEventTrigger>();
        foreach (var t in triggers)
        {
            t.ConditionMet(TriggerConditionMet);
        }

    }
}
