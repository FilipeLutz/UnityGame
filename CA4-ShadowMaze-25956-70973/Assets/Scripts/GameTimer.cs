using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

/** 
 * GameTimer.cs
 * This script manages the game timer, including countdown, display updates, and audio warnings.
 * It also handles the visibility of the map overlay and menu controller interactions.
 * Additionally, it provides methods to manipulate the timer from other scripts.
 */

public class GameTimer : MonoBehaviour
{
    // Public variables to set in the Unity Inspector
    public float totalTime = 190f;
    public TMP_Text timerText;
    public AudioSource warningSound;
    float previousTime;
    public GameObject mapOverlay; 
    private bool hasHiddenMap = false;
    private bool isRunning = true;
    private bool hasPlayedWarning = false;
    private float blinkTimer = 0f;
    private bool isBlinking = false;
    public MenuController menuController;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the timer
        Time.timeScale = 1f;
        UpdateDisplay();
        previousTime = totalTime;

        Debug.Log("GameTimer started with total time: " + totalTime);
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the game is running and time scale is not zero
        if (!isRunning || Time.timeScale == 0) return;

        // Update the timer
        totalTime -= Time.deltaTime;

        // Game Over
        if (totalTime <= 0)
        {
            totalTime = 0;
            isRunning = false;
            SceneManager.LoadScene("MainMenu"); 
        }

        // Play sound & blink when crossing 10s
        if (previousTime > 10f && totalTime <= 10f)
        {
            // Play warning sound
            if (warningSound != null) warningSound.Play();
            hasPlayedWarning = true;
        }

        // Blink the timer text when crossing 10s
        if (totalTime <= 10f)
        {
            BlinkTimerText();
        }

        // Hide map overlay and show panel in game menu when time after 10 seconds
        if (!hasHiddenMap && totalTime <= 180f)
        {
            // Hide map overlay and show menu
            if (mapOverlay != null)
                mapOverlay.SetActive(false);

            // Show panel menu controller
            if (menuController != null)
                menuController.ShowMenu();

            // Set the map overlay to be hidden
            hasHiddenMap = true;
        }

        // Update the timer display
        UpdateDisplay();
        previousTime = totalTime;

        Debug.Log("GameTimer updated. Current time: " + totalTime);
    }

    // Blink the timer text every 0.5 seconds
    void BlinkTimerText()
    {
        // Check if the timer text is not null
        blinkTimer += Time.deltaTime;
        if (blinkTimer >= 0.5f)
        {
            // Toggle the visibility of the timer text
            isBlinking = !isBlinking;
            timerText.enabled = isBlinking;
            blinkTimer = 0f;
        }

        Debug.Log("Blinking timer text. Is blinking: " + isBlinking);
    }

    // Update the timer display
    void UpdateDisplay()
    {
        // Check if the timer text is not null
        int minutes = Mathf.FloorToInt(totalTime / 60F);
        int seconds = Mathf.FloorToInt(totalTime % 60F);
        // Format the timer text
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Change the color of the timer text based on time remaining
        if (totalTime > 180f)
        {
            timerText.color = Color.green; 
        }
        else if (totalTime <= 31f)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }

        Debug.Log("Timer display updated. Current time: " + totalTime);
    }

    // Method to add time to the timer. There would be a collectable item that adds time, but it was not added to the game.
    public void AddTime(float seconds)
    {
        // Check if the time to add is positive
        totalTime += seconds;
    }

    // Get the current time remaining
    public float GetCurrentTime()
    {
        return totalTime;
    }

    // Set the timer to a specific time
    public void SetTime(float newTime)
    {
        totalTime = newTime;
    }

    // Stop the timer
    public void StopTimer()
    {
        isRunning = false;
    }

    // Start the timer
    public void StartTimer()
    {
        isRunning = true;
    }
}