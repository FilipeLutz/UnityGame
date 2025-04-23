using UnityEngine;


/** * CheckpointManager is a singleton class that manages the player's checkpoint data.
 * It saves the player's position and time when a checkpoint is reached and allows
 * loading that data when respawning.
 */

public class CheckpointManager : MonoBehaviour
{
    // Singleton instance
    public static CheckpointManager Instance;
    public Vector3 savedPosition;
    public float savedTime;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Check if an instance already exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // SaveCheckpoint saves the player's position and time
    public void SaveCheckpoint(Vector3 position, float time)
    {
        savedPosition = position;
        savedTime = time;
    }

    // LoadCheckpoint loads the saved position and time
    public void LoadCheckpoint(GameObject player, GameTimer timer)
    {
        player.transform.position = savedPosition;
        timer.SetTime(savedTime);
    }
}