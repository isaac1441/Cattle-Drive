using UnityEngine;
using System.Collections;

public class WolfSpawner : MonoBehaviour
{
    public GameObject wolfPrefab;  // Assign the Wolf prefab in the Inspector
    public float spawnRadius = 10f; // Random spawn range
    public float timeBetweenWaves = 10f; // Set delay between waves to 10 sec
    public int maxWaves = 3; // Number of waves
    private int currentWave = 1;

    void Start()
    {
        StartCoroutine(SpawnWaves());
    }

    IEnumerator SpawnWaves()
    {
        while (currentWave <= maxWaves)
        {
            SpawnWave(currentWave); // Spawn wolves based on wave number
            yield return new WaitForSeconds(timeBetweenWaves); // Wait for 10 sec before next wave
            currentWave++;
        }
    }

    void SpawnWave(int wolfCount)
    {
        Debug.Log("Spawning Wave " + wolfCount);

        for (int i = 0; i < wolfCount; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(-spawnRadius, spawnRadius),
                0, // Keep it on ground level
                Random.Range(-spawnRadius, spawnRadius)
            );

            Instantiate(wolfPrefab, randomPosition, Quaternion.identity);
        }
    }
}
