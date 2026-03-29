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

    private float eventTimer = 0f;   // TIMER VOOR EVENTS

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        checkpoint = player.GetComponent<PlayerCheckpoint>();

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

    void Update()
    {
        // EVENT TIMER
        eventTimer += Time.deltaTime;

        if (eventTimer >= 10f)
        {
            TriggerEvent();
            eventTimer = 0f;
        }
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

    // ⭐ EVENT FUNCTIE
    public void TriggerEvent()
    {
       

        // SPEEL EVENT SOUND ELKE KEER
        if (SoundManager.instance != null)
            SoundManager.instance.PlayEvent(0.5f);
    }

    public void PlayerDied()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlayDeath(0.5f);

        loseScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ShowWinScreen()
    {
        if (SoundManager.instance != null)
            SoundManager.instance.PlayWin(0.5f);

        WinScreen.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void RespawnToMainMenu()
    {
        WinScreen.SetActive(false);
        mainMenu.SetActive(true);

        Time.timeScale = 1f;
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
