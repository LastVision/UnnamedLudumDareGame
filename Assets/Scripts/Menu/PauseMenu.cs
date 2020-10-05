using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public CameraFader CameraFader;
    private bool IsPaused = false;

    private GameObject[] pauseObjects;


    void Start()
    {
        pauseObjects = GameObject.FindGameObjectsWithTag("PauseMenu");
        SetPauseMenuActive(false);
    }
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            if (IsPaused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }
    }

    void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        IsPaused = true;
        Time.timeScale = 0;
        CameraFader.FadeToColor(Color.black, 0f, 0.6f, 0f);
        SetPauseMenuActive(true);
    }
    void Unpause()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        IsPaused = false;
        Time.timeScale = 1;
        CameraFader.FadeToColor(Color.black, 0f, 0f, 0f);
        SetPauseMenuActive(false);
    }

    public void ContinueButton()
    {
        Unpause();
    }

    public void ExitButton()
    {
        Time.timeScale = 1;
        Cursor.visible = true;
        SceneManager.LoadScene("StartMenu");
    }

    void SetPauseMenuActive(bool active)
    {
        foreach(GameObject g in pauseObjects)
        {
            g.SetActive(active);
        }
    }
}
