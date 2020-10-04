using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class EnviormentColor : MonoBehaviour
{
    [SerializeField]
    public Color emissiveColor = Color.red;

    public bool setEmissiveColor = false;

    void Update()
    {
       if(setEmissiveColor)
       {
           SetEmissiveColor();
       }
       setEmissiveColor = false;
    }

    private void SetEmissiveColor()
    {
        var renderers = GetComponentsInChildren<MeshRenderer>();
        foreach(MeshRenderer renderer in renderers)
        {
            renderer.material.SetColor("_emissiveColor", emissiveColor);
        }
    }
}
