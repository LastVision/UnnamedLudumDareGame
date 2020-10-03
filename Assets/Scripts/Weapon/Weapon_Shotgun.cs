using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon_Shotgun : Weapon_Base
{
    public override void FireAlgoritm()
    {
        RaycastHit hit;
        int layerMaskAll = ~0;


        for (int i = 0; i < 20; ++i)
        {
            Vector3 dir = (Camera.main.transform.forward * 20f + Random.insideUnitSphere).normalized;

            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, Mathf.Infinity, layerMaskAll))
            {
                Debug.Log("Did Hit");
            }

            Vector3 start = Camera.main.transform.position + Camera.main.transform.up * -0.05f + dir;
            UtilityFunctions.DrawLine(start, start + dir * 20f, Color.green, 0.2f);
        }
    }
}
