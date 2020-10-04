using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFader : MonoBehaviour
{
    public Image fadeImage;
    public void FadeToColor(Color color, float fadeTime)
    {
        if (fadeImage)
        {
            fadeImage.color = color;
            fadeImage.canvasRenderer.SetAlpha(0.0f);
            fadeImage.CrossFadeAlpha(1.0f, fadeTime, false);
        }
        else
        {
            Debug.Log("Fade image is null");
        }
    }

    public void FadeFromColor(Color color, float fadeTime)
    {
        if (fadeImage)
        {
            fadeImage.color = color;
            fadeImage.canvasRenderer.SetAlpha(1.0f);
            fadeImage.CrossFadeAlpha(0.0f, fadeTime, false);
        }
        else
        {
            Debug.Log("Fade image is null");
        }
    }
}
