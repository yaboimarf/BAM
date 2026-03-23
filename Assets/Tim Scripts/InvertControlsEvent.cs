using System.Collections;
using UnityEngine;

public class InvertControlsEvent : GameEvent
{
    private PlayerMovement2 playerMovement;

    [Header("Settings")]
    public float invertDuration = 5f;

    public override IEnumerator PlayEvent()
    {
        // player vinden via tag
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement2>();
        }

        if (playerMovement == null)
        {
            Debug.LogError("PlayerMovement2 niet gevonden!");
            yield break;
        }

        Debug.Log("Inverted Controls gestart!");

        playerMovement.invertMultiplier = -1f;

        yield return new WaitForSeconds(invertDuration);

        playerMovement.invertMultiplier = 1f;

        Debug.Log("Inverted Controls voorbij");
    }
}