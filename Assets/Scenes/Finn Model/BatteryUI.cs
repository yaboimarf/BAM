using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;
public class BatteryUI : MonoBehaviour
{
    public UnityEngine.UI.Slider battery;
    public PlayerMovement2 playerMovement;


    // Update is called once per frame
    void Update()
    {
        battery.value = playerMovement.batteryRemaining;
    }
}
