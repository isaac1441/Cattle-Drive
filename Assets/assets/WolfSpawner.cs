using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WolfSpawner : MonoBehaviour
{
    public GameObject wolfPrefab;  // Assign the Wolf prefab in the Inspector
    public float spawnOffset = 1f; // Wolves spawn right outside the screen
    public float timeBetweenWaves = 5f; // Delay after all wolves are killed
    public int maxWaves = 3; // Number of waves
    private int currentWave = 1;
    private Camera mainCamera;
    private int activeWolves = 0; // Track number of wolves in scene

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave <= maxWaves)
        {
            yield return StartCoroutine(SpawnWave(currentWave)); // Spawn wolves one by one
            yield return new WaitUntil(() => activeWolves <= 0); // Wait until all wolves are killed
            yield return new WaitForSeconds(timeBetweenWaves); // Wait before next wave
            currentWave++;
        }
    }

    IEnumerator SpawnWave(int wolfCount)
    {
        Debug.Log("Spawning Wave " + wolfCount);

        for (int i = 0; i < wolfCount; i++)
        {
            Vector3 spawnPosition = GetSpawnPositionJustOutsideScreen(i);
            GameObject wolf = Instantiate(wolfPrefab, spawnPosition, Quaternion.identity);

            // Make sure the wolf is fully active
            wolf.SetActive(true);

            // Ensure its renderer is enabled
            SpriteRenderer wolfRenderer = wolf.GetComponent<SpriteRenderer>();
            if (wolfRenderer != null)
            {
                wolfRenderer.enabled = true;
            }
            else
            {
                Debug.LogError($"Wolf {i + 1} spawned but has NO SpriteRenderer!");
            }

            Debug.Log($"Wolf {i + 1} spawned at {spawnPosition}");

            activeWolves++;

            WolfAI wolfScript = wolf.GetComponent<WolfAI>();
            if (wolfScript != null)
            {
                wolfScript.OnWolfKilled += WolfKilled;
            }

            yield return new WaitForSeconds(0.3f); // Delay before spawning next wolf
        }
    }


    void WolfKilled()
    {
        activeWolves--;

        Debug.Log("Wolf killed. Remaining wolves: " + activeWolves);

        if (activeWolves <= 0)
        {
            GameManager gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                gm.WaveCompleted(); // Notify the GameManager when the wave ends
            }
        }
    }


    Vector3 GetSpawnPositionJustOutsideScreen(int index)
    {
        float screenHeight = mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;
        float cameraX = mainCamera.transform.position.x;

        float verticalPosition = Random.Range(-screenHeight * 0.9f, screenHeight * 0.9f);
        float spawnX = cameraX + screenWidth + spawnOffset;

        return new Vector3(spawnX, verticalPosition, 0); // Corrected Y-axis usage
    }


}
