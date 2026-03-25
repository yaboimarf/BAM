using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject mainMenu;
    public GameObject loseScreen;
    public GameObject WinScreen;          // al aanwezig
    public GameObject timerCanvas;
    public GameObject eventAnnouncement;
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

        mainMenu.SetActive(true);
        loseScreen.SetActive(false);
        WinScreen.SetActive(false);        // blijft hetzelfde
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
        loseScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ==================== NIEUWE FUNCTIES VOOR WIN ====================

    public void ShowWinScreen()
    {
        WinScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RespawnToMainMenu()     // Dit wordt aangeroepen door de Respawn knop
    {
        WinScreen.SetActive(false);     // WinScreen uit
        mainMenu.SetActive(true);       // MainMenu weer aan

        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // ==================== BESTAANDE FUNCTIES ====================

    public void RespawnPlayer()
    {
        loseScreen.SetActive(false);
        checkpoint.Respawn();
        Time.timeScale = 1f;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Game gesloten");
    }
}