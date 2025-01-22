using UnityEngine;

public class MusicController : MonoBehaviour
{
    public GameObject musicOnButton;    // Referensi ke tombol On Music
    public GameObject musicOffButton;   // Referensi ke tombol Off Music
    public GameObject ObjectMusic;      // Referensi ke GameObject yang memutar musik

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

        // Muat status mute dari PlayerPrefs
        bool isMuted = PlayerPrefs.GetInt(MusicPrefKey, 0) == 1; // Default 0 (tidak mute)

        // Set status tombol berdasarkan PlayerPrefs
        if (isMuted)
        {
            musicOnButton.SetActive(false);
            musicOffButton.SetActive(true);
        }
        else
        {
            musicOnButton.SetActive(true);
            musicOffButton.SetActive(false);
        }

        // Perbarui status musik berdasarkan tombol yang aktif
        UpdateMusicStatus();
    }

    // Fungsi untuk mengubah status musik berdasarkan tombol yang aktif
    public void UpdateMusicStatus()
    {
        // Jika tombol On aktif, maka musik tidak mute
        if (musicOnButton.activeSelf)
        {
            audioSource.mute = false;
            PlayerPrefs.SetInt(MusicPrefKey, 0); // Simpan status tidak mute
            Debug.Log("Musik dimainkan.");
        }
        // Jika tombol Off aktif, maka musik mute
        else if (musicOffButton.activeSelf)
        {
            audioSource.mute = true;
            PlayerPrefs.SetInt(MusicPrefKey, 1); // Simpan status mute
            Debug.Log("Musik dimatikan.");
        }

        PlayerPrefs.Save(); // Simpan perubahan ke disk
    }

    // Fungsi yang dipanggil saat tombol On Music ditekan
    public void ActivateOnMusic()
    {
        // Aktifkan tombol On dan nonaktifkan tombol Off
        musicOnButton.SetActive(true);
        musicOffButton.SetActive(false);

        // Perbarui status musik
        UpdateMusicStatus();
    }

    // Fungsi yang dipanggil saat tombol Off Music ditekan
    public void ActivateOffMusic()
    {
        // Aktifkan tombol Off dan nonaktifkan tombol On
        musicOnButton.SetActive(false);
        musicOffButton.SetActive(true);

        // Perbarui status musik
        UpdateMusicStatus();
    }
}
