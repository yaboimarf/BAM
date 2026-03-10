using System.Collections;
using UnityEngine;

public class InvertControlsEvent : GameEvent
{
    [Header("Player reference")]
    public Rigidbody playerRb;

    [Header("Settings")]
    public float invertDuration = 5f;
    public float invertForce = 30f;

    private bool active = false;

    public override IEnumerator PlayEvent()
    {
        active = true;

        Debug.Log("Inverted Controls gestart!");

        float timer = 0f;

        while (timer < invertDuration)
        {
            timer += Time.deltaTime;

            ApplyInvertedMovement();

            yield return null;
        }

        active = false;

        Debug.Log("Inverted Controls voorbij");
    }

    void ApplyInvertedMovement()
    {
        if (!active) return;

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 invertedMove = new Vector3(-horizontal, 0, -vertical);

        playerRb.AddRelativeForce(invertedMove * invertForce * Time.deltaTime, ForceMode.Impulse);
    }
}