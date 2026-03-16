using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SliderController : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler
{
    public enum SliderType { Volume, Brightness }
    public SliderType sliderType;

 
    public AudioClip hoverSound;
    public AudioClip clickSound;
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverSound != null)
            audioSource.PlayOneShot(hoverSound);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (clickSound != null)
            audioSource.PlayOneShot(clickSound);
    }

    void OnSliderValueChanged(float value)
    {
        PlayerPrefs.SetFloat(sliderType.ToString(), value);

        if (sliderType == SliderType.Volume)
        {
            AudioListener.volume = value;
        }
        else if (sliderType == SliderType.Brightness)
        {
            // Pas brightness aan via post-processing of custom shader
            // Bijvoorbeeld: RenderSettings.ambientIntensity = value;
        }
    }
}
