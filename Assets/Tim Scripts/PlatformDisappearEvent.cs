using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDisappearEvent : GameEvent
{
    [Header("Platform Settings")]
    public string platformTag = "Platform";

    [Header("Player")]
    public Transform player;

    [Header("Aantal platforms")]
    public int platformsToDisable = 3;

    [Header("Timing")]
    public float disappearDuration = 3f;
    public float scaleSpeed = 2f;

    private List<GameObject> affectedPlatforms = new List<GameObject>();
    private Dictionary<GameObject, Vector3> originalScales = new Dictionary<GameObject, Vector3>();

    public override IEnumerator PlayEvent()
    {
        Debug.Log("Platform Disappear Event gestart!");

        affectedPlatforms.Clear();
        originalScales.Clear();

        GameObject[] platforms = GameObject.FindGameObjectsWithTag(platformTag);

        if (platforms.Length == 0)
            yield break;

        
        GameObject playerPlatform = GetPlayerPlatform();

        List<GameObject> platformList = new List<GameObject>(platforms);

        
        if (playerPlatform != null && platformList.Contains(playerPlatform))
        {
            platformList.Remove(playerPlatform);
        }

        int amount = Mathf.Min(platformsToDisable, platformList.Count);

        for (int i = 0; i < amount; i++)
        {
            int randomIndex = Random.Range(0, platformList.Count);
            GameObject chosen = platformList[randomIndex];

            affectedPlatforms.Add(chosen);
            originalScales.Add(chosen, chosen.transform.localScale);

            platformList.RemoveAt(randomIndex);
        }

        // platforms laten krimpen
        foreach (GameObject platform in affectedPlatforms)
        {
            StartCoroutine(ShrinkPlatform(platform));
        }

        yield return new WaitForSeconds(disappearDuration);

        // platforms weer laten groeien
        foreach (GameObject platform in affectedPlatforms)
        {
            StartCoroutine(GrowPlatform(platform));
        }
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

    IEnumerator ShrinkPlatform(GameObject platform)
    {
        Vector3 startScale = platform.transform.localScale;
        Vector3 targetScale = Vector3.zero;

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * scaleSpeed;
            platform.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
    }

    IEnumerator GrowPlatform(GameObject platform)
    {
        Vector3 startScale = platform.transform.localScale;
        Vector3 targetScale = originalScales[platform];

        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * scaleSpeed;
            platform.transform.localScale = Vector3.Lerp(startScale, targetScale, t);
            yield return null;
        }
    }
}