using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    // A set of settings for easier use of sounds in Unity, with Unity's preset tools.
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(.1f, 3f)]
    public float pitch;

    [HideInInspector]
    public AudioSource source;
}
