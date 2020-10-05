using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Strikes : MonoBehaviour
{
    public GameObject UI_Strike_Icon;
    public GameObject UI_Anchor;
    public List<AudioClip> StrikeSounds = new List<AudioClip>();
    
    int NbrOfStrikes = 0;
    int MaxStrikes = 3;


    private bool strikeSoundShouldPlay = false;
    private float strikeSoundTimer = 0;

    void Update()
    {
        if (strikeSoundShouldPlay)
        {
            if (strikeSoundTimer > 0)
            {
                strikeSoundTimer -= Time.deltaTime;
            }
            else
            {
                gameObject.GetComponent<AudioSource>().PlayOneShot(StrikeSounds[NbrOfStrikes - 1]);
                strikeSoundTimer = 0;
                strikeSoundShouldPlay = false;
            }
        }
    }
    public void ReceiveStrike(AudioClip strikeSound)
    {
        ++NbrOfStrikes;
        
        if (NbrOfStrikes > MaxStrikes)
        {
            OneTooMany();
        }
        else if (UI_Anchor && UI_Strike_Icon)
        {
            gameObject.GetComponent<AudioSource>().Stop();
            gameObject.GetComponent<AudioSource>().PlayOneShot(strikeSound);
            strikeSoundTimer = strikeSound.length + 0.5f;
            strikeSoundShouldPlay = true;
            float width = UI_Strike_Icon.GetComponent<RectTransform>().rect.width;
            var strike = Instantiate(UI_Strike_Icon, Vector3.zero, Quaternion.identity);
            strike.transform.parent = UI_Anchor.transform;
            strike.transform.localPosition = Vector3.right * width * (NbrOfStrikes - 1);
        }

    }

    void OneTooMany()
    {
        gameObject.GetComponent<CameraFader>().FadeToColor(Color.black, 2.5f);
        Invoke("ReloadLevel", 2.5f);
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene("Level");
    }
}
