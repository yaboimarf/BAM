using System.Collections;
using UnityEngine;

public class JumpBoostEvent : GameEvent
{
    [Header("Player")]
    public Rigidbody playerRb;

    [Header("Jump Boost Settings")]
    public float boostDuration = 6f;
    public float extraJumpForce = 20f;

    private bool boostActive = false;

    public override IEnumerator PlayEvent()
    {
        Debug.Log("Jump Boost Event gestart!");

        boostActive = true;

        float timer = 0f;

        while (timer < boostDuration)
        {
            timer += Time.deltaTime;

            CheckJumpBoost();

            yield return null;
        }

        boostActive = false;

        Debug.Log("Jump Boost Event voorbij!");
    }

    void CheckJumpBoost()
    {
        if (!boostActive) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(Vector3.up * extraJumpForce, ForceMode.Impulse);
        }
    }
}