using UnityEngine;

public class FullscreenOnStart : MonoBehaviour
{
    [Header("Fullscreen Settings")]
    public bool fullscreen = true;
    public FullScreenMode mode = FullScreenMode.ExclusiveFullScreen;

    [Header("Optional Resolution (0 = keep current)")]
    public int width = 0;
    public int height = 0;
    public int refreshRate = 0; // 0 = default

    void Start()
    {
        // If no custom resolution is set, just apply fullscreen mode.
        if (width <= 0 || height <= 0)
        {
            Screen.fullScreenMode = mode;
            Screen.fullScreen = fullscreen;
            return;
        }

        // Apply fullscreen + resolution
        if (refreshRate > 0)
            Screen.SetResolution(width, height, mode, refreshRate);
        else
            Screen.SetResolution(width, height, mode);
    }
}