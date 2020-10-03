using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : WeaponBase
{
    public override short MaxAmmo {get; protected set;}
    public override void Fire()
    {
        RaycastHit hit;
        int layerMaskAll = ~0;

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMaskAll))
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 1000, Color.red);
            Debug.Log("Did not Hit");
        }
    }
}
