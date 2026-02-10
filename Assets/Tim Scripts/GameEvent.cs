using System.Collections;
using UnityEngine;

public abstract class GameEvent : MonoBehaviour
{
    [Header("Event info")]
    public string eventName = "New Event";
    public float eventWeight = 1f; // hoe vaak dit event voorkomt

    // elk event moet dit hebben
    public abstract IEnumerator PlayEvent();
}
