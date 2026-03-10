using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class BAMTitleHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private TextMeshProUGUI tmp;
    private Material materialInstance;
    private Vector3 originalScale;

    [Header("Colors")]
    public Color normalColor = Color.white;
    public Color hoverColor = new Color(1f, 0.2f, 0.2f);

    [Header("Glow")]
    public float normalGlow = 0f;
    public float hoverGlow = 1f;
    public Color glowColor = new Color(1f, 0.2f, 0.2f);

    [Header("Pulse")]
    public float pulseSpeed = 4f;
    public float pulseAmount = 0.08f;

    [Header("Scale")]
    public float hoverScale = 1.1f;
    public float scaleSpeed = 8f;

    private bool isHovering = false;

    void Start()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        originalScale = transform.localScale;

        // Maak een eigen material instance zodat alleen deze tekst verandert
        materialInstance = new Material(tmp.fontSharedMaterial);
        tmp.fontMaterial = materialInstance;

        tmp.color = normalColor;
        SetGlow(normalGlow);
    }

    void Update()
    {
        Vector3 targetScale = originalScale;

        if (isHovering)
        {
            float pulse = 1f + Mathf.Sin(Time.unscaledTime * pulseSpeed) * pulseAmount;
            targetScale = originalScale * hoverScale * pulse;
        }

        transform.localScale = Vector3.Lerp(
            transform.localScale,
            targetScale,
            Time.unscaledDeltaTime * scaleSpeed
        );
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        tmp.color = hoverColor;
        SetGlow(hoverGlow);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        tmp.color = normalColor;
        SetGlow(normalGlow);
    }

    private void SetGlow(float glowPower)
    {
        if (materialInstance.HasProperty(ShaderUtilities.ID_GlowColor))
            materialInstance.SetColor(ShaderUtilities.ID_GlowColor, glowColor);

        if (materialInstance.HasProperty(ShaderUtilities.ID_GlowPower))
            materialInstance.SetFloat(ShaderUtilities.ID_GlowPower, glowPower);

        if (materialInstance.HasProperty(ShaderUtilities.ID_FaceColor))
            materialInstance.SetColor(ShaderUtilities.ID_FaceColor, tmp.color);
    }
}