using UnityEngine;
using UnityEngine.EventSystems;

public class UI_ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("Per-button volume")]
    [Range(0f, 1f)] public float hoverVolume = 1f;
    [Range(0f, 1f)] public float clickVolume = 1f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        SoundManager.instance.PlayHover(hoverVolume);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        SoundManager.instance.PlayClick(clickVolume);
    }
}
