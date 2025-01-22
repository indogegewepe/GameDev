using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SFXButtonController : MonoBehaviour
{
    public GameObject objectSFX;        // Referensi ke GameObject yang memutar SFX
    private AudioSource audioSource;

    // Fungsi ini dipanggil saat game dimulai
    void Start()
    {
        // Cari GameObject yang memiliki komponen AudioSource
        objectSFX = GameObject.FindWithTag("GameSFX");

        // Pastikan GameObject ditemukan
        if (objectSFX == null)
        {
            Debug.LogError("GameObject dengan tag 'GameSFX' tidak ditemukan.");
            return;
        }

        // Dapatkan komponen AudioSource dari GameObject
        audioSource = objectSFX.GetComponent<AudioSource>();

        //if (audioSource == null)
        //{
        //    Debug.LogError("AudioSource tidak ditemukan.");
        //    return;
        //}

        // Cari semua tombol dengan tag "GameSFX"
        Button[] buttons = GameObject.FindGameObjectsWithTag("GameSFX")
            .Select(go => go.GetComponent<Button>())
            .Where(button => button != null)
            .ToArray();

        // Untuk setiap tombol, tambahkan event listener
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlaySFX());
        }
    }

    // Fungsi untuk memutar SFX saat tombol ditekan
    void PlaySFX()
    {
        if (audioSource != null)
        {
            audioSource.Play(); // Memutar suara SFX
            Debug.Log("SFX dimainkan.");
        }
    }
}
