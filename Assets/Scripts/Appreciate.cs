using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Appreciate : MonoBehaviour
{
    public List<AudioClip> AppreciateAudioClips = new List<AudioClip>();
    public List<AudioClip> MusicStrikeAudioClips = new List<AudioClip>();
    public GameObject UIHand = null;
    public GameObject AppreciateMusic = null;
    private int myLastPlayedAppreciateIndex = 0;
    private float myAppreciatingCooldownTimer = 0.0f;
    private float myAppreciateCooldown;
    private bool myHaveToAppreciateMusic;
    private float myHaveToAppreciateMusicTimer;
    private float myHaveToAppreciateMusicDeadline = 5.0f;
    // Start is called before the first frame update

    void Start()
    {
        if (UIHand)
        {
            UIHand.GetComponent<RawImage>().enabled = false;
        }
        if (AppreciateMusic)
        {
            AppreciateMusic.GetComponent<RawImage>().enabled = false;
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
            if (myHaveToAppreciateMusic)
            {
                AppreciateMusicFunction();
            }
            else
            {
                TryToAppreciate();
            }
        }
        if (myHaveToAppreciateMusic)
        {
            if (myHaveToAppreciateMusicTimer > 0.0f)
            {
                myHaveToAppreciateMusicTimer -= Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<Strikes>().ReceiveStrike(MusicStrikeAudioClips[Random.Range(0, MusicStrikeAudioClips.Count - 1)]);
                AppreciateMusic.GetComponent<RawImage>().enabled = false;
                myHaveToAppreciateMusicTimer = 0.0f;
                myHaveToAppreciateMusic = false;
            }
        }
    }

    public void TryToAppreciate()
    {
        int layerMaskAll = ~0;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMaskAll))
        {
            AppreciateObject(hit.collider.gameObject);
        }
    }

    private void AppreciateObject(GameObject foundObject)
    {
        var ap = foundObject.GetComponent<Appreciateable>();
        Appreciateable ap_parent = null;
        if (foundObject.transform.parent)
        {
            ap_parent = foundObject.transform.parent.GetComponent<Appreciateable>();
        }
        if (ap)
        {
            ap.Appreciate();
            TriggerAppreciationEffect();
        }
        else if (ap_parent)
        {
            ap_parent.Appreciate();
            TriggerAppreciationEffect();
        }
    }

    private void AppreciateMusicFunction()
    {
        TriggerAppreciationEffect();
        AppreciateMusic.GetComponent<RawImage>().enabled = false;
        myHaveToAppreciateMusicTimer = 0.0f;
        myHaveToAppreciateMusic = false;
    }

    private void TriggerAppreciationEffect()
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

    public void TriggerNewMusicAppreciationRequirement()
    {
        myHaveToAppreciateMusic = true;
        myHaveToAppreciateMusicTimer = myHaveToAppreciateMusicDeadline;
        AppreciateMusic.GetComponent<RawImage>().enabled = true;
    }
}
