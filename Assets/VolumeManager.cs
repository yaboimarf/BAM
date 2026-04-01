using UnityEngine;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public Slider volumeSlider;

    void Start()
    {
        if (PlayerPrefs.HasKey("volume"))
            volumeSlider.value = PlayerPrefs.GetFloat("volume");
        else
            volumeSlider.value = 0.5f;

        UpdateVolume(volumeSlider.value);

        volumeSlider.onValueChanged.AddListener(UpdateVolume);
    }

    void UpdateVolume(float value)
    {
        AudioListener.volume = value; // ALLES syncen
        PlayerPrefs.SetFloat("volume", value);
    }
}