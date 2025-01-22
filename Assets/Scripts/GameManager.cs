using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int currentLevel; // Set dynamically when the level starts
    public int currentLevelScore; // Current score for this level

    public GameObject endPanel; // Reference to your EndPanel
    public GameObject nextLevelButton; // Reference to the Next Level button

    [SerializeField] GameObject pauseMenu;

    private TimerControllerMove timerController;

    void Start()
    {
        timerController = GetComponent<TimerControllerMove>();
        if (timerController == null)
        {
            Debug.LogError("TimerController not found on this GameObject!");
        }
    }

    public void CalculateFinalScore()
    {
        if (timerController != null)
        {
            float currentFillAmount = timerController.GetFillAmount();

            if (currentFillAmount <= 0f)
            {
                currentLevelScore = 0;
            }
            else if (currentFillAmount <= 0.3f)
            {
                currentLevelScore = 1;
            }
            else if (currentFillAmount <= 0.7f)
            {
                currentLevelScore = 2;
            }
            else
            {
                currentLevelScore = 3;
            }

            SaveLevelScore(currentLevel, currentLevelScore);

            // Show the end panel
            ShowEndPanel();
        }
        else
        {
            Debug.LogError("Cannot calculate final score because TimerController is null.");
        }
    }

    private void SaveLevelScore(int level, int score)
    {
        int previousScore = PlayerPrefs.GetInt($"Level{level}_Score", 0);
        if (score > previousScore)
        {
            PlayerPrefs.SetInt($"Level{level}_Score", score);
            PlayerPrefs.Save();
        }
    }

    public void ShowEndPanel()
    {
        endPanel.SetActive(true);

        int nextLevelIndex = currentLevel + 1;
        bool nextLevelExists = nextLevelIndex <= SceneManager.sceneCountInBuildSettings - 1;

        Debug.Log($"Current Level Score: {currentLevelScore}");
        Debug.Log($"Timer Fill Amount: {timerController.GetFillAmount()}");
        Debug.Log($"Next Level Exists: {nextLevelExists}");

        if (currentLevelScore >= 2 && nextLevelExists && timerController.GetFillAmount() > 0f)
        {
            Debug.Log("Next Level Button Enabled");
            nextLevelButton.SetActive(true);
        }
        else
        {
            Debug.Log("Next Level Button Disabled");
            nextLevelButton.SetActive(false);
        }
    }


    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoHome()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("levelScene"); 
    }

    public void NextLevel()
    {
        int nextLevelIndex = currentLevel + 1;
        if (nextLevelIndex <= SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene($"Level{nextLevelIndex}"); 
        }
        else
        {
            Debug.LogError("No more levels to load!");
        }
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0;
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }
}
