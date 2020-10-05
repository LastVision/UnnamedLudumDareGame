using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POI_02 : MonoBehaviour
{
    [SerializeField]
    public float spherePhaseModifier = 0.25f;
    private GameObject childSphere;
    private Vector3 childSphereStartPosition;
    private Vector3 childSphereCurrentPosition;
    // Start is called before the first frame update
    void Start()
    {
        childSphere = gameObject.transform.Find("Sphere").gameObject;
        childSphereStartPosition = childSphere.transform.localPosition;
        childSphereCurrentPosition = childSphereStartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        childSphereCurrentPosition.y = childSphereStartPosition.y + (Mathf.Sin(Time.fixedTime) * spherePhaseModifier);
        childSphere.transform.localPosition = childSphereCurrentPosition;
        childSphere.transform.Rotate(Vector3.up, (Mathf.Sin(Time.fixedTime) * spherePhaseModifier) * Mathf.PI, Space.Self);
    }
}
