using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour
{

    public AudioMixer audioMixer;
    
    // Sets the volume to said float value in the element 'audioMixer'. 
    // Simply made to transfer the audio volume between the mainmenu and ingame settings midst gameplay.
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
    
    // Checks if fullscreen is on or off.
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
