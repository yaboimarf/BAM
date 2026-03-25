using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    [SerializeField] private GameObject winScreenCanvas;   // Sleep je WinScreen hierin in de Inspector

    private void OnTriggerEnter(Collider other)
    {
        // Check of het de speler is die het object raakt
        if (other.CompareTag("Player"))   // Zorg dat je speler de tag "Player" heeft!
        {
            winScreenCanvas.SetActive(true);

            // Optioneel: stop de speler met bewegen of pauseer het spel
            // Time.timeScale = 0f;   // als je het spel wilt pauzeren
            // of: andere scripts uitschakelen...
        }
    }
}