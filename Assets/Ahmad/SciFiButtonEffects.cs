using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SciFiButtonEffects : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image img;
    private Vector3 startScale;
    private Vector3 startPos;

    [Header("Glow Colors")]
    public Color normalColor = Color.white;
    public Color hoverColor = new Color(1f, 0.3f, 0.3f);

    [Header("Scale")]
    public float hoverScale = 1.1f;
    public float scaleSpeed = 10f;

    [Header("Slide")]
    public float slideDistance = 15f;
    public float slideSpeed = 8f;

    private bool hovering = false;

    void Start()
    {
        img = GetComponent<Image>();
        startScale = transform.localScale;
        startPos = transform.localPosition;

        img.color = normalColor;
    }

    void Update()
    {
        Vector3 targetScale = startScale;
        Vector3 targetPos = startPos;

        if (hovering)
        {
            float pulse = 1f + Mathf.Sin(Time.unscaledTime * 5f) * 0.05f;
            targetScale = startScale * hoverScale * pulse;
            targetPos = startPos + new Vector3(slideDistance, 0, 0);
        }

        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.unscaledDeltaTime * scaleSpeed);
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPos, Time.unscaledDeltaTime * slideSpeed);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        img.color = hoverColor;

        // Geluid gaat nu via SoundManager
        SoundManager.instance.PlayHover();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        img.color = normalColor;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Geluid gaat nu via SoundManager
        SoundManager.instance.PlayClick();
    }
}
