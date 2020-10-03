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
        if (Input.GetKeyDown("E")) // APPRECIATE
        {
            Appreciate appreciateScript = (Appreciate)GameObject.FindWithTag("Player").GetComponent(typeof(Appreciate));
            appreciateScript.TryToAppreciate();
            Debug.Log("Pressed E");
        }
        if (Input.GetKeyDown("R")) // RESTART - TEST FOR LATER RESET FUNCTION
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
