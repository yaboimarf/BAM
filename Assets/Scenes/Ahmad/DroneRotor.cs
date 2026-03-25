using UnityEngine;

public class DroneRotor1 : MonoBehaviour
{
    public float speed = 1000f; // snelheid van draaien
    public Vector3 rotationAxis = Vector3.up; // draai richting

    void Update()
    {
        transform.Rotate(rotationAxis * speed * Time.deltaTime);
    }
}