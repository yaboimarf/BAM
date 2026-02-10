using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlashbangEvent : GameEvent
{
    [Header("UI")]
    public Image flashImage;
    public TextMeshProUGUI flashText;

    [Header("Tekst")]
    public string flashWord = "FLASHBANG";
    public float letterDelay = 0.06f;
    public float beforeFlashDelay = 0.25f;

    [Header("Flash settings")]
    public float flashInSpeed = 0.05f;
    public float holdWhite = 0.6f;
    public float fadeOutSpeed = 1.5f;

    public override IEnumerator PlayEvent()
    {
        flashText.text = "";
        SetAlpha(0);

        // letter voor letter
        for (int i = 0; i < flashWord.Length; i++)
        {
            flashText.text += flashWord[i];
            yield return new WaitForSeconds(letterDelay);
        }

        yield return new WaitForSeconds(beforeFlashDelay);
        flashText.text = "";

        // snel naar wit
        float t = 0;
        while (t < 1)
        {
            t += Time.deltaTime / flashInSpeed;
            SetAlpha(Mathf.Lerp(0, 1, t));
            yield return null;
        }

        yield return new WaitForSeconds(holdWhite);

        // fade terug
        float alpha = 1;
        while (alpha > 0)
        {
            alpha -= Time.deltaTime * fadeOutSpeed;
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(0);
    }

    void SetAlpha(float a)
    {
        Color c = flashImage.color;
        c.a = Mathf.Clamp01(a);
        flashImage.color = c;
    }
}
