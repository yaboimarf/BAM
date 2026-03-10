using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Game");  // Vervang met de naam van je game scene
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene("MainMenu");  
    }
        
    
    public void Settings()
    {
        SceneManager.LoadScene("SettingsScene");  // naam van je settings scene
    }

    public void Exit()
    {
        Debug.Log("Game Closed");
        Application.Quit();  // Werkt alleen in build, niet in Unity Editor
    }
}