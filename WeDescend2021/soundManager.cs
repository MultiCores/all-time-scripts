using System;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    public Sound[] sounds;

    public static soundManager instance;

    void Awake ()
    {
        if(instance == null)
        {
            instance = this;
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
        }
    }

    public void playSound(string name)
    {
        /*switch (soundStr)
        {
            case "gravel1": source.PlayOneShot(sounds[0]);
                break;
        }*/

        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    /*public void playGunFire(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayOneShot(name, 1f);
    }
    */
}
