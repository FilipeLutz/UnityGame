using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/** 
 * GameEnding.cs
 * This script handles the game ending conditions, including player exit and caught scenarios.
 * It manages the display of background images and audio playback during the ending sequence.
 */

public class GameEnding : MonoBehaviour
{
    // Public variables to set in the Unity Inspector
    public float fadeDuration = 1f;
    public float displayImageDuration = 1f;
    public GameObject player;
    public CanvasGroup exitBackgroundImageCanvasGroup;
    public AudioSource exitAudio;
    public CanvasGroup caughtBackgroundImageCanvasGroup;
    public AudioSource caughtAudio;
    public GameObject mapOverlay;
    public GameObject menuPanel;
    public GameObject timerText;
    public GameObject timerBackground;

    // Private variables to track game state
    bool m_IsPlayerAtExit;
    bool m_IsPlayerCaught;
    float m_Timer;
    bool m_HasAudioPlayed;

    // OnTriggerEnter is called when the Collider other enters the trigger
    void OnTriggerEnter(Collider other)
    {
        // Check if the player has entered the trigger
        if (other.gameObject == player)
        {
            m_IsPlayerAtExit = true;
        }

        Debug.Log("Player has entered the exit trigger.");
    }

    // CaughtPlayer is called when the player is caught
    public void CaughtPlayer()
    {
        // This method is called when the player is caught
        m_IsPlayerCaught = true;

        Debug.Log("Player has been caught.");
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player is at the exit or caught
        if (m_IsPlayerAtExit)
        {
            EndLevel(exitBackgroundImageCanvasGroup, false, exitAudio);
        }
        else if(m_IsPlayerCaught)
        {
            EndLevel(caughtBackgroundImageCanvasGroup, true, caughtAudio);
        }

        Debug.Log("Game ending conditions checked.");
    }

    // EndLevel handles the end of the game
    void EndLevel(CanvasGroup imageCanvasGroup, bool doRestart, AudioSource audioSource)
    {
        // Hide Map Overlay,Menu Panel, Timer Text, and Timer Background
        if (mapOverlay != null)
            mapOverlay.SetActive(false);

        if (menuPanel != null)
            menuPanel.SetActive(false);

        if (timerText != null)
            timerText.SetActive(false);

        if (timerBackground != null)
            timerBackground.SetActive(false);

        // Check if the audio has already played
        if (!m_HasAudioPlayed)
        {
            audioSource.Play();
            m_HasAudioPlayed = false;
        }
        // Fade in the image
        m_Timer += Time.deltaTime;
        imageCanvasGroup.alpha = m_Timer / fadeDuration;
        if(m_Timer > fadeDuration + displayImageDuration)
        {
            // Fade out the image
            if (doRestart)
            {
                SceneManager.LoadScene(0);
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
        }

        Debug.Log("Ending level with fade effect.");
    }
}
