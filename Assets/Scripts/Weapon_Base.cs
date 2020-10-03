using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Weapon_Base : MonoBehaviour
{
    //public
    public short MaxAmmo;

    //protected
    public GameObject MuzzlePosition;
    public GameObject HandlePosition;
    protected short MaxAmmo_internal = 5; //hax to have inherited variable
    public List<AudioClip> fireSounds = new List<AudioClip>();
    
    //private
    protected short CurrentAmmo = 5;
    

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

    public abstract void Fire();
    public virtual void Reload()
    {
        CurrentAmmo = MaxAmmo_internal;
    }
}
