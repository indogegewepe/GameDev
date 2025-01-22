using UnityEngine;

public class SpotTheDifferenceController : MonoBehaviour
{
    public static SpotTheDifferenceController Instance; // Singleton instance
    public int totalDifferences;                 // Total differences to find
    private int differencesFound = 0;                  // Count of found differences
    public GameObject endPanel;
    public GameObject gamePanel;
    public GameObject pauseButton;

    void Awake()
    {
        // Ensure there's only one instance of this controller
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instance
        }
    }

    // Call this method to set the total differences for the level
    public void SetTotalDifferences(int count)
    {
        totalDifferences = count;
    }

    // Call this method when a difference is found
    public void DifferenceFound()
    {
        differencesFound++;
        Debug.Log($"Differences found: {differencesFound}/{totalDifferences}");

        // Check if all differences have been found
        if (differencesFound >= totalDifferences)
        {
            EndLevel(); // End the level or trigger victory logic
        }
    }

    private void EndLevel()
    {
        // Logic for what happens when all differences are found
        Debug.Log("All differences found! Level complete.");
        FindObjectOfType<TimerControllerMove>().StopTimer();
        FindObjectOfType<GameManager>().CalculateFinalScore();
        endPanel.SetActive(true);
        gamePanel.SetActive(false);
        pauseButton.SetActive(false);
    }
}
