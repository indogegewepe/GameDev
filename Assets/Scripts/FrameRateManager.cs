using UnityEngine;

public class FrameRateManager : MonoBehaviour
{
    // Frame rate yang diinginkan
    public int targetFrameRate = 60;

    void Start()
    {
        // Mengatur frame rate yang diinginkan
        SetTargetFrameRate(targetFrameRate);
    }

    // Fungsi untuk mengatur target frame rate
    public void SetTargetFrameRate(int frameRate)
    {
        // Set target frame rate sesuai input
        Application.targetFrameRate = frameRate;

        // Memberikan feedback di konsol (opsional)
        Debug.Log("Target Frame Rate set to: " + frameRate);
    }

    // Fungsi ini akan dipanggil ketika ingin menonaktifkan vsync untuk meningkatkan performa
    public void DisableVSync()
    {
        QualitySettings.vSyncCount = 0;
        Debug.Log("V-Sync disabled for higher performance");
    }

    // Fungsi ini akan dipanggil untuk mengaktifkan vsync kembali
    public void EnableVSync()
    {
        QualitySettings.vSyncCount = 1;
        Debug.Log("V-Sync enabled");
    }
}
