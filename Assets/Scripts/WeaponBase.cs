using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class WeaponBase : MonoBehaviour
{
    
    public short MaxAmmo = 5;
    public Mesh model;

    private short CurrentAmmo = 5;
    // Update is called once per frame
    void Start()
    {
        CurrentAmmo = MaxAmmo;
    }
    public abstract void Fire();
    public virtual void Reload()
    {
        CurrentAmmo = MaxAmmo;
    }
}
