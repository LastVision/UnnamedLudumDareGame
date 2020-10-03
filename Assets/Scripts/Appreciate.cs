using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Appreciate : MonoBehaviour
{
    public List<AudioClip> AppreciateAudioClips = new List<AudioClip>();
    public GameObject UIHand = null;
    private int myLastPlayedAppreciateIndex = 0;
    private float myAppreciatingCooldownTimer = 0.0f;
    private float myAppreciateCooldown;
    // Start is called before the first frame update

    void Start()
    {
        UIHand = GameObject.FindGameObjectsWithTag("UIHand")[0];
        if (UIHand)
        {
            UIHand.GetComponent<RawImage>().enabled = false;
        }
        myAppreciateCooldown = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {    
        if (myAppreciatingCooldownTimer > 0.0f)
        {
            myAppreciatingCooldownTimer -= Time.deltaTime;
            if (myAppreciatingCooldownTimer <= 0.0f)
            {
                UIHand.GetComponent<RawImage>().enabled = false;
            }
        }
        else if (Input.GetButtonDown("Appreciate"))
        {
            TryToAppreciate();
        }

    }

    public void TryToAppreciate()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask))
        {
            Vector3 dir = (hit.point - Camera.main.transform.position).normalized;
            Vector3 start = Camera.main.transform.position + Camera.main.transform.up * -0.05f + dir * 0.2f;
            UtilityFunctions.DrawLine(start, hit.point, Color.yellow, 1.5f);
            Debug.Log("Did Hit");
            AppreciateObject(hit.collider.gameObject);
        }
        else
        {
            Vector3 dir = Camera.main.transform.forward;
            Vector3 start = Camera.main.transform.position + Camera.main.transform.up * -0.05f + dir * 0.2f;
            UtilityFunctions.DrawLine(start, dir * 1000f, Color.white, 1.5f);
            Debug.Log("Did not Hit");
        }
    }

    private void AppreciateObject(GameObject foundObject)
    {
        AudioSource audioData = gameObject.GetComponent<AudioSource>();
        int randomizedIndex = Random.Range(0, AppreciateAudioClips.Count);
        if (randomizedIndex == myLastPlayedAppreciateIndex) // Make sure we don't get same audio twice
        {
            if (randomizedIndex > 0)
            {
                randomizedIndex--;
            }
            else
            {
                randomizedIndex++;
            }
        }
        audioData.PlayOneShot(AppreciateAudioClips[randomizedIndex]);
        myLastPlayedAppreciateIndex = randomizedIndex;
        myAppreciatingCooldownTimer = myAppreciateCooldown;
        if (UIHand != null)
        {
            UIHand.GetComponent<RawImage>().enabled = true;
        }
    }
}
