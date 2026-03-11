using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventManager : MonoBehaviour
{
    [Header("Tijd tussen events")]
    public float timeBetweenEvents = 10f;

    [Header("Max timer limiet")]
    public float maxTimerTime = 25f;

    [Header("Timer UI")]
    public TextMeshProUGUI timerText;

    [Header("Events")]
    public List<GameEvent> events = new List<GameEvent>();

    [Header("Error instellingen")]
    [Range(0f, 1f)]
    public float errorChancePerSecond = 0.05f;

    public float minTimeBeforeError = 2f;

    private float timer;
    private bool errorActive = false;
    private float timeSinceReset = 0f;

    void Start()
    {
        timer = timeBetweenEvents;
    }

    void Update()
    {
        // timer loopt ALTIJD
        timer -= Time.deltaTime;
        timeSinceReset += Time.deltaTime;

        // random error
        if (!errorActive && timeSinceReset > minTimeBeforeError)
        {
            if (Random.value < errorChancePerSecond * Time.deltaTime)
            {
                StartCoroutine(ErrorRoutine());
            }
        }

        if (timer <= 0f)
        {
            StartRandomEvent();
            ResetTimer();
        }

        UpdateTimerUI();
    }

    void ResetTimer()
    {
        timer = timeBetweenEvents;
        timeSinceReset = 0f;
    }

    void UpdateTimerUI()
    {
        if (timerText == null) return;

        if (errorActive)
            timerText.text = "ERROR...";
        else
            timerText.text = "Event in: " + Mathf.Ceil(timer).ToString();
    }

    IEnumerator ErrorRoutine()
    {
        errorActive = true;

        yield return new WaitForSeconds(2f);

        int extraTime = Random.value < 0.5f ? 5 : 10;

        timer += extraTime;
        timer = Mathf.Min(timer, maxTimerTime);

        errorActive = false;
    }

    void StartRandomEvent()
    {
        if (events.Count == 0) return;

        GameEvent chosenEvent = GetRandomEvent();

        if (chosenEvent != null)
        {
            Debug.Log("Event gestart: " + chosenEvent.eventName);
            EventAnnouncement.instance.AnnounceEvent(chosenEvent.eventName);
            StartCoroutine(chosenEvent.PlayEvent());
        }
    }

    GameEvent GetRandomEvent()
    {
        float totalWeight = 0f;

        foreach (GameEvent e in events)
            totalWeight += e.eventWeight;

        float randomValue = Random.Range(0, totalWeight);
        float current = 0f;

        foreach (GameEvent e in events)
        {
            current += e.eventWeight;

            if (randomValue <= current)
                return e;
        }

        return null;
    }
}