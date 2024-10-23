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
        if (gameManager != null)
        {
            gameManager.beatLevel += PlayWinSFX;
        }
        if (playerController != null)
        {
            playerController.playerDiedEvent += PlayDeathSFX;
            playerController.enemyKilledEvent += PlayKillSFX;
        }
    }

    void PlayWinSFX()
    {
        Debug.Log("3");
        if (WinSFX != null)
        {
            audioSource.clip = WinSFX;
            WinSFX.Play();
        } 
    }
    
    void PlayDeathSFX()
    {
        Debug.Log("1");
        if (DeathSFX != null)
        {
            audioSource.clip = DeathSFX;
            DeathSFX.Play();
        }
    }

    void PlayKillSFX()
    {
        Debug.Log("2");
        if (KillSFX != null)
        {
            audioSource.clip = KillSFX;
            KillSFX.Play();
        }
    }

}
    

