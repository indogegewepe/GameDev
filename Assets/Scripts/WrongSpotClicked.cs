using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class WrongClickHandler : MonoBehaviour
{
    public RectTransform imageA;
    public RectTransform imageB;
    private Sprite wrongSprite; // Holds the sprite loaded from Resources
    private AudioClip wrongSound; // Holds the audio clip loaded from Resources
    private AudioSource audioSource; // AudioSource to play the sound

    void Start()
    {
        // Load the sprite and sound from Resources folder
        wrongSprite = Resources.Load<Sprite>("wrongSprite"); // Sprite in Resources folder
        wrongSound = Resources.Load<AudioClip>("Sounds/wrongSound"); // Audio clip in Resources/Sounds folder

        // Ensure we have an AudioSource on this GameObject
        audioSource = gameObject.AddComponent<AudioSource>();

        // Ensure that SFX mute status is loaded (default to false if not set)
        bool isMuted = PlayerPrefs.GetInt("SFXMuted", 0) == 1;
        audioSource.mute = isMuted;  // Set the mute status on the AudioSource
    }

    public void ShowWrongSprite(string clickedImage)
    {
        if (wrongSprite != null && wrongSound != null)
        {
            // Play the wrong sound only if not muted
            if (!audioSource.mute)
            {
                PlayWrongSound();
            }

            // Create the primary wrong sprite
            GameObject wrongSpriteObject = new GameObject("WrongSprite");
            wrongSpriteObject.transform.SetParent(GameObject.Find("GamePanel").transform, false);

            // Set up the Image component with the loaded sprite
            Image img = wrongSpriteObject.AddComponent<Image>();
            img.sprite = wrongSprite;
            img.SetNativeSize(); // Ensure sprite uses its native size
            RectTransform rectTransform = wrongSpriteObject.GetComponent<RectTransform>();
            rectTransform.sizeDelta = new Vector2(80, 80);  // Adjusted to consistent size
            rectTransform.localScale = Vector3.one;  // Set scale to 1

            // Position the primary sprite at the click location
            wrongSpriteObject.transform.position = Input.mousePosition;

            // Start shaking animation on the wrong sprite
            StartCoroutine(ShakeSprite(rectTransform));

            // Find ImageA and ImageB
            RectTransform imageATransform = GameObject.Find("ImageA").GetComponent<RectTransform>();
            RectTransform imageBTransform = GameObject.Find("ImageB").GetComponent<RectTransform>();

            // Calculate local position within clicked image
            Vector2 localPosition;
            RectTransform clickedTransform = clickedImage == "ImageA" ? imageATransform : imageBTransform;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(clickedTransform, Input.mousePosition, null, out localPosition);

            // Create mirrored sprite for the other image
            GameObject mirroredSpriteObject = new GameObject("MirroredWrongSprite");
            mirroredSpriteObject.transform.SetParent(GameObject.Find("GamePanel").transform, false);

            // Set up the mirrored Image component
            Image mirroredImg = mirroredSpriteObject.AddComponent<Image>();
            mirroredImg.sprite = wrongSprite;
            mirroredImg.SetNativeSize(); // Ensure sprite uses its native size
            RectTransform mirroredRectTransform = mirroredSpriteObject.GetComponent<RectTransform>();
            mirroredRectTransform.sizeDelta = new Vector2(80, 80); // Adjusted to consistent size

            RectTransform targetTransform = clickedImage == "ImageA" ? imageBTransform : imageATransform;
            mirroredSpriteObject.transform.position = targetTransform.TransformPoint(localPosition);

            // Start shaking animation on the mirrored sprite as well
            StartCoroutine(ShakeSprite(mirroredRectTransform));

            // Call the wrong penalty method
            FindObjectOfType<TimerControllerMove>().WrongPenalty();

            Destroy(wrongSpriteObject, 1f);
            Destroy(mirroredSpriteObject, 1f);
        }
        else
        {
            Debug.LogError("Sprite or Sound not found in Resources!");
        }
    }

    // Coroutine to handle the shake animation
    private IEnumerator ShakeSprite(RectTransform rectTransform)
    {
        Vector3 originalPosition = rectTransform.localPosition;
        float shakeDuration = 0.5f; // Duration of the shake
        float shakeMagnitude = 5f; // Magnitude of the shake

        float elapsed = 0.0f;

        while (elapsed < shakeDuration)
        {
            float shakeX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float shakeY = Random.Range(-shakeMagnitude, shakeMagnitude);

            rectTransform.localPosition = originalPosition + new Vector3(shakeX, shakeY, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset the position after shaking
        rectTransform.localPosition = originalPosition;
    }

    // Method to play the wrong sound
    private void PlayWrongSound()
    {
        if (wrongSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(wrongSound);
        }
        else
        {
            Debug.LogError("Sound not found or AudioSource missing!");
        }
    }

    // Call this function to toggle mute status (can be linked to a UI button)
    public void ToggleMute()
    {
        audioSource.mute = !audioSource.mute;

        // Save mute status using PlayerPrefs
        PlayerPrefs.SetInt("SFXMuted", audioSource.mute ? 1 : 0);
        PlayerPrefs.Save();
    }
}