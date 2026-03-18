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
            }
            else
            {
                settingsCanvas.SetActive(true);
                isOpen = true;
            }
        }
    }
}
