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
    public Quaternion TargetReloadRotation;
    public float Damage;
    public List<AudioClip> fireSounds = new List<AudioClip>();
    public List<AudioClip> reloadSounds = new List<AudioClip>();
    
    //protected

    protected Quaternion InitialRotation = new Quaternion(0, 0, 0, 0);
    protected Quaternion TargetReloadRotation_Internal = new Quaternion(0, 0, 0, 0);
    protected float Damage_internal = 50f;
    protected short MaxAmmo_internal = 5; //hax to have inherited variable
    protected float ReloadCooldown_internal = 1.5f;
    protected float WeaponCooldown_internal = 0.25f;
    protected float ReloadTimer = 0.0f;
    protected float WeaponTimer = 0.0f;
    
    //private
    public short CurrentAmmo{get; protected set;}
    

    void Start()
    {
        Damage_internal = Damage;
        MaxAmmo_internal = MaxAmmo;
        CurrentAmmo = MaxAmmo_internal;
        ReloadCooldown_internal = ReloadCooldown;
        WeaponCooldown_internal = WeaponCooldown;
        InitialRotation = gameObject.transform.localRotation;
        TargetReloadRotation_Internal = TargetReloadRotation;
    }

    void FixedUpdate()
    {
        if (ReloadTimer > 0.0f) // Reload
        {
            if (ReloadTimer >= ReloadCooldown / 4.0f)
            {
                gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.localRotation, TargetReloadRotation, 0.025f);
            }
            else
            {
                gameObject.transform.localRotation = Quaternion.Lerp(gameObject.transform.localRotation, InitialRotation, 0.1f);
            }
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
        ReloadTimer = ReloadCooldown_internal;
        GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(reloadSounds[Random.Range(0, reloadSounds.Count - 1)]);
    }

    void DoneReloading()
    {
        CurrentAmmo = MaxAmmo_internal;
    }

    public void InterruptReload()
    {
        if (ReloadTimer > 0.0f)
        {
            ReloadTimer = 0.0f;
            GameObject.FindWithTag("Player").GetComponent<AudioSource>().Stop();
        }
    }
}
