using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject settingsCanvas;
    private bool isOpen = false;

    void Start()
    {
        settingsCanvas.SetActive(false); // menu start uit
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isOpen)
            {
                settingsCanvas.SetActive(false);
                isOpen = false;

                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;

                Time.timeScale = 1f;
            }
            else
            {
                settingsCanvas.SetActive(true);
                isOpen = true;

                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                Time.timeScale = 0f;
            }
        }
    }
}
