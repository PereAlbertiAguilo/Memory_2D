using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioClip clickSound;

    public void ClickSound()
    {
        GetComponent<PauseMenu>()._sfxAduioSource.PlayOneShot(clickSound, 0.2f);
    }
}
