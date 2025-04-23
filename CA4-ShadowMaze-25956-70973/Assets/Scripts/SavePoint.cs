using UnityEngine;

/*
 * SavePoint.cs
 * This script is attached to the save point object in the game.
 * When the player enters the trigger collider, it saves the player's position and time.
 */

public class SavePoint : MonoBehaviour
{
    // OnTriggerEnter is called when another collider enters the trigger collider attached to the object where this script is attached.
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject timerObject = GameObject.FindWithTag("GameTimer");
            GameTimer timer = timerObject.GetComponent<GameTimer>();

            CheckpointManager.Instance.SaveCheckpoint(other.transform.position, timer.GetCurrentTime());
            Debug.Log("Checkpoint saved.");
        }
    }
}