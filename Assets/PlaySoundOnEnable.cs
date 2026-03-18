using UnityEngine;

public class PlaySoundOnEnable : MonoBehaviour
{
    public float volume = 1f;

    void OnEnable()
    {
        SoundManager.instance.PlayDeath(volume);
    }
}
