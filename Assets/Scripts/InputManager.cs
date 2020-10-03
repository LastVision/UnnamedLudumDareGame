using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("R")) // RESTART - TEST FOR LATER RESET FUNCTION
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
