using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFader : MonoBehaviour
{
    public Image fadeImage;
    public void FadeToColor(Color color, float fadeTime)
    {
       FadeToColor(color, 0f, 1f, fadeTime);
    }

    public void FadeFromColor(Color color, float fadeTime)
    {
        FadeToColor(color, 1f, 0f, fadeTime);
    }

    public void FadeToColor(Color color, float startAlpha, float endAlpha, float fadeTime)
    {
        if (fadeImage)
        {
            fadeImage.color = color;
            fadeImage.canvasRenderer.SetAlpha(startAlpha);
            fadeImage.CrossFadeAlpha(endAlpha, fadeTime, false);
        }
        else
        {
            Debug.Log("Fade image is null");
        }
    }
}
