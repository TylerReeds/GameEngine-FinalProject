using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXObserver : MonoBehaviour
{
    public AudioSource WinSFX;
    public AudioSource DeathSFX;
    public AudioSource KillSFX;

    void Awake()
    {
        WinSFX = GetComponent<AudioSource>(); 
        DeathSFX = GetComponent<AudioSource>(); 
        KillSFX = GetComponent<AudioSource>(); 
    }


    void PlayWinSFX()
    {
        if (WinSFX != null)
        {
            WinSFX.Play();
        }
    }
    
    void PlayDeathSFX()
    {
        if (DeathSFX != null)
        {
            DeathSFX.Play();
        }
    }

    void PlayKillSFX()
    {
        if (KillSFX != null)
        {
            KillSFX.Play();
        }
    }

}
    

