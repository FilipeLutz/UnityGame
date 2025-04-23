using UnityEngine;

/*
 * This script is attached to a collectible object that gives the player extra time when collected.
 * It checks for collision with the player and adds bonus time to the game timer.
 * 
 * At the end it is not being used in the game, but it is a good example of how to implement a collectible item.
 */

public class TimeCollectible : MonoBehaviour
{
    // This is the amount of time to add to the game timer when collected
    public float bonusTime = 10f;

    // This method is called when the collider attached to this object enters a trigger collider
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject timerObject = GameObject.FindWithTag("GameTimer");
            if (timerObject != null)
            {
                GameTimer timer = timerObject.GetComponent<GameTimer>();
                if (timer != null)
                {
                    timer.AddTime(bonusTime);
                }
            }

            Destroy(gameObject);
        }
    }
}