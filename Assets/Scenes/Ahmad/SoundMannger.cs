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
    public AudioClip winSound;
    public AudioClip eventSound;   // ÉÉN geluid voor alle events

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayDeath(float volume = 0.5f)
    {
        if (deathSound != null)
            sfxSource.PlayOneShot(deathSound, volume * AudioListener.volume);
    }

    public void PlayWin(float volume = 0.5f)
    {
        if (winSound != null)
            sfxSource.PlayOneShot(winSound, volume * AudioListener.volume);
    }

    public void PlayEvent(float volume = 0.5f)
    {
        if (eventSound != null)
            sfxSource.PlayOneShot(eventSound, volume * AudioListener.volume);
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
}
