using System;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static soundManager instance;

    void Awake ()
    {
        // Sets the soundManager's instance.
        if(instance == null)
        {
            instance = this;
        }
        
        // Adds settings for easier use of sounds, with Unity's tools.
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }
    
    // A function to play sounds determined by name.
    public void playSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }
}
