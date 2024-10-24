using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXObserver : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip WinSFX;
    public AudioClip DeathSFX;
    public AudioClip KillSFX;

    public GameManager gameManager;
    public PlayerController playerController; 

    void Awake()
    {
        audioSource = GetComponent<AudioSource>(); 
    }

    private void OnEnable()
    {
        if (playerController != null)
        {
            playerController.playerDiedEvent += PlayDeathSFX;
            playerController.enemyKilledEvent += PlayKillSFX;
        }
    }

    public void PlayWinSFX()
    {
        //Debug.Log("3");
        if (WinSFX != null)
        {
            audioSource.clip = WinSFX;
            audioSource.Play();
        } 
    }
    
    void PlayDeathSFX()
    {
        //Debug.Log("1");
        if (DeathSFX != null)
        {
            audioSource.clip = DeathSFX;
            audioSource.Play();
        }
    }

    void PlayKillSFX()
    {
        //Debug.Log("2");
        if (KillSFX != null)
        {
            audioSource.clip = KillSFX;
            audioSource.Play();
        }
    }

}
    

