using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class WeaponBase : MonoBehaviour
{
    
    public virtual short MaxAmmo {get; protected set;}
    public Mesh model;
    public List<AudioClip> fireSounds = new List<AudioClip>();
    
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
