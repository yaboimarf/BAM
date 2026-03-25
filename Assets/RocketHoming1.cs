using System;
using UnityEngine;

public class RocketHoming1 : MonoBehaviour
{
    [Header("Rocket Settings")]
    public float speed = 20f;
    public float lifeTime = 5f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void Update()
    {
        // 🚀 recht vooruit vliegen (GEEN homing meer)
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("PLAYER DEAD 💀");

            GameObject loseScreen = GameObject.Find("LoseScreen");
            if (loseScreen != null)
            {
                loseScreen.SetActive(true);
            }

            Destroy(gameObject);

            Time.timeScale = 0f;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    internal void SetTarget(Transform player)
    {
        throw new NotImplementedException();
    }
}