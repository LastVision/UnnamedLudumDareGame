using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class WeildingGun : MonoBehaviour
{

    private GameObject currentWeapon;
    private int currentWeaponIndex;
    public List<GameObject> WeaponPrefabs;
    protected List<GameObject> Weapons = new List<GameObject>();
    public AudioClip ChangeWeaponSound;

    public Text UI_Ammo;

    void Start()
    {
        if (WeaponPrefabs.Count > 0)
        {
            foreach(GameObject prefab in WeaponPrefabs)
            {
                EquipWeapon(prefab);
            }
            SwitchToWeapon(0);
        }
    }

    void SwitchToWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < Weapons.Count)
        {
            if (currentWeapon)
            {
                currentWeapon.GetComponent<Weapon_Base>().InterruptReload();
                currentWeapon.SetActive(false);
            }
            currentWeapon = Weapons[weaponIndex];
            currentWeapon.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<AudioSource>().PlayOneShot(ChangeWeaponSound);

            currentWeaponIndex = weaponIndex;
        }
    }

    void SwitchToNextWeapon()
    {
        int weaponIndex = currentWeaponIndex + 1;
        if (weaponIndex < Weapons.Count)
        {
            SwitchToWeapon(weaponIndex);
        }
        else
        {
            SwitchToWeapon(0);
        }
    }

    void SwitchToPreviousWeapon()
    {
        int weaponIndex = currentWeaponIndex - 1;
        if (weaponIndex >= 0)
        {
            SwitchToWeapon(weaponIndex);
        }
        else
        {
            SwitchToWeapon(Weapons.Count - 1);
        }
    }

    public void EquipWeapon(GameObject WeaponPrefab)
    {
        GameObject newWeapon = Instantiate(WeaponPrefab, new Vector3(0, 0, 0), Quaternion.identity);

        Transform handleTransform = newWeapon.GetComponent<Weapon_Base>().GetHandleTransform();
        var handPosition = Camera.main.transform.Find("HandPosition");
        newWeapon.transform.SetParent(handPosition);
        newWeapon.transform.localPosition = -handleTransform.localPosition;
        newWeapon.transform.localRotation = Quaternion.Inverse(handleTransform.localRotation);
        newWeapon.SetActive(false);
        Weapons.Add(newWeapon);

        SwitchToWeapon(Weapons.Count - 1);
    }

    void Update()
    {
        if (!currentWeapon)
        {
            return;
        } 
        Weapon_Base Weapon = currentWeapon.GetComponent<Weapon_Base>();

        if (Input.GetButtonDown("Reload"))
        {
            Weapon.TryToReload();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (Weapon.CurrentAmmo > 0)
            {
                Weapon.Fire();
            }
            else
            {
                NoAmmo();
            }
        }

        float scrollInput = Input.GetAxis("ChangeWeaponWithScroll");
        if (Weapons.Count > 1)
        {
            if (scrollInput > 0f)
            {
                SwitchToNextWeapon();
            }
            if (scrollInput < 0f)
            {
                SwitchToPreviousWeapon();
            }
        }

        if (UI_Ammo)
        {
            if (!UI_Ammo.enabled)
            {
                UI_Ammo.enabled = true;
            }

            UI_Ammo.text = string.Format("Ammo: {0}/{1}", Weapon.CurrentAmmo, Weapon.MaxAmmo);     
        }
    }

    void NoAmmo()
    {
    }
}
