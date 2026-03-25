using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("UI Sounds")]
    public AudioSource sfxSource;
    public AudioClip hoverSound;
    public AudioClip clickSound;

    [Header("Gameplay Sounds")]
    public AudioClip deathSound;
    public AudioClip winSound;        // ← NIEUW

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayHover(float hoverVolume = 1f)
    {
        if (hoverSound != null)
            sfxSource.PlayOneShot(hoverSound, hoverVolume * AudioListener.volume);
    }

    public void PlayClick(float clickVolume = 1f)
    {
        if (clickSound != null)
            sfxSource.PlayOneShot(clickSound, clickVolume * AudioListener.volume);
    }

    public void PlayDeath(float volume = 1f)
    {
        if (deathSound != null)
            sfxSource.PlayOneShot(deathSound, volume * AudioListener.volume);
    }

    // ==================== NIEUWE FUNCTIE ====================
    public void PlayWin(float volume = 1f)
    {
        if (winSound != null)
            sfxSource.PlayOneShot(winSound, volume * AudioListener.volume);
    }
}