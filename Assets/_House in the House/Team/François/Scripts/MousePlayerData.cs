using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MouseData", menuName = "MouseInTheHouse/MouseData", order = 1)]
public class MousePlayerData : ScriptableObject
{
    [Header("Left-Right Movement")]
    [SerializeField] private float m_MaxSpeed = 10f;
    public float HorizontalSpeed
    {
        get { return m_MaxSpeed; }
    }
    
    [Header("Jump")]
    [SerializeField] private bool m_canJump = true;
    public bool CanJump
    {
        get { return m_canJump; }
    }

    [SerializeField] private bool m_AirControl = false;
    public bool AirControl
    {
        get { return m_AirControl; }
    }

    [SerializeField] private float m_JumpForce = 400f;
    public float JumpForce
    {
        get { return m_JumpForce; }
    }

    [SerializeField] private LayerMask m_WhatIsGround;
    public LayerMask WhatIsGround
    {
        get { return m_WhatIsGround; }
    }

    [Header("Climb")]
    [SerializeField] [Range(0.01f, 0.1f)] private float m_ClimbingSpeedMultiplier;
    public float ClimbSpeed
    {
        get { return m_ClimbingSpeedMultiplier; }
    }
}
