using UnityEngine;
using UnityEngine.UI;  // Untuk mengakses komponen Image

public class FillOnEnable : MonoBehaviour
{
    public Image fillImage;  // Drag Image yang digunakan untuk pengisian fill amount
    public float fillSpeed = 0.5f;  // Kecepatan pengisian (dalam detik)

    private bool isFilling = false;  // Menandakan apakah pengisian sedang berjalan
    private float targetFillAmount = 0f;  // Nilai target fillAmount (0 berarti tidak terisi)
    private bool hasFilled = false;  // Menandakan apakah pengisian sudah selesai

    private GameManager gameManager;  // Reference to GameManager script

    void Start()
    {
        // Get the GameManager from the same GameObject
        gameManager = FindObjectOfType<GameManager>();  // Get GameManager instance
        if (gameManager == null)
        {
            Debug.LogError("GameManager not found!");
        }

        // Pastikan fillAmount mulai dari 0 saat pertama kali
        if (fillImage != null)
        {
            fillImage.fillAmount = 0f;
        }
    }

    void Update()
    {
        // Pastikan pengisian hanya dimulai jika currentLevelScore berubah dan pengisian belum dimulai
        if (gameManager != null && !isFilling && !hasFilled)
        {
            StartFillingProcess(gameManager.currentLevelScore);
        }
    }

    void StartFillingProcess(int score)
    {
        // Tentukan target fillAmount berdasarkan currentLevelScore
        switch (score)
        {
            case 0:
                targetFillAmount = 0f;
                break;
            case 1:
                targetFillAmount = 0.3f;
                break;
            case 2:
                targetFillAmount = 0.7f;
                break;
            case 3:
                targetFillAmount = 1f;
                break;
            default:
                targetFillAmount = 0f;  // Default jika skor tidak valid
                break;
        }

        // Jika pengisian belum pernah dilakukan, mulai pengisian
        if (fillImage != null && !hasFilled)
        {
            fillImage.fillAmount = 0f;  // Reset fillAmount ke 0 sebelum mulai pengisian
            isFilling = true;  // Tandai pengisian sedang berlangsung
            StartCoroutine(FillAmountCoroutine());
        }
    }

    private System.Collections.IEnumerator FillAmountCoroutine()
    {
        float currentFill = fillImage.fillAmount;

        // Isi hingga mencapai targetFillAmount
        while (currentFill < targetFillAmount)
        {
            currentFill += Time.deltaTime / fillSpeed;  // Sesuaikan fillAmount dengan waktu
            fillImage.fillAmount = Mathf.Clamp01(currentFill);  // Pastikan tidak melebihi 1
            yield return null;
        }

        // Setelah pengisian selesai, pastikan mencapai target
        fillImage.fillAmount = targetFillAmount;
        isFilling = false;  // Tandai pengisian selesai
        hasFilled = true;  // Tandai bahwa pengisian sudah selesai
    }
}
