using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour
{
    public CameraFader CameraFader;
    private bool doingRestartForFallingInVoid = false;

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

    void Update()
    {
        if(transform.position.y < -100 && doingRestartForFallingInVoid == false)
        {
            doingRestartForFallingInVoid = true;
            ReloadLevel();
        }
    }
}
