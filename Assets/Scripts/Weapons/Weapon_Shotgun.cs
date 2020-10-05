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


        for (int i = 0; i < 40; ++i)
        {
            Vector3 dir = (Camera.main.transform.forward * 2f + Random.insideUnitSphere).normalized;

            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, Mathf.Infinity, layerMaskAll))
            {
                var healthComponent = hit.transform.gameObject.GetComponent<Health>();
                if (healthComponent)
                {
                    Debug.Log("hit");
                    healthComponent.Damage(Damage_internal);
                }
            }

            Vector3 start = Camera.main.transform.position + Camera.main.transform.up * -0.05f + dir;
            UtilityFunctions.DrawLine(start, start + dir * 20f, Color.green, 0.2f);
        }
    }
}
