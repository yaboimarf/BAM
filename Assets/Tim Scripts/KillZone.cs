using UnityEngine;

public class KillZone : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Als player geraakt wordt
        if (other.CompareTag("Player"))
        {
            GameManager.instance.PlayerDied();
        }

        // Rocket verwijderen (altijd bij hit)
        Destroy(gameObject);
    }
}