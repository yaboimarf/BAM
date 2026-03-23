using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaosPlatformEvent : GameEvent
{
    [Header("Platform Settings")]
    public string platformTag = "Platform";

    [Header("Player")]
    public Transform player;

    [Header("Chaos Settings")]
    public float moveDistance = 3f;
    public float moveSpeed = 2f;
    public float eventDuration = 6f;

    private List<GameObject> platforms = new List<GameObject>();
    private Dictionary<GameObject, Vector3> originalPositions = new Dictionary<GameObject, Vector3>();
    private GameObject playerPlatform;

    public override IEnumerator PlayEvent()
    {
        Debug.Log("CHAOS PLATFORM EVENT gestart!");

        // platforms ophalen
        GameObject[] foundPlatforms = GameObject.FindGameObjectsWithTag(platformTag);

        platforms.Clear();
        originalPositions.Clear();

        foreach (GameObject p in foundPlatforms)
        {
            platforms.Add(p);
            originalPositions[p] = p.transform.position;
        }

        // player platform detecteren
        playerPlatform = GetPlayerPlatform();

        float timer = 0f;

        while (timer < eventDuration)
        {
            timer += Time.deltaTime;

            foreach (GameObject platform in platforms)
            {
                if (platform == null) continue;

                // skip platform waar speler op staat
                if (platform == playerPlatform) continue;

                MovePlatformRandom(platform);
            }

            yield return null;
        }

        // alles terug naar originele positie
        foreach (GameObject platform in platforms)
        {
            if (platform == null) continue;

            StartCoroutine(MoveBack(platform));
        }

        Debug.Log("CHAOS PLATFORM EVENT voorbij!");
    }

    GameObject GetPlayerPlatform()
    {
        RaycastHit hit;

        if (Physics.Raycast(player.position, Vector3.down, out hit, 5f))
        {
            if (hit.collider.CompareTag(platformTag))
            {
                return hit.collider.gameObject;
            }
        }

        return null;
    }

    void MovePlatformRandom(GameObject platform)
    {
        Vector3 randomOffset = new Vector3(
            Mathf.Sin(Time.time * moveSpeed + platform.GetInstanceID()) * moveDistance,
            Mathf.Cos(Time.time * moveSpeed + platform.GetInstanceID()) * moveDistance,
            Mathf.Sin(Time.time * moveSpeed * 0.5f + platform.GetInstanceID()) * moveDistance
        );

        platform.transform.position = originalPositions[platform] + randomOffset;
    }

    IEnumerator MoveBack(GameObject platform)
    {
        Vector3 startPos = platform.transform.position;
        Vector3 targetPos = originalPositions[platform];

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * moveSpeed;
            platform.transform.position = Vector3.Lerp(startPos, targetPos, t);
            yield return null;
        }

        platform.transform.position = targetPos;
    }
}