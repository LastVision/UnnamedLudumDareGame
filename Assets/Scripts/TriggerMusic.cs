using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMusic : MonoBehaviour
{
    private bool myHasBeenTriggered = false;
    public GameObject Jukebox = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collider)
    {
        if (!myHasBeenTriggered)
        {
            if (collider.tag == "Player")
            {
                if (Jukebox)
                {
                    Jukebox.GetComponent<BackgroundMusic>().TriggerNextInstrumentLevel();
                    myHasBeenTriggered = true;
                }
            }
        }
    }


}
