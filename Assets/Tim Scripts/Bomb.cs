using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public static int currentEventCount = 0;

    private GameObject loseScreen;
    private int spawnEventNumber;

    void Start()
    {
        spawnEventNumber = currentEventCount;
    }

    // functie om LoseScreen automatisch te koppelen
    public void SetLoseScreen(GameObject screen)
    {
        loseScreen = screen;
        if (loseScreen != null)
            loseScreen.SetActive(false); // verberg hem bij start
    }

    void Update()
    {
        // bom verdwijnt na 2 events
        if (currentEventCount >= spawnEventNumber + 2)
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player geraakt door bom!");

            if (loseScreen != null)
            {
                loseScreen.SetActive(true);
            }

            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            Destroy(gameObject);
        }
    }
}
