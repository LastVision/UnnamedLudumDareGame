using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appreciateable : MonoBehaviour
{

    public ECondition ConditionMet;
    public void Appreciate()
    {
        var triggers = FindObjectsOfType<BackseatGamerEventTrigger>();
        foreach (var t in triggers)
        {
            t.ConditionMet(ConditionMet);
        }
    }
}
