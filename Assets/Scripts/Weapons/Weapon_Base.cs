using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Weapon_Base : MonoBehaviour
{
    //public
    public short MaxAmmo;
    public GameObject MuzzlePosition;
    public GameObject HandlePosition;
    public List<AudioClip> fireSounds = new List<AudioClip>();
    
    //protected
    protected short MaxAmmo_internal = 5; //hax to have inherited variable
    
    //private
    public short CurrentAmmo{get; protected set;}
    

    void Start()
    {
        MaxAmmo_internal = MaxAmmo;
        CurrentAmmo = MaxAmmo_internal;
    }

    void Reset()
    {
        DestroyImmediate(MuzzlePosition);
        MuzzlePosition = new GameObject();
        MuzzlePosition.name = "MuzzlePosition";
        MuzzlePosition.transform.SetParent(gameObject.transform);
        MuzzlePosition.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);

        DestroyImmediate(HandlePosition);
        HandlePosition = new GameObject();
        HandlePosition.name = "HandlePosition";
        HandlePosition.transform.SetParent(gameObject.transform);
        HandlePosition.transform.SetPositionAndRotation(gameObject.transform.position, gameObject.transform.rotation);
    }

    public Transform GetHandleTransform()
    {
        return HandlePosition.transform;
    }

    public virtual void Fire()
    {
        --CurrentAmmo;
        FireAlgoritm();
    }

    public abstract void FireAlgoritm();

    public virtual void Reload()
    {
        CurrentAmmo = MaxAmmo_internal;
    }

}
