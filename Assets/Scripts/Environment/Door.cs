using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool StartsLocked = false;
    private bool IsLocked;
    Vector3 ClosePosition;
    private GameObject DoorModel;

    public AudioClip OpenDoorSound;
    public AudioClip CloseDoorSound;
    private Vector3 myTargetPosition;

    private Material DoorMaterial;

    void Start()
    {

        DoorModel = gameObject.transform.Find("DoorModel").gameObject;
        if (DoorModel)
        {
            DoorMaterial = DoorModel.GetComponent<MeshRenderer>().material;
            ClosePosition = DoorModel.transform.localPosition;
        }

        if (StartsLocked)
        {
            Lock();
        }
        else
        {
            Unlock();
        }
    }

    void Update()
    {
        if (Vector3.Distance(DoorModel.transform.localPosition, myTargetPosition) > 0.01f)
        {
            DoorModel.transform.localPosition = Vector3.Lerp(DoorModel.transform.localPosition, myTargetPosition, 0.02f);
        }
    }

    public void TryOpen()
    {
        if (!IsLocked)
        {
            Open();
        }
    }
    private void Open()
    {
        gameObject.GetComponent<AudioSource>().clip = OpenDoorSound;
        gameObject.GetComponent<AudioSource>().Play();
        myTargetPosition = ClosePosition + Vector3.up * 3.5f;
    }

    private void Close()
    {
        gameObject.GetComponent<AudioSource>().clip = CloseDoorSound;
        gameObject.GetComponent<AudioSource>().Play();
        myTargetPosition = ClosePosition;
    }

    public void Unlock()
    {
        IsLocked = false;
        DoorMaterial.SetColor("_doorEmissiveColor", Color.green);
    }
    public void Lock()
    {
        IsLocked = true;
        DoorMaterial.SetColor("_doorEmissiveColor", Color.red);
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag != "Player")
        {
            return;
        }
        TryOpen();
    }

    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag != "Player")
        {
            return;
        }
        Close();
    }
}
