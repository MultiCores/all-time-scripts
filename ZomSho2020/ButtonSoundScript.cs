using UnityEngine;

public class ButtonSoundScript : MonoBehaviour
{
    public AudioSource buttonPress;

    public void PlaySoundEffect()
    {
        buttonPress.Play();
    }
}
