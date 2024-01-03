using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("MainMenuTheme");
    }

    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else 
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }

        else
    {
        // Create a new GameObject for the sound effect
        GameObject sfxObject = new GameObject("SFX_" + name);
        AudioSource sfxAudioSource = sfxObject.AddComponent<AudioSource>();

        // Set the clip and play the sound effect
        sfxAudioSource.clip = s.clip;
        sfxAudioSource.Play();

        // Destroy the GameObject after the sound finishes playing
        Destroy(sfxObject, s.clip.length);
    }
    }

    public void FadeOutAndStopMusic(float fadeOutTime)
    {
        if (AudioManager.Instance.musicSource != null)
        {
           StartCoroutine(FadeOutAndStop(AudioManager.Instance.musicSource, fadeOutTime));
        }
        else
        {
            Debug.LogError($"AudioSource to fade out not found.");
        } 
    }

    private IEnumerator FadeOutAndStop(AudioSource audioSource, float fadeOutTime)
    {
        // Store the initial volume for later reference
        float initialVolume = audioSource.volume;

        // Wait for the next frame
        yield return null;

        // Calculate the new volume based on the elapsed time
        float elapsedTime = 0f;
        while (elapsedTime < fadeOutTime)
        {
            float newVolume = Mathf.Lerp(initialVolume, 0f, elapsedTime / fadeOutTime);

            // Apply the new volume to the AudioSource
            audioSource.volume = newVolume;

            // Wait for the next frame
            yield return null;

            // Increase the elapsed time
            elapsedTime += Time.deltaTime;
        }

        // Stop the AudioSource
        audioSource.Stop();

        // Reset the volume to the initial value
        audioSource.volume = initialVolume;
    }
}
