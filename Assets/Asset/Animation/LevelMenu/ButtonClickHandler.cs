using UnityEngine;
using UnityEngine.UI;
using System.Collections; // Pastikan ini ada!

public class ButtonClickHandler : MonoBehaviour
{
    public Button yourButton; // Button yang akan di klik
    public RectTransform targetRectTransform; // RectTransform yang akan diubah posisinya
    public Vector2 newPosition; // Posisi baru yang diinginkan
    public float moveDuration = 1f; // Durasi pergerakan dalam detik

    void Start()
    {
        // Pastikan Button dan RectTransform sudah di-assign
        if (yourButton != null && targetRectTransform != null)
        {
            // Tambahkan listener untuk button
            yourButton.onClick.AddListener(OnButtonClick);
        }
    }

    // Fungsi yang akan dipanggil ketika button diklik
    void OnButtonClick()
    {
        // Mulai coroutine untuk pergerakan smooth
        StartCoroutine(MoveToPositionSmoothly(targetRectTransform, newPosition, moveDuration));
    }

    // Coroutine untuk menggerakkan RectTransform secara smooth
    IEnumerator MoveToPositionSmoothly(RectTransform rectTransform, Vector2 targetPosition, float duration)
    {
        Vector2 startPosition = rectTransform.anchoredPosition;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            // Hitung posisi yang interpolasi secara smooth
            rectTransform.anchoredPosition = Vector2.Lerp(startPosition, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime; // Tambahkan waktu yang sudah berlalu
            yield return null; // Tunggu frame berikutnya
        }

        // Pastikan posisi akhir tercapai
        rectTransform.anchoredPosition = targetPosition;
    }
}
