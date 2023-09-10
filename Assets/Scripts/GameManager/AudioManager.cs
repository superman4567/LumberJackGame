using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixer audioMixer; 
    public AudioSource[] soundEffects; 
    public AudioSource[] music; 

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            PlayMusic(0);
        }

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            PlayMusic(0);
        }

        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            PlayMusic(0);
        }
    }

    public void PlaySoundEffect(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < soundEffects.Length)
            soundEffects[soundIndex].Play();
    }

    public void PlayMusic(int soundIndex)
    {
        if (!music[soundIndex].isPlaying)
        {
            music[soundIndex].Play();
        }
            
    }

    public void StopMusic(int soundIndex)
    {
        music[soundIndex].Stop();
    }
}

