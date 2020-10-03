using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeildingGun : MonoBehaviour
{

    public WeaponBase currentWeapon;

    void Update()
    {
        if (currentWeapon is null)
        {
            return;
        }
        
        if (Input.GetButtonDown("Reload"))
        {
            currentWeapon.Reload();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            currentWeapon.Fire();
        }
    }
}
