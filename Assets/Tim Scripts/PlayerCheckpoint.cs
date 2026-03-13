using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    public Transform currentCheckpoint;

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
            transform.position = currentCheckpoint.position;
        }
    }
}
