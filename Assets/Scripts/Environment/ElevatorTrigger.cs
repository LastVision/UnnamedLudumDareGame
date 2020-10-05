using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField]
    public GameObject elevator;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(elevator)
            {
                Elevator elevatorScript = elevator.GetComponent<Elevator>();
                elevatorScript.OpenDoor();
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(elevator)
            {
                Elevator elevatorScript = elevator.GetComponent<Elevator>();
                elevatorScript.CloseDoor();
            }
        }
    }
}
