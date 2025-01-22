using UnityEngine;

public class MuteEnableSFX : MonoBehaviour
{
    public AudioSource audioSource; // AudioSource yang digunakan untuk memutar suara
    public string SFXPrefKey = "SFXMuted"; // Key untuk menyimpan status mute di PlayerPrefs

    void Awake()
    {
        // Pastikan audioSource sudah ter-assign
        if (audioSource == null)
        {
            Debug.LogError("AudioSource tidak ditemukan pada GameObject.");
            return;
        }
        UpdateSFXStatus();

        // Tidak ada pemanggilan langsung UpdateSFXStatus di sini
        // Status mute akan diperbarui hanya setelah tombol ditekan
    }

    // Fungsi untuk memuat dan mengupdate status mute
    private void UpdateSFXStatus()
    {
        int sfxStatus = PlayerPrefs.GetInt(SFXPrefKey, 0); // Default ke 0 (tidak mute)

        if (sfxStatus == 1)
        {
            MuteSFX(); // Mute audio jika statusnya 1
        }
        else
        {
            UnmuteSFX(); // Unmute audio jika statusnya 0
        }

        Debug.Log(audioSource.mute ? "SFX dalam keadaan Mute." : "SFX dalam keadaan Dinyalakan.");
    }

    // Fungsi untuk mute SFX
    public void MuteSFX()
    {
        audioSource.mute = true;

        // Simpan status mute
        PlayerPrefs.SetInt(SFXPrefKey, 1);
        PlayerPrefs.Save(); // Simpan perubahan ke disk

        Debug.Log("SFX Dimatikan.");
    }

    // Fungsi untuk unmute SFX
    public void UnmuteSFX()
    {
        audioSource.mute = false;

        // Simpan status unmute
        PlayerPrefs.SetInt(SFXPrefKey, 0);
        PlayerPrefs.Save(); // Simpan perubahan ke disk

        Debug.Log("SFX Dinyalakan.");
    }

    // Fungsi untuk memutar SFX
    public void PlaySFX()
    {
        // Cek jika audio tidak dalam keadaan mute, baru putar audio
        if (!audioSource.mute)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); // Putar audio hanya jika tidak mute
                Debug.Log("SFX Dimainkan.");
            }
        }
        else
        {
            Debug.Log("SFX tidak dimainkan karena mute.");
        }
    }

    // Fungsi yang dipanggil saat tombol mute/unmute ditekan
    public void OnMuteUnmuteButtonPressed()
    {
        // Panggil UpdateSFXStatus untuk memperbarui status berdasarkan PlayerPrefs
        UpdateSFXStatus();
    }
}
