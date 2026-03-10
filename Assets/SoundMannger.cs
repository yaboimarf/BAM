using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Sources")]
    public AudioSource musicSource;
    public AudioSource sfxSource;

    [Header("UI Sounds")]
    public AudioClip hoverSound;
    public AudioClip clickSound;

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

    // Achtergrondmuziek volume
    public void SetMusicVolume(float value)
    {
        musicSource.volume = value;
    }

    // SFX volume (hover, click, etc.)
    public void SetSFXVolume(float value)
    {
        sfxSource.volume = value;
    }

    // UI hover geluid
    public void PlayHover()
    {
        if (hoverSound != null)
            sfxSource.PlayOneShot(hoverSound);
    }

    // UI click geluid
    public void PlayClick()
    {
        if (clickSound != null)
            sfxSource.PlayOneShot(clickSound);
    }
}
