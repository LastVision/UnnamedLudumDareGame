using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeildingGun : MonoBehaviour
{

    private GameObject currentWeapon;
    private int currentWeaponIndex;
    public List<GameObject> WeaponPrefabs;
    protected List<GameObject> Weapons = new List<GameObject>();

    void Start()
    {
        if (WeaponPrefabs.Count > 0)
        {
            foreach(GameObject prefab in WeaponPrefabs)
            {
                GameObject newWeapon = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);

                Transform handleTransform = newWeapon.GetComponent<Weapon_Base>().GetHandleTransform();
                var handPosition = Camera.main.transform.Find("HandPosition");
                newWeapon.transform.SetParent(handPosition);
                newWeapon.transform.localPosition = -handleTransform.localPosition;
                newWeapon.transform.localRotation = Quaternion.Inverse(handleTransform.localRotation);
                newWeapon.SetActive(false);
                Weapons.Add(newWeapon);
            }
            EquipWeapon(0);
        }
    }

    void EquipWeapon(int weaponIndex)
    {
        if (weaponIndex >= 0 && weaponIndex < Weapons.Count)
        {
            if (currentWeapon)
            {
                currentWeapon.SetActive(false);
            }
            currentWeapon = Weapons[weaponIndex];
            currentWeapon.SetActive(true);

            currentWeaponIndex = weaponIndex;
        }
    }

    void EquipNextWeapon()
    {
        int weaponIndex = currentWeaponIndex + 1;
        if (weaponIndex < Weapons.Count)
        {
            EquipWeapon(weaponIndex);
        }
        else
        {
            EquipWeapon(0);
        }
    }

    void EquipPreviousWeapon()
    {
        int weaponIndex = currentWeaponIndex - 1;
        if (weaponIndex >= 0)
        {
            EquipWeapon(weaponIndex);
        }
        else
        {
            EquipWeapon(Weapons.Count - 1);
        }
    }

    void Update()
    {
        if (currentWeapon is null)
        {
            return;
        }
        Weapon_Base Weapon = currentWeapon.GetComponent<Weapon_Base>();

        if (Input.GetButtonDown("Reload"))
        {
            Weapon.Reload();
        }
        if (Input.GetButtonDown("Fire1"))
        {
            if (Weapon.CurrentAmmo > 0)
            {
                Weapon.Fire();
                Debug.Log("Ammo left: " + Weapon.CurrentAmmo);
            }
        }

        float scrollInput = Input.GetAxis("ChangeWeaponWithScroll");
        if (scrollInput > 0f)
        {
            EquipNextWeapon();
        }
        if (scrollInput < 0f)
        {
            EquipPreviousWeapon();
        }
    }

    void NoAmmo()
    {
        Debug.Log("No Ammo");
    }
}
