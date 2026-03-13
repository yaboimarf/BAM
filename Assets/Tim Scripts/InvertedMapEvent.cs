using System.Collections;
using UnityEngine;

public class InvertedMapEvent : GameEvent
{
    [Header("Camera")]
    public Transform playerCamera;

    [Header("Event Settings")]
    public float eventDuration = 6f;
    public float rotateSpeed = 3f;

    private Quaternion normalRotation;
    private Quaternion invertedRotation;

    public override IEnumerator PlayEvent()
    {
        Debug.Log("Inverted Map Event gestart!");

        normalRotation = playerCamera.localRotation;
        invertedRotation = Quaternion.Euler(
            normalRotation.eulerAngles.x,
            normalRotation.eulerAngles.y,
            180f
        );

        // camera omdraaien
        yield return StartCoroutine(RotateCamera(invertedRotation));

        yield return new WaitForSeconds(eventDuration);

        // camera terugdraaien
        yield return StartCoroutine(RotateCamera(normalRotation));

        Debug.Log("Inverted Map Event voorbij!");
    }

    IEnumerator RotateCamera(Quaternion targetRotation)
    {
        Quaternion startRotation = playerCamera.localRotation;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * rotateSpeed;
            playerCamera.localRotation = Quaternion.Lerp(startRotation, targetRotation, t);
            yield return null;
        }
    }
}
