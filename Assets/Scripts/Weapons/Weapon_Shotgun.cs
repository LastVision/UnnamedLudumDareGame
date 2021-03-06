﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon_Shotgun : Weapon_Base
{
    public override void FireAlgoritm()
    {
        RaycastHit hit;
        int layerMaskAll = ~0;


        for (int i = 0; i < 30; ++i)
        {
            Vector3 dir = (Camera.main.transform.forward * 10f + Random.insideUnitSphere).normalized;

            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 20, layerMaskAll))
            {
                var healthComponent = hit.transform.gameObject.GetComponent<Health>();
                if (healthComponent)
                {
                    healthComponent.Damage(Damage_internal);
                }
            }
        }
    }
}
