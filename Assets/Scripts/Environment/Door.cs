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

    private Light Light1;
    private Light Light2;

    void Start()
    {

        DoorModel = gameObject.transform.Find("DoorModel").gameObject;
        if (DoorModel)
        {
            Light1 = DoorModel.transform.Find("Light1").gameObject.GetComponent<Light>();
            Light2 = DoorModel.transform.Find("Light2").gameObject.GetComponent<Light>();
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
        DoorModel.transform.localPosition = ClosePosition + Vector3.up * 3f;
        Light1.enabled = false;
        Light2.enabled = false;
    }

    private void Close()
    {
        gameObject.GetComponent<AudioSource>().clip = CloseDoorSound;
        gameObject.GetComponent<AudioSource>().Play();
        DoorModel.transform.localPosition = ClosePosition;
        Light1.enabled = true;
        Light2.enabled = true;
    }

    public void Unlock()
    {
        IsLocked = false;
        Light1.color = Color.green;
        Light2.color = Color.green;
    }
    public void Lock()
    {
        IsLocked = true;
        Light1.color = Color.red;
        Light2.color = Color.red;
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
