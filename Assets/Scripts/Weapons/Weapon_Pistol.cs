using System.Collections;
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
    }

    public override void Reload()
    {
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(reloadSounds[Random.Range(0, reloadSounds.Count - 1)]);
        base.Reload();
    }
}
