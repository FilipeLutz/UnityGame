﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine; // for MonoBehaviour

/*
 * PlayerMovement.cs
 * 
 * This script controls character movement using Unity's Rigidbody physics and Animator system.
 * It listens for horizontal and vertical input, plays walk animations and audio when moving,
 * and rotates the character to face its movement direction. Movement and rotation are applied 
 * during OnAnimatorMove for smooth physics-based animation blending.
 */

public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f;  // Angle (in radians) character turns per sec
    
    Animator m_Animator;
    Rigidbody m_Rigidbody;
    AudioSource m_AudioSource;
    Vector3 m_Movement;  // Default value = (0, 0, 0)
    Quaternion m_Rotation = Quaternion.identity;  // Default value = no rotation

    // Start is called before the first frame update automatically
    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void FixedUpdate()  // Called 50 times per sec
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        
        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();
        
        // 3. Identify whether there is player Input
        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);
        if(isWalking)
        {
            if(!m_AudioSource.isPlaying)
            {
                m_AudioSource.Play();
            }
        }
        else
        {
            m_AudioSource.Stop();
        }

        // 4. For the character to face its direction movement
        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void OnAnimatorMove()
    {
        // 5. Apply movement and rotation to the character
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }
}

// 1. Input Manager - create variables for x and y axes
// 2. Normalize the Input
// 3. Identify whether there is player Input
// 4. For the character to face its direction movement (& rotation)
// 5. Apply movement and rotation to the character