using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    //Options menu
    public AudioMixer AudioMix; 
    public void SetVolume(float volume)
    {
        AudioMix.SetFloat("volume", volume);
    }

    public void GraphicsQuality(int QualityIndex) 
    {
        QualitySettings.SetQualityLevel(QualityIndex);
        Debug.Log(QualityIndex);
        //controls graphic quality, doesn't really serve a purpose for my graphics at this point.
    }

    public void SetFullScreen(bool IsFullScreen)
    {
        Screen.fullScreen = IsFullScreen;
        Debug.Log("Is full screen" + IsFullScreen);
    }
   
}
