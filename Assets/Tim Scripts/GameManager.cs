using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject loseScreen;

    PlayerCheckpoint player;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        player = Object.FindFirstObjectByType<PlayerCheckpoint>();
        loseScreen.SetActive(false);
    }

    public void PlayerDied()
    {
        Time.timeScale = 0f;
        loseScreen.SetActive(true);
    }

    public void RespawnPlayer()
    {
        Time.timeScale = 1f;
        loseScreen.SetActive(false);
        player.Respawn();
    }
}
