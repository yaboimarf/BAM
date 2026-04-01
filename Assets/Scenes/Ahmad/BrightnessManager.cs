using UnityEngine;
using UnityEngine.UI;

public class BrightnessManager : MonoBehaviour
{
    public Slider brightnessSlider;

    public Image menuOverlay;   // menu panel
    public Image gameOverlay;   // main game panel

    public float menuMaxAlpha = 200f / 255f;
    public float gameMaxAlpha = 200f / 255f;

    void Start()
    {
        if (PlayerPrefs.HasKey("brightness"))
            brightnessSlider.value = PlayerPrefs.GetFloat("brightness");

        UpdateBrightness(brightnessSlider.value);

        brightnessSlider.onValueChanged.AddListener(UpdateBrightness);
    }

    void UpdateBrightness(float value)
    {
        // MENU
        if (menuOverlay != null)
        {
            float menuAlpha = value * menuMaxAlpha;
            Color c = menuOverlay.color;
            c.a = menuAlpha;
            menuOverlay.color = c;
        }

        // GAME
        if (gameOverlay != null)
        {
            float gameAlpha = value * gameMaxAlpha;
            Color c2 = gameOverlay.color;
            c2.a = gameAlpha;
            gameOverlay.color = c2;
        }

        PlayerPrefs.SetFloat("brightness", value);
    }
}