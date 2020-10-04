using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour
{
    public CameraFader CameraFader;

    void Start()
    {
        CameraFader.FadeFromColor(Color.black, 1f);
    }

    void Kill()
    {
        CameraFader.FadeToColor(Color.black, 2.5f);
        Invoke("ReloadLevel", 2.5f);
        gameObject.GetComponent<FPSMovement>().enabled = false;
        gameObject.GetComponent<WeildingGun>().enabled = false; 
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
