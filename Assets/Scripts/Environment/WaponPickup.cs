using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaponPickup : MonoBehaviour
{

    public GameObject PickupPrefab;
    public float RotationsPerSecond = 1f;
    private GameObject SpinningGunMesh;
    private GameObject Light;
    private bool IsPickedUp = false;

    void Start()
    {
        GameObject mesh = PickupPrefab.transform.Find("Mesh").gameObject;
        SpinningGunMesh = Instantiate(mesh, transform.position + transform.up * 1.5f, Quaternion.identity);
        Light = transform.gameObject.transform.Find("Light").gameObject;
    }
    void Update()
    {
        if (!IsPickedUp)
        {
            SpinningGunMesh.transform.Rotate(Vector3.up * RotationsPerSecond * 360f * Time.deltaTime);
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player" && !IsPickedUp)
        {
            var weildingGun = collider.gameObject.GetComponent<WeildingGun>();
            if (weildingGun)
            {
                PickupWeapon(weildingGun);
            }
        }
    }

    private void PickupWeapon(WeildingGun weildingGun)
    {
        weildingGun.EquipWeapon(PickupPrefab);
        IsPickedUp = true;
        Destroy(SpinningGunMesh);
        Light.GetComponent<Light>().intensity = 0.2f;
    }
}
