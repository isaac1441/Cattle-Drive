using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public StarDisplay starDisplay;   // <- Reference to StarDisplay script
    private bool gameEnded = false;

    public List<GameObject> cows = new List<GameObject>();
    private int remainingWaves; // Track remaining waves
    private int activeWolves; // Track active wolves

    void Start()
    {
        cows.AddRange(GameObject.FindGameObjectsWithTag("Cow"));

        // Find the WolfSpawner and get max waves
        WolfSpawner spawner = FindObjectOfType<WolfSpawner>();
        if (spawner != null)
        {
            remainingWaves = spawner.maxWaves;
        }
    }

    public void CowDespawned(GameObject cow)
    {
        if (cows.Contains(cow))
        {
            cows.Remove(cow);
            Debug.Log($"Cow despawned: {cow.name}. Correct count: {cows.Count}");
        }

        if (starDisplay != null)
        {
            Debug.Log($"Updating stars with {cows.Count} cows remaining");
            starDisplay.ShowStars(cows.Count); // FORCE refresh after cow removal
        }

        if (cows.Count <= 0)
        {
            EndGame("All cows are dead! You lose.");
        }
    }





    public void RegisterCow(GameObject cow)
    {
        if (!cows.Contains(cow))
        {
            cows.Add(cow);
            Debug.Log($"Registered cow: {cow.name}. Current count: {cows.Count}");
        }
        else
        {
            Debug.LogWarning($"Duplicate cow registration detected for {cow.name}!");
        }
    }


    public void UnregisterCow(GameObject cow)
    {
        if (cows.Contains(cow))
        {
            cows.Remove(cow);
            Debug.Log($"Unregistered cow: {cow.name}. Updated count: {cows.Count}");
        }
        else
        {
            Debug.LogWarning($"Tried to remove a cow that wasn't in the list: {cow.name}");
        }
    }


    public void WolfKilled()
    {
        activeWolves--;

        // Check if all wolves are dead AND no more waves remain
        if (activeWolves <= 0 && remainingWaves <= 0)
        {
            EndGame("All wolves defeated! You win.");
        }
    }

    public void WaveCompleted()
    {
        remainingWaves--;

        Debug.Log("Wave completed! Remaining waves: " + remainingWaves);

        if (remainingWaves <= 0 && activeWolves <= 0)
        {
            EndGame("All waves completed! You win.");
        }
    }


    void EndGame(string message)
    {
        gameEnded = true;
        Debug.Log("Game Over! " + message);

        // Pause the game
        Time.timeScale = 1f;

        // Show Game Over screen
        gameOverScreen.SetActive(true);

        // Show stars based on remaining cows
        if (starDisplay != null)
        {
            starDisplay.ShowStars(cows.Count);
        }
        else
        {
            Debug.LogWarning("StarDisplay not set in GameManager!");
        }
    }

    public void RestartButton()
    {
        SceneManager.LoadScene("CD");
    }
}
