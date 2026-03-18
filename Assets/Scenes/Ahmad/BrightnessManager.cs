using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public Slider brightnessSlider;
    public Image brightnessOverlay;

    void Start()
    {
        if (PlayerPrefs.HasKey("brightness"))
            brightnessSlider.value = PlayerPrefs.GetFloat("brightness");

        UpdateBrightness(brightnessSlider.value);

        brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
    }

    void UpdateBrightness(float value)
    {
        float maxAlpha = 230f / 255f;   // jouw grens
        float alpha = value * maxAlpha;

        Color c = brightnessOverlay.color;
        c.a = alpha;
        brightnessOverlay.color = c;

        PlayerPrefs.SetFloat("brightness", value);
    }
}
