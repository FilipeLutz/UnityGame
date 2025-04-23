using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * MenuController is responsible for managing the panel in game menu, including pausing,
 * resuming, restarting the game, and exiting to the main menu.
 * It handles the visibility of the pause and resume buttons, as well as the menu panel.
 * Audio sources are paused or resumed when the game is paused or resumed.
 */

public class MenuController : MonoBehaviour
{
    /// Reference to the pause and resume buttons and the menu panel
    public GameObject pauseButton;
    public GameObject resumeButton;
    public GameObject menuPanel;
    private bool isPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the game state
        resumeButton.SetActive(false);
        pauseButton.SetActive(true);
        menuPanel.SetActive(false);
        Time.timeScale = 1f;
        Debug.Log("Start() called!");
    }
    // Pause the game when the pause button is clicked
    void PauseGame()
    {
        // Pause the game and show the menu
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        resumeButton.SetActive(true);

        // All audio sources in the scene are paused
        AudioSource[] allSources = FindObjectsOfType<AudioSource>();
        foreach (var src in allSources)
        {
            if (src.isPlaying)
                src.Pause();
        }

        Debug.Log("Pause clicked!");
    }

    // Resume the game when the resume button is clicked
    void ResumeGame()
    {
        // Resume the game and hide the menu
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        resumeButton.SetActive(false);

        // All audio sources in the scene are resumed
        AudioSource[] allSources = FindObjectsOfType<AudioSource>();
        foreach (var src in allSources)
        {
            src.UnPause();
        }

        Debug.Log("Resume clicked!");
    }

    // Restart the game when the restart button is clicked
    public void RestartGame()
    {
        // Restart the game and reset the time scale
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        Debug.Log("Restart clicked!");
    }

    public void ExitToMenu()
    {
        // Exit to the main menu and reset the time scale
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");

        Debug.Log("Exit clicked!");
    }

    // Show the menu when the menu button is clicked
    public void ShowMenu()
    {
        // Show the menu panel and pause the game
        menuPanel.SetActive(true);   
        pauseButton.SetActive(true);  
        resumeButton.SetActive(false);

        Debug.Log("Show Panel Menu called!");
    }
}