using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public GameObject sfxOnButton;    // Referensi ke tombol On SFX
    public GameObject sfxOffButton;   // Referensi ke tombol Off SFX
    public GameObject objectSFX;      // Referensi ke GameObject yang memutar SFX

    private AudioSource audioSource;
    private const string SFXPrefKey = "SFXMuted"; // Key untuk PlayerPrefs

    // Fungsi ini dipanggil saat game dimulai
    void Start()
    {
        // Cari GameObject dengan tag "GameSFX" untuk mendapatkan AudioSource
        objectSFX = GameObject.FindWithTag("GameSFX");

        // Pastikan GameObject ditemukan
        if (objectSFX == null)
        {
            Debug.LogError("GameObject dengan tag 'GameSFX' tidak ditemukan.");
            return;
        }

        // Dapatkan komponen AudioSource dari GameObject
        audioSource = objectSFX.GetComponent<AudioSource>();

        if (audioSource == null)
        {
            Debug.LogError("AudioSource tidak ditemukan pada GameObject dengan tag 'GameSFX'.");
            return;
        }

        // Muat status mute dari PlayerPrefs
        bool isMuted = PlayerPrefs.GetInt(SFXPrefKey, 0) == 1; // Default 0 (tidak mute)

        // Set status tombol berdasarkan PlayerPrefs
        if (isMuted)
        {
            sfxOnButton.SetActive(false);
            sfxOffButton.SetActive(true);
        }
        else
        {
            sfxOnButton.SetActive(true);
            sfxOffButton.SetActive(false);
        }

        // Perbarui status SFX berdasarkan tombol yang aktif
        UpdateSFXStatus();
    }

    // Fungsi untuk mengubah status SFX berdasarkan tombol yang aktif
    public void UpdateSFXStatus()
    {
        // Jika tombol On aktif, maka SFX tidak mute
        if (sfxOnButton.activeSelf)
        {
            audioSource.mute = false;
            PlayerPrefs.SetInt(SFXPrefKey, 0); // Simpan status tidak mute
            Debug.Log("SFX dimainkan.");
        }
        // Jika tombol Off aktif, maka SFX mute
        else if (sfxOffButton.activeSelf)
        {
            audioSource.mute = true;
            PlayerPrefs.SetInt(SFXPrefKey, 1); // Simpan status mute
            Debug.Log("SFX dimatikan.");
        }

        PlayerPrefs.Save(); // Simpan perubahan ke disk
    }

    // Fungsi yang dipanggil saat tombol On SFX ditekan
    public void ActivateOnSFX()
    {
        // Aktifkan tombol On dan nonaktifkan tombol Off
        sfxOnButton.SetActive(true);
        sfxOffButton.SetActive(false);

        // Perbarui status SFX
        UpdateSFXStatus();
    }

    // Fungsi yang dipanggil saat tombol Off SFX ditekan
    public void ActivateOffSFX()
    {
        // Aktifkan tombol Off dan nonaktifkan tombol On
        sfxOnButton.SetActive(false);
        sfxOffButton.SetActive(true);

        // Perbarui status SFX
        UpdateSFXStatus();
    }

    // Fungsi ini dipanggil saat tombol dengan tag "GameButton" diklik
    public void OnGameButtonClick()
    {
        // Cari GameObject dengan tag "GameSFX" yang memiliki AudioSource
        GameObject objectSFX = GameObject.FindWithTag("GameSFX");

        // Pastikan GameObject ditemukan
        if (objectSFX != null)
        {
            // Dapatkan AudioSource dari GameObject
            AudioSource audioSource = objectSFX.GetComponent<AudioSource>();

            if (audioSource != null && !audioSource.mute) // Pastikan AudioSource ada dan tidak dimute
            {
                audioSource.Play(); // Mainkan suara
                Debug.Log("Suara dimainkan dari GameSFX.");
            }
            else
            {
                Debug.Log("SFX dimute atau AudioSource tidak ditemukan.");
            }
        }
        else
        {
            Debug.LogError("GameObject dengan tag 'GameSFX' tidak ditemukan.");
        }
    }
}
