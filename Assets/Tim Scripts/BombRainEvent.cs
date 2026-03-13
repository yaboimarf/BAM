using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombRainEvent : GameEvent
{
    [Header("Platform instellingen")]
    public string platformTag = "Platform";

    [Header("Bomb instellingen")]
    public GameObject bombPrefab;
    public int totalBombsToSpawn = 10;

    [Header("Spawn instellingen")]
    public float spawnHeight = 6f;
    public float spawnSpread = 2f;

    [Header("LoseScreen (Canvas)")]
    public GameObject loseScreen;

    public override IEnumerator PlayEvent()
    {
        Debug.Log("Bomb Rain Event gestart!");

        GameObject[] platforms = GameObject.FindGameObjectsWithTag(platformTag);

        if (platforms.Length == 0)
        {
            Debug.LogWarning("Geen platforms gevonden!");
            yield break;
        }

        for (int i = 0; i < totalBombsToSpawn; i++)
        {
            GameObject randomPlatform = platforms[Random.Range(0, platforms.Length)];

            Vector3 platformPos = randomPlatform.transform.position;

            float randomX = Random.Range(-spawnSpread, spawnSpread);
            float randomZ = Random.Range(-spawnSpread, spawnSpread);

            Vector3 spawnPos = new Vector3(
                platformPos.x + randomX,
                platformPos.y + spawnHeight,
                platformPos.z + randomZ
            );

            GameObject bomb = Instantiate(bombPrefab, spawnPos, Quaternion.identity);

            // Zorg dat het LoseScreen veld automatisch wordt ingesteld
            Bomb bombScript = bomb.GetComponent<Bomb>();
            if (bombScript != null)
            {
                bombScript.SetLoseScreen(loseScreen);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }
}
