using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Pistol : WeaponBase
{
    public override short MaxAmmo {get; protected set;}
    public override void Fire()
    {
        RaycastHit hit;
        int layerMaskAll = ~0;

        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(fireSounds[Random.Range(0, fireSounds.Count - 1)]);

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMaskAll))
        {
            Vector3 dir = (hit.point - Camera.main.transform.position).normalized;
            Vector3 start = Camera.main.transform.position + Camera.main.transform.up * -0.05f + dir * 0.2f;
            UtilityFunctions.DrawLine(start, hit.point, Color.green, 0.5f);

            Debug.Log("Did Hit");
        }
        else
        {
            Vector3 dir = Camera.main.transform.forward;
            Vector3 start = Camera.main.transform.position + Camera.main.transform.up * -0.05f + dir * 0.2f;
            UtilityFunctions.DrawLine(start, dir * 1000f, Color.red, 0.5f);

            Debug.Log("Did not Hit");
        }
    }
}
