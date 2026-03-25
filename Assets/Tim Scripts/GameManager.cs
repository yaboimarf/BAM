using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("Canvassen")]
    public GameObject mainMenu;
    public GameObject loseScreen;
    public GameObject WinScreen;
    public GameObject timerCanvas;
    public GameObject eventAnnouncement;

    [Header("Player")]
    public GameObject player;
    public Rigidbody playerRb;
    PlayerCheckpoint checkpoint;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        checkpoint = player.GetComponent<PlayerCheckpoint>();

        // Begin toestand
        mainMenu.SetActive(true);
        loseScreen.SetActive(false);
        WinScreen.SetActive(false);
        timerCanvas.SetActive(false);
        eventAnnouncement.SetActive(false);

        playerRb.constraints = RigidbodyConstraints.FreezeAll;
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void StartGame()
    {
        mainMenu.SetActive(false);
        timerCanvas.SetActive(true);
        eventAnnouncement.SetActive(true);
        playerRb.constraints = RigidbodyConstraints.FreezeRotation;
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void PlayerDied()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlayDeath();

        loseScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowWinScreen()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlayWin();

        WinScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ==================== RESPAWN VANUIT WIN SCREEN ====================
    public void RespawnToMainMenu()
    {
        WinScreen.SetActive(false);     // WinScreen weg
        mainMenu.SetActive(true);       // MainMenu open

        Time.timeScale = 1f;

        // Muis weer locken zoals in het spel (zoals jij wilt)
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void RespawnPlayer()
    {
        loseScreen.SetActive(false);
        checkpoint.Respawn();
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game gesloten");
    }
}