using UnityEngine;
using System.Collections;

public class BGMMusic : MonoBehaviour
{
    private AudioSource audioSource; // To control music playback
    public float fadeDuration = 0.5f; // Duration of the fade effect

    public AudioClip levelMusic; // Reference to the music for "Level" scenes
    public AudioClip menuMusic; // Reference to the music for the "MainMenu" scene

    private void Awake()
    {
        // Find the AudioSource component attached to this GameObject
        audioSource = GetComponent<AudioSource>();

        GameObject[] musicObj = GameObject.FindGameObjectsWithTag("GameMusic");
        if (musicObj.Length > 1)
        {
            Destroy(this.gameObject); // Destroy duplicate music objects
        }
        else
        {
            DontDestroyOnLoad(this.gameObject); // Don't destroy on scene load
        }
    }

    private void Update()
    {
        // Check if we are in a "Level" scene and fade out the music
        if (GameObject.FindWithTag("Level") != null)
        {
            // Change music and fade out if music is playing
            if (audioSource.isPlaying && audioSource.clip != levelMusic)
            {
                StartCoroutine(FadeOut(levelMusic));
            }
            else if (!audioSource.isPlaying || audioSource.clip != levelMusic)
            {
                StartCoroutine(FadeIn(levelMusic));
            }
        }
        // Check if we are in the "MainMenu" scene and fade in the music
        else if (GameObject.FindWithTag("MainMenu") != null)
        {
            // Change music and fade in if music is not playing
            if (!audioSource.isPlaying || audioSource.clip != menuMusic)
            {
                StartCoroutine(FadeIn(menuMusic));
            }
        }
    }

    // Coroutine to fade out the music to a new clip
    private IEnumerator FadeOut(AudioClip newClip)
    {
        float startVolume = audioSource.volume; // Get current volume
        float targetVolume = 0f; // Target volume is 0

        // Fade out over the specified duration
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            audioSource.volume = Mathf.Lerp(startVolume, targetVolume, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the volume is 0 and stop the current music
        audioSource.volume = 0;
        audioSource.Stop();

        // Switch to the new clip
        audioSource.clip = newClip;
        audioSource.Play(); // Start playing the new music
        StartCoroutine(FadeIn(newClip)); // Fade in the new music
    }

    // Coroutine to fade in the music
    private IEnumerator FadeIn(AudioClip newClip)
    {
        // Start with volume at 0
        audioSource.clip = newClip; // Set the new music clip
        audioSource.volume = 0;
        audioSource.Play();

        // Fade in the volume over time
        float targetVolume = 1f;
        while (audioSource.volume < targetVolume)
        {
            audioSource.volume += Time.deltaTime / fadeDuration;
            yield return null;
        }

        // Ensure the volume is exactly 1 (full volume)
        audioSource.volume = 1;
    }
}
