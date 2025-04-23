using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * GameManager class to handle game state and scene management.
 * It implements the Singleton pattern to ensure only one instance exists.
 * Allows for game over handling and scene reloading.
 */

public class GameManager : MonoBehaviour
{
    // Singleton instance
    public static GameManager Instance;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        Debug.Log("GameManager instance created.");
    }

    // GameOver method to handle game over state
    public void GameOver()
    {
        // Reload the scene or send to Game Over screen
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        Debug.Log("Game Over triggered.");
    }
}