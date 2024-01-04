using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

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
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // Play the MainMenuTheme with fade in
        PlayMusic("MainMenuTheme");

        // Play the CrowdTalkingSmall with fade in
        PlayMusic("CrowdTalkingSmall");
    }


    public void PlayMusic(string name, float fadeDuration)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            // Set the clip and play the sound effect
            musicSource.clip = s.clip;
            musicSource.Play();

            // Start fading in the music
            StartCoroutine(FadeIn(musicSource, fadeDuration));
        }
    }

    private IEnumerator FadeIn(AudioSource audioSource, float fadeDuration)
    {
        float elapsedTime = 0f;

        // Gradually increase the volume
        while (elapsedTime < fadeDuration)
        {
            float newVolume = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            audioSource.volume = newVolume;

            // Wait for the next frame
            yield return null;

            // Increase the elapsed time
            elapsedTime += Time.deltaTime;
        }

        // Ensure the volume is set to 1 at the end
        audioSource.volume = 1f;
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
        // Create a new GameObject for the sound effect
        GameObject musicObject = new GameObject("SFX_" + name);
        AudioSource musicSource = musicObject.AddComponent<AudioSource>();

        // Set the clip and play the sound effect    

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
