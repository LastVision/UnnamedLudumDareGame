using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    public bool isOpen = true;

    private GameObject LeftDoor;
    private GameObject RightDoor;

    private Vector3 LeftDoorOrginalPosition;
    private Vector3 RightDoorOrginalPosition;

    private Vector3 DoorClosedAxisValue = new Vector3(0, 0, 0.5f);
    private float elapsedTime = 0.0f;
    private float transitionTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        GameObject baseObject = gameObject.transform.Find("Base").gameObject;

        LeftDoor = baseObject.transform.Find("Door_Left").gameObject;
        RightDoor = baseObject.transform.Find("Door_Right").gameObject;

        LeftDoorOrginalPosition = LeftDoor.transform.localPosition;
        RightDoorOrginalPosition = RightDoor.transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.fixedDeltaTime;
        float doorTransitionTime = Mathf.Min(elapsedTime, transitionTime) / transitionTime;
        
        if(isOpen)
        {
            LeftDoor.transform.localPosition = Vector3.Lerp(LeftDoorOrginalPosition - DoorClosedAxisValue, LeftDoorOrginalPosition, doorTransitionTime);
            RightDoor.transform.localPosition = Vector3.Lerp(RightDoorOrginalPosition + DoorClosedAxisValue, RightDoorOrginalPosition, doorTransitionTime);
        }
        else
        {
            LeftDoor.transform.localPosition = Vector3.Lerp(LeftDoorOrginalPosition, LeftDoorOrginalPosition - DoorClosedAxisValue, doorTransitionTime);
            RightDoor.transform.localPosition = Vector3.Lerp(RightDoorOrginalPosition, RightDoorOrginalPosition + DoorClosedAxisValue, doorTransitionTime);
        }
    }

    public void OpenDoor()
    {
        isOpen = true;
        elapsedTime = 0.0f;
    }

    public void CloseDoor()
    {
        isOpen = false;
        elapsedTime = 0.0f;
    }
}
