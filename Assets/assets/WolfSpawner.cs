using UnityEngine;
using System.Collections;

public class WolfSpawner : MonoBehaviour
{
    public GameObject wolfPrefab;  // Assign the Wolf prefab in the Inspector
    public float spawnOffset = 1f; // Wolves spawn right outside the screen
    public float timeBetweenWaves = 10f; // Delay between waves
    public int maxWaves = 3; // Number of waves
    private int currentWave = 1;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave <= maxWaves)
        {
            SpawnWave(currentWave);
            yield return new WaitForSeconds(timeBetweenWaves);
            currentWave++;
        }
    }

    void SpawnWave(int wolfCount)
    {
        Debug.Log("Spawning Wave " + wolfCount);

        for (int i = 0; i < wolfCount; i++)
        {
            Vector3 spawnPosition = GetSpawnPositionJustOutsideScreen();
            Instantiate(wolfPrefab, spawnPosition, Quaternion.identity);
        }
    }

    Vector3 GetSpawnPositionJustOutsideScreen()
    {
        float screenHeight = mainCamera.orthographicSize;
        float screenWidth = screenHeight * mainCamera.aspect;

        // Randomly pick one of four edges (left, right, top, bottom)
        int edge = Random.Range(0, 4);
        Vector3 spawnPosition = Vector3.zero;

        switch (edge)
        {
            case 0: // Left
                spawnPosition = new Vector3(-screenWidth - spawnOffset, 0, Random.Range(-screenHeight, screenHeight));
                break;
            case 1: // Right
                spawnPosition = new Vector3(screenWidth + spawnOffset, 0, Random.Range(-screenHeight, screenHeight));
                break;
            case 2: // Top
                spawnPosition = new Vector3(Random.Range(-screenWidth, screenWidth), 0, screenHeight + spawnOffset);
                break;
            case 3: // Bottom
                spawnPosition = new Vector3(Random.Range(-screenWidth, screenWidth), 0, -screenHeight - spawnOffset);
                break;
        }

        return spawnPosition;
    }
}
