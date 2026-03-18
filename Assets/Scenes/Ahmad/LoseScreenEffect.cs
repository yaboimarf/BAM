using System.Collections;
using UnityEngine;
using TMPro;

public class LoseScreenEffect : MonoBehaviour
{
    [Header("Objects")]
    public GameObject LoseScreen;          // HELE scherm
    public TextMeshProUGUI youDiedText;    // tekst
    public RectTransform panel;            // panel voor shake

    [Header("Settings")]
    public float fadeDuration = 1f;
    public float shakeDuration = 0.4f;
    public float shakeStrength = 10f;

    private Vector2 originalPos;

    void Start()
    {
        // Zorg dat scherm uit staat bij start
        LoseScreen.SetActive(false);
    }

    public void ShowLoseScreen()
    {
        // Zet scherm aan
        LoseScreen.SetActive(true);

        // Reset text
        Color c = youDiedText.color;
        c.a = 0f;
        youDiedText.color = c;
        youDiedText.rectTransform.localScale = Vector3.one * 1.2f;

        // Save positie panel
        originalPos = panel.anchoredPosition;

        StopAllCoroutines();
        StartCoroutine(PlayEffect());
    }

    IEnumerator PlayEffect()
    {
        StartCoroutine(Shake());

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float p = t / fadeDuration;

            // Fade in
            Color c = youDiedText.color;
            c.a = Mathf.Lerp(0f, 1f, p);
            youDiedText.color = c;

            // Scale effect
            float scale = Mathf.Lerp(1.2f, 1f, p);
            youDiedText.rectTransform.localScale = Vector3.one * scale;

            yield return null;
        }
    }

    IEnumerator Shake()
    {
        float t = 0f;

        while (t < shakeDuration)
        {
            t += Time.deltaTime;

            float x = Random.Range(-shakeStrength, shakeStrength);
            float y = Random.Range(-shakeStrength, shakeStrength);

            panel.anchoredPosition = originalPos + new Vector2(x, y);

            yield return null;
        }

        panel.anchoredPosition = originalPos;
    }
}