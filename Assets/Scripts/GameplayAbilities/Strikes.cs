using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Strikes : MonoBehaviour
{
    public GameObject UI_Strike_Icon;
    public GameObject UI_Anchor;
    
    int NbrOfStrikes = 0;
    int MaxStrikes = 3;

    public void ReceiveStrike()
    {
        ++NbrOfStrikes;
        if (NbrOfStrikes > MaxStrikes)
        {
            OneTooMany();
        }
        else if (UI_Anchor && UI_Strike_Icon)
        {
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
