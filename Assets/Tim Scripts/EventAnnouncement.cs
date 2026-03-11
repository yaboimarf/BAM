using System.Collections;
using UnityEngine;
using TMPro;

public class EventAnnouncement : MonoBehaviour
{
    public static EventAnnouncement instance;

    public TextMeshProUGUI eventText;

    [Header("Text Settings")]
    public float letterDelay = 0.05f;
    public float showTime = 1.5f;

    void Awake()
    {
        instance = this;
    }

    public void AnnounceEvent(string eventName)
    {
        StopAllCoroutines();
        StartCoroutine(ShowText(eventName));
    }

    IEnumerator ShowText(string message)
    {
        eventText.text = "";

        foreach (char letter in message)
        {
            eventText.text += letter;
            yield return new WaitForSeconds(letterDelay);
        }

        yield return new WaitForSeconds(showTime);

        eventText.text = "";
    }
}