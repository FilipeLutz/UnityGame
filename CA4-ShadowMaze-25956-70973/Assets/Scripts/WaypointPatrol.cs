using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
 * This script is used to make a GameObject patrol between a set of waypoints.
 * The GameObject will move to the next waypoint when it reaches the current one.
 * 
 * Attach this script to a GameObject with a NavMeshAgent component.
 * Assign the waypoints in the inspector.
 */

public class WaypointPatrol : MonoBehaviour
{
    // The NavMeshAgent component that will be used for movement
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;

    // The current waypoint index
    int m_CurrentWaypointIndex;

    void Start ()
    {
        // Start at the first waypoint
        navMeshAgent.SetDestination (waypoints[0].position);
    }

    void Update ()
    {
        // Update the destination of the NavMeshAgent to the current waypoint
        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination (waypoints[m_CurrentWaypointIndex].position);
        }
    }
}
