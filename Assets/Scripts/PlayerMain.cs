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

    void Kill(float delay)
    {
        CameraFader.FadeToColor(Color.black, delay);
        Invoke("ReloadLevel", delay);
        gameObject.GetComponent<FPSMovement>().enabled = false;
        gameObject.GetComponent<WeildingGun>().enabled = false; 
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
