using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Weapon_Base : MonoBehaviour
{
    //public
    public short MaxAmmo;
    public float ReloadCooldown;
    public GameObject MuzzlePosition;
    public GameObject HandlePosition;
    public List<AudioClip> fireSounds = new List<AudioClip>();
    public List<AudioClip> reloadSounds = new List<AudioClip>();
    
    //protected
    protected short MaxAmmo_internal = 5; //hax to have inherited variable
    protected float ReloadCooldown_internal = 1.5f;
    protected float ReloadTimer = 0.0f;
    
    //private
    public short CurrentAmmo{get; protected set;}
    

    void Start()
    {
        MaxAmmo_internal = MaxAmmo;
        CurrentAmmo = MaxAmmo_internal;
        ReloadCooldown_internal = ReloadCooldown;
    }

    void Update()
    {
        if (ReloadTimer > 0.0f)
        {
            ReloadTimer -= Time.deltaTime;
        }
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
        if (ReloadTimer <= 0.0f)
        {
            --CurrentAmmo;
            FireAlgoritm();
            GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(fireSounds[Random.Range(0, fireSounds.Count - 1)]);
        }
    }

    public abstract void FireAlgoritm();

    public void TryToReload()
    {
        if (ReloadTimer <= 0.0f)
        {
            Reload();
        }
    }

    public virtual void Reload()
    {
        CurrentAmmo = MaxAmmo_internal;
        ReloadTimer = ReloadCooldown_internal;
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(reloadSounds[Random.Range(0, reloadSounds.Count - 1)]);
    }

}
