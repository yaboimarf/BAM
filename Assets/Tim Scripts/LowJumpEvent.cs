using System.Collections;
using UnityEngine;

public class LowJumpEvent : GameEvent
{
    [Header("Player")]
    public Rigidbody playerRb;

    [Header("Low Jump Settings")]
    public float eventDuration = 6f;
    public float downwardForce = 25f;

    private bool lowJumpActive = false;

    public override IEnumerator PlayEvent()
    {
        Debug.Log("Low Jump Event gestart!");

        lowJumpActive = true;

        float timer = 0f;

        while (timer < eventDuration)
        {
            timer += Time.deltaTime;

            CheckLowJump();

            yield return null;
        }

        lowJumpActive = false;

        Debug.Log("Low Jump Event voorbij!");
    }

    void CheckLowJump()
    {
        if (!lowJumpActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            // duwt speler omlaag na springen
            playerRb.AddForce(Vector3.down * downwardForce, ForceMode.Impulse);
        }
    }
}