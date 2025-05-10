using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverScreen;
    public StarDisplay starDisplay;   // <- Reference to StarDisplay script
    private float timer = 0f;
    public float gameDuration = 30f;
    private bool gameEnded = false;

    public List<GameObject> cows = new List<GameObject>();

    void Start()
    {
        // Optional: Auto-fill cow list at start
        cows.AddRange(GameObject.FindGameObjectsWithTag("Cow"));
    }

    void Update()
    {
        if (gameEnded) return;

        timer += Time.deltaTime;

        if (timer >= gameDuration)
        {
            EndGame();
        }
    }

    public void CowDespawned()
    {
        // Remove a cow from list and check count
        if (cows.Count > 0)
            cows.RemoveAt(0);

        if (cows.Count <= 0)
        {
            EndGame();
        }
    }

    public void RegisterCow(GameObject cow)
    {
        if (!cows.Contains(cow))
            cows.Add(cow);
    }

    public void UnregisterCow(GameObject cow)
    {
        cows.Remove(cow);
    }

    void EndGame()
    {
        gameEnded = true;
        Debug.Log("Game Over!");

        // Pause the game
        Time.timeScale = 0f;

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

    // Method to restart the game
    public void RestartGame()
    {
        Time.timeScale = 1f; // Resume normal game speed
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
