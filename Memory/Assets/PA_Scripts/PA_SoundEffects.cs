using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PA_SoundEffects : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip clickSound2;

    // Function that executes a sound effect
    public void ClickSound()
    {
        GetComponent<PA_PauseMenu>()._sfxAduioSource.PlayOneShot(clickSound, 0.2f);
    }

    // Function that executes a sound effect from an especific audioSource
    public void ClickSound2(AudioSource a)
    {
        a.PlayOneShot(clickSound2, 0.5f);
    }
}
