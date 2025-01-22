using UnityEngine;
using UnityEngine.UI;

public class TimerControllerMove : MonoBehaviour
{
    public Image linearFG;            // Foreground bar sebagai komponen Image
    public Image fillImage;           // End Stars Image
    public float timerDuration;       // Total durasi timer dalam detik
    private float elapsedTime;        // Waktu yang telah berlalu
    private bool timerRunning = false; // Apakah timer sedang berjalan
    public GameObject endPanel;
    public GameObject gamePanel;
    private GameManager gameManager;

    private int wrongAttempts = 0;

    // Tambahkan tiga objek bintang
    public GameObject[] stars;        // Array untuk menyimpan objek bintang

    // Fungsi ini dipanggil saat game dimulai
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Pastikan Image diatur sebagai Filled dan Fill Method horizontal
        linearFG.type = Image.Type.Filled;
        linearFG.fillMethod = Image.FillMethod.Horizontal;

        // Mulai timer saat level dimulai
        StartTimer();

        if (endPanel != null)
            endPanel.SetActive(false);

        // Pastikan semua bintang ada di array
        if (stars.Length != 3)
            Debug.LogError("Pastikan ada 3 objek bintang yang diassign pada array stars.");
    }

    // Fungsi ini dipanggil setiap frame
    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime;
            float progress = elapsedTime / timerDuration;
            linearFG.fillAmount = Mathf.Clamp01(1 - progress);

            // Menghitung sisa waktu
            float remainingTime = timerDuration - elapsedTime;

            // Mengubah warna bintang berdasarkan waktu persentase
            if (remainingTime <= timerDuration * 0.0f && stars[2] != null)  // Kurang dari 20% waktu tersisa
            {
                stars[2].GetComponent<Image>().color = Color.black;
                fillImage.fillAmount = 0f;
            }
            else if (remainingTime <= timerDuration * 0.3f && stars[1] != null)  // Kurang dari 50% waktu tersisa
            {
                stars[1].GetComponent<Image>().color = Color.black;
                fillImage.fillAmount = 0.3f;
            }
            else if (remainingTime <= timerDuration * 0.6f && stars[0] != null)  // Kurang dari 70% waktu tersisa
            {
                stars[0].GetComponent<Image>().color = Color.black;
                fillImage.fillAmount = 0.7f;
            }
            else
            {
                fillImage.fillAmount = 1f;
            }

            // Mengakhiri timer saat durasi habis
            if (elapsedTime >= timerDuration)
            {
                timerRunning = false;
                Debug.Log("Timer finished.");
                gameManager.ShowEndPanel();
            }
        }
    }

    // Fungsi untuk memulai timer
    public void StartTimer()
    {
        elapsedTime = 0f;
        timerRunning = true;
        //Debug.Log("Timer dimulai.");
    }

    // Fungsi untuk menghentikan timer
    public void StopTimer()
    {
        timerRunning = false;
    }

    // Fungsi untuk mengatur durasi timer secara dinamis
    public void SetTimerDuration(float newDuration)
    {
        timerDuration = newDuration;
    }

    public void WrongPenalty()
    {
        wrongAttempts++;  // Menambahkan jumlah kesalahan

        // Menurunkan pengurangan waktu seiring bertambahnya kesalahan
        float penaltyFactor = Mathf.Pow(0.9f, wrongAttempts); // Misalnya, faktor penalti berkurang 10% setiap kesalahan

        // Terapkan penalti berdasarkan faktor yang dihitung
        timerDuration -= (5f * penaltyFactor);
        timerDuration = Mathf.Max(1f, timerDuration); // Pastikan timer tidak menjadi negatif atau terlalu kecil
        Debug.Log("Timer duration after penalty: " + timerDuration);
    }

    public Image GetFillImage()
    {
        return fillImage;
    }
    public float GetFillAmount()
    {
        if (fillImage != null)
        {
            return fillImage.fillAmount;
        }
        else
        {
            Debug.LogError("FillImage is not assigned.");
            return 0f;
        }
    }
}
