using UnityEngine;
using UnityEngine.SceneManagement;

/**
 * MenuController1 is responsible for handling the main menu interactions.
 * It allows the player to start the game or quit the application.
 * It also manages the background music for the main menu.
 */

public class MenuController1 : MonoBehaviour
{
    // Reference to the AudioSource component for background music
    public AudioSource mainMenuMusic;

    // Reference to the AudioClip for the main menu music
    public void StartGame()
    {
        // Check if the main menu music is playing and stop it
        if (mainMenuMusic != null)
        {   // Check if the music is playing
            mainMenuMusic.Stop(); 
        }
        // Load the main game scene
        SceneManager.LoadScene("MainScene");

        Debug.Log("Game started.");
    }

    /// Method to quit the game
    public void QuitGame()
    {   // Check if the main menu music is playing and stop it
        Application.Quit();
        Debug.Log("Quit Game clicked.");
    }
}