using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndOfLevelTrigger : MonoBehaviour
{

    public CameraFader CameraFader;
    public string NextLevel;

    void OnTriggerEnter(Collider collider)
    {   
        if (collider.tag != "Player")
        {
            return;
        }

        Invoke("Endlevel", 3f);
    }


    void Endlevel()
    {
        if (CameraFader)
        {
            CameraFader.FadeToColor(Color.black, 2.5f);
        }

         Invoke("AfterCameraFace", 2.6f);

    }

    void AfterCameraFace()
    {   
        if (NextLevel != "")
        {
            SceneManager.LoadScene(NextLevel);
        }
        else
        {
            SceneManager.LoadScene(0);
            Debug.Log("Next level was not set, load level index 0");
        }
    }
}
