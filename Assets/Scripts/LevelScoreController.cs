using UnityEngine;
using UnityEngine.UI;

public class LevelScoreController : MonoBehaviour
{
    public GameObject[] levelContainers; // Assign Level1, Level2, etc., in the Inspector.
    public Sprite lockedLevelSprite;    // Assign a "locked" sprite for locked levels.

    void Start()
    {
        UpdateAllLevelStates();
    }

    public void UpdateAllLevelStates()
    {
        for (int i = 0; i < levelContainers.Length; i++)
        {
            int score = PlayerPrefs.GetInt($"Level{i + 1}_Score", 0);
            Button levelButton = levelContainers[i].GetComponent<Button>();

            // Find or create the LockedOverlay
            Transform lockedOverlay = levelContainers[i].transform.Find("LockedOverlay");
            if (lockedOverlay == null)
            {
                lockedOverlay = CreateLockedOverlay(levelContainers[i]);
            }

            RectTransform rectTransform = lockedOverlay.GetComponent<RectTransform>();

            if (rectTransform != null)
            {
                rectTransform.sizeDelta = new Vector2(155, 170); // Set size to 155x170
            }

            lockedOverlay.localScale = new Vector3(0.4f, 0.4f, 1f);

            

            // Ensure Level 1 is always unlocked
            if (i == 0)
            {
                levelButton.interactable = true;
                lockedOverlay.gameObject.SetActive(false);
            }
            else
            {
                // Determine if the level is locked or unlocked
                if (score >= 2 || (i > 0 && PlayerPrefs.GetInt($"Level{i}_Score", 0) >= 2))
                {
                    // Unlock the level if its own score is >= 2 or the previous level's score is >= 2
                    levelButton.interactable = true;
                    lockedOverlay.gameObject.SetActive(false);
                }
                else
                {
                    // Keep the level locked
                    levelButton.interactable = false;
                    lockedOverlay.gameObject.SetActive(true);
                }
            }

            // Update the stars (optional)
            Transform starWrapper = levelContainers[i].transform.Find("StarWrapper");
            if (starWrapper != null)
            {
                UpdateStars(starWrapper, score);
            }

            if (i == 5)
            {
                levelButton.interactable = false;
                lockedOverlay.gameObject.SetActive(true);
            }
        }
    }

    Transform CreateLockedOverlay(GameObject levelContainer)
    {
        // Create a new GameObject for the LockedOverlay
        GameObject overlay = new GameObject("LockedOverlay");
        overlay.transform.SetParent(levelContainer.transform);
        overlay.transform.localPosition = Vector3.zero;
        overlay.transform.localScale = Vector3.one;

        // Add an Image component to the overlay
        Image overlayImage = overlay.AddComponent<Image>();
        overlayImage.sprite = lockedLevelSprite;
        overlayImage.color = Color.white; // Ensure the locked sprite is fully visible
        overlayImage.raycastTarget = true; // Block clicks on elements behind the overlay

        // Stretch the overlay to cover the entire level container
        RectTransform rectTransform = overlay.GetComponent<RectTransform>();
        rectTransform.anchorMin = Vector2.zero;
        rectTransform.anchorMax = Vector2.one;
        rectTransform.offsetMin = Vector2.zero;
        rectTransform.offsetMax = Vector2.zero;

        return overlay.transform;
    }

    void UpdateStars(Transform starWrapper, int score)
    {
        for (int j = 0; j < 3; j++) // Assume 3 stars per level
        {
            Image starImage = starWrapper.GetChild(j).GetComponent<Image>();

            // Set stars as bright or dimmed based on score
            if (starImage != null)
            {
                Color color = j < score ? Color.white : Color.black; // Adjust colors as needed
                starImage.color = color;
            }
        }
    }
}
