using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appreciate : MonoBehaviour
{
    private List<AudioClip> myAppreciateAudioClips = new List<AudioClip>();

    // Start is called before the first frame update

    void Start()
    {
        for (int i = 1; i <= 6; i++)
        {
            AudioClip clip = (AudioClip)Resources.Load("Sound/Appreciate/" + i.ToString() + ".ogg");
            myAppreciateAudioClips.Add(clip);
        }
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void TryToAppreciate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
            AppreciateObject(hit.collider.gameObject);
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
            Debug.Log("Did not Hit");
        }
    }

    private void AppreciateObject(GameObject foundObject)
    {
        AudioSource audioData = gameObject.GetComponent<AudioSource>();
        audioData.PlayOneShot(myAppreciateAudioClips[Random.Range(0, myAppreciateAudioClips.Count)]);
    }
}
