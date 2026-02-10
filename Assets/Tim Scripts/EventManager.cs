using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    [Header("Tijd tussen events")]
    public float timeBetweenEvents = 10f;

    [Header("Events (druk op + om toe te voegen)")]
    public List<GameEvent> events = new List<GameEvent>();

    private bool eventRunning = false;

    void Start()
    {
        StartCoroutine(EventLoop());
    }

    IEnumerator EventLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenEvents);

            if (!eventRunning && events.Count > 0)
            {
                StartCoroutine(RunRandomEvent());
            }
        }
    }

    IEnumerator RunRandomEvent()
    {
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
