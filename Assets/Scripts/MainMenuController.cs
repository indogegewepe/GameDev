using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    public GameObject ObjectMusic;  // Referensi ke GameObject yang memutar musik
    private AudioSource audioSource;
    private const string MusicPrefKey = "MusicMuted"; // Key untuk PlayerPrefs

    // Fungsi ini dipanggil saat game dimulai
    void Start()
    {
        // Cari GameObject dengan tag "GameMusic"
        ObjectMusic = GameObject.FindWithTag("GameMusic");

        // Pastikan GameObject ditemukan
        if (ObjectMusic == null)
        {
            Debug.LogError("GameObject dengan tag 'GameMusic' tidak ditemukan.");
            return;
        }

        // Dapatkan komponen AudioSource dari GameObject
        audioSource = ObjectMusic.GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource tidak ditemukan.");
            return;
        }

        // Cek status mute dari PlayerPrefs
        bool isMuted = PlayerPrefs.GetInt(MusicPrefKey, 0) == 1; // Default 0 (tidak mute)

        // Terapkan status mute berdasarkan PlayerPrefs
        audioSource.mute = isMuted;

        if (isMuted)
        {
            Debug.Log("Musik dimulai dalam keadaan mute.");
        }
        else
        {
            Debug.Log("Musik dimulai dalam keadaan tidak mute.");
        }
    }
}
