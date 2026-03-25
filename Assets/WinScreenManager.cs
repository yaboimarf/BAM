using UnityEngine;

public class WinScreenManager : MonoBehaviour
{
    [Header("Menu Canvassen")]
    [SerializeField] private GameObject mainMenuCanvas;     // Sleep hier je MainMenu Canvas in

    // ======================
    // WORDT GEROEPEN VANUIT DE RESPAWN KNOP
    // ======================
    public void RespawnToMainMenu()
    {
        // WinScreen canvas uitzetten
        gameObject.SetActive(false);

        // MainMenu canvas aanzetten
        if (mainMenuCanvas != null)
        {
            mainMenuCanvas.SetActive(true);
        }
        else
        {
            Debug.LogWarning("MainMenu Canvas is niet toegewezen in de Inspector!");
        }

        // Spel hervatten
        Time.timeScale = 1f;

        // Muis vrij en zichtbaar houden (geen lock zoals jij wilt)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ======================
    // OPTIONEEL: Quit knop
    // ======================
    public void QuitGame()
    {
        // Werkt in built game
        Application.Quit();

        // Stopt het spel in de Unity Editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}