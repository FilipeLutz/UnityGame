using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Observer.cs
 * 
 * This script is attached to NPCs or environmental agents that can "see" the player.
 * It uses a trigger zone to determine when the player is nearby, and a raycast to check
 * for direct line-of-sight. If the player is in range and visible, it calls the CaughtPlayer()
 * method on the GameEnding script to trigger a game over or related event.
 *
 */

public class Observer : MonoBehaviour
{
    // Reference to the player transform
    public Transform player;
    public GameEnding gameEnding;

    bool m_IsPlayerInRange;

    // Called when another collider enters the trigger zone
    void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    // Called when another collider exits the trigger zone
    void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }

    // Called every frame
    void Update()
    {
        if (m_IsPlayerInRange)
        {
            // Build a ray from this object to the player
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            // Cast a ray and check if the player is directly visible
            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    // If the ray hits the player without obstruction, trigger the game over
                    gameEnding.CaughtPlayer();
                }
            }
        }
    }
}