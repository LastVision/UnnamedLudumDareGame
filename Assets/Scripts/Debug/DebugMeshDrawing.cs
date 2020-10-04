using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMeshDrawing : MonoBehaviour
{
    [SerializeField]
    public Mesh mesh = null;
    [SerializeField]
    public Color color = Color.gray;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnDrawGizmos()
    {
        if(mesh)
        {
            Gizmos.color = color;
            Gizmos.DrawMesh(mesh, -1, transform.position, transform.rotation, transform.localScale);
        }
    }
}
