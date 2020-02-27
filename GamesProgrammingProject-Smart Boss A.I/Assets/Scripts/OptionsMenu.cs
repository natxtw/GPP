using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer AudioMix;
    public void SetVolume(float volume)
    {
        AudioMix.SetFloat("volume", volume);
    }

    public void GraphicsQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
        Debug.Log(QualityIndex);
    }

    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
        Debug.Log("Is full screen" + IsFullScreen);
    }
   
}
