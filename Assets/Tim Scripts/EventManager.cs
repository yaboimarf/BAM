using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EventManager : MonoBehaviour
{
    [Header("Tijd tussen events")]
    public float timeBetweenEvents = 10f;

    [Header("Max timer limiet")]
    public float maxTimerTime = 25f; // hoger dan dit kan timer nooit worden

    [Header("Timer UI")]
    public TextMeshProUGUI timerText;

    [Header("Events")]
    public List<GameEvent> events = new List<GameEvent>();

    [Header("Error instellingen")]
    [Range(0f, 1f)]
    public float errorChancePerSecond = 0.05f;
    public float minTimeBeforeError = 2f;

    private bool eventRunning = false;
    private float timer;
    private bool errorActive = false;
    private float timeSinceReset = 0f;

    void Start()
    {
        timer = timeBetweenEvents;
    }

    void Update()
    {
        if (eventRunning) return;

        timer -= Time.deltaTime;
        timeSinceReset += Time.deltaTime;

        // random error check
        if (!errorActive && timeSinceReset > minTimeBeforeError)
        {
            if (Random.value < errorChancePerSecond * Time.deltaTime)
            {
                StartCoroutine(ErrorRoutine());
            }
        }

        if (timer <= 0f)
        {
            StartCoroutine(RunRandomEvent());
            ResetTimer();
            return;
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

        // laat error tekst zien
        yield return new WaitForSeconds(2f);

        int extraTime = Random.value < 0.5f ? 5 : 10;

        // voeg tijd toe maar met MAX LIMIET
        timer += extraTime;
        timer = Mathf.Min(timer, maxTimerTime);

        errorActive = false;
    }

    IEnumerator RunRandomEvent()
    {
        if (events.Count == 0) yield break;

        eventRunning = true;

        GameEvent chosenEvent = GetRandomEvent();

        if (chosenEvent != null)
        {
            Debug.Log("Event gestart: " + chosenEvent.eventName);
            yield return StartCoroutine(chosenEvent.PlayEvent());
        }

        eventRunning = false;
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
