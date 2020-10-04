using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum eInstruments
{
    eNone = 0,
    eBass,
    eDrums,
    eSaw,
    eLead,
    eVocals
}

public class BackgroundMusic : MonoBehaviour
{

    public eInstruments currentInstrumentLevel = eInstruments.eNone;
    public List<AudioClip> audioClips = new List<AudioClip>();
    private AudioSource myAudioSource = new AudioSource();
    private bool myPlayNextInstrument;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("DebugNextInstrument"))
        {
            TriggerNextInstrumentLevel();
        }
        if (myPlayNextInstrument)
        {
            myAudioSource.loop = false;
            if (!myAudioSource.isPlaying)
            {
                myAudioSource.clip = audioClips[(int)currentInstrumentLevel - 1];
                myAudioSource.loop = true;
                myPlayNextInstrument = false;
                myAudioSource.Play();
                GameObject.FindWithTag("Player").GetComponent<Appreciate>().TriggerNewMusicAppreciationRequirement();
            }
        }
    }

    public void TriggerNextInstrumentLevel()
    {
        if (currentInstrumentLevel != eInstruments.eVocals)
        {
            currentInstrumentLevel++;
            myPlayNextInstrument = true;
        }
    }
}
