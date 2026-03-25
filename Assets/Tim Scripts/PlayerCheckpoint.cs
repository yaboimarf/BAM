using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public Transform currentCheckpoint;

    [Header("Respawn Settings")]
    public float respawnOffsetY = 1.5f;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            currentCheckpoint = other.transform;
            Debug.Log("Checkpoint bereikt!");
        }
    }

    public void Respawn()
    {
        if (currentCheckpoint != null)
        {
            Vector3 respawnPos = currentCheckpoint.position + Vector3.up * respawnOffsetY;
            transform.position = respawnPos;
        }
    }
}