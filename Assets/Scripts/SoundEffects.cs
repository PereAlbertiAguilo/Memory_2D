using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip clickSound;
    public AudioClip clickSound2;

    public void ClickSound()
    {
        GetComponent<PauseMenu>()._sfxAduioSource.PlayOneShot(clickSound, 0.2f);
    }

    public void ClickSound2(AudioSource a)
    {
        a.PlayOneShot(clickSound2, 0.5f);
    }
}
