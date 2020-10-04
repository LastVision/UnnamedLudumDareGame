using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public abstract class Weapon_Base : MonoBehaviour
{
    //public
    public short MaxAmmo;
    public float ReloadCooldown;
    public float WeaponCooldown;
    public GameObject MuzzlePosition;
    public GameObject HandlePosition;
    public List<AudioClip> fireSounds = new List<AudioClip>();
    public List<AudioClip> reloadSounds = new List<AudioClip>();
    
    //protected
    protected short MaxAmmo_internal = 5; //hax to have inherited variable
    protected float ReloadCooldown_internal = 1.5f;
    protected float ReloadTimer = 0.0f;
    protected float WeaponCooldown_internal = 0.25f;
    protected float WeaponTimer = 0.0f;
    
    //private
    public short CurrentAmmo{get; protected set;}
    

    void Start()
    {
        MaxAmmo_internal = MaxAmmo;
        CurrentAmmo = MaxAmmo_internal;
        ReloadCooldown_internal = ReloadCooldown;
        WeaponCooldown_internal = WeaponCooldown;
    }

    void Update()
    {
        if (ReloadTimer > 0.0f)
        {
            ReloadTimer -= Time.deltaTime;
            if (ReloadTimer <= 0.0f)
            {
                DoneReloading();
            }
        }
        if (WeaponTimer > 0.0f)
        {
            WeaponTimer -= Time.deltaTime;
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
        if (ReloadTimer <= 0.0f && WeaponTimer <= 0.0f)
        {
            --CurrentAmmo;
            FireAlgoritm();
            WeaponTimer = WeaponCooldown_internal;
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
        gameObject.transform.Rotate(new Vector3(90, 0, 0));
        ReloadTimer = ReloadCooldown_internal;
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(reloadSounds[Random.Range(0, reloadSounds.Count - 1)]);
    }

    void DoneReloading()
    {
        CurrentAmmo = MaxAmmo_internal;
        gameObject.transform.Rotate(new Vector3(-90, 0, 0));
    }

    public void InterruptReload()
    {
        if (ReloadTimer > 0.0f)
        {
            ReloadTimer = 0.0f;
            gameObject.transform.Rotate(new Vector3(-90, 0, 0));
            GameObject.FindWithTag("Player").GetComponent<AudioSource>().Stop();
        }
    }
}
