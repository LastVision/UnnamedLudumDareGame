﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon_Pistol : Weapon_Base
{
    public override void FireAlgoritm()
    {
        RaycastHit hit;
        int layerMaskAll = ~0;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMaskAll))
        {
            var healthComponent = hit.transform.gameObject.GetComponent<Health>();
            if (healthComponent)
            {
                healthComponent.Damage(Damage_internal);
            }
        }
        
        Vector3 dir = (hit.point - Camera.main.transform.position).normalized;
        Vector3 start = Camera.main.transform.position + Camera.main.transform.up * -0.05f + Camera.main.transform.forward * 0.2f;
        UtilityFunctions.DrawLine(start, hit.point, Color.green, 0.5f);


    }

    public override void Reload()
    {
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(reloadSounds[Random.Range(0, reloadSounds.Count - 1)]);
        base.Reload();
    }
}
