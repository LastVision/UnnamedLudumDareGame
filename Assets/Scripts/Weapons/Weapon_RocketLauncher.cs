using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Weapon_RocketLauncher : Weapon_Base
{
    public GameObject Ammo;
    public override void FireAlgoritm()
    {
        if (Ammo)
        {
            Instantiate(Ammo, Camera.main.transform.position + Camera.main.transform.forward * 1.5f, Camera.main.transform.rotation);
        }
    }
}
