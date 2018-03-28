using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MouseData", menuName = "MouseInTheHouse/MouseData", order = 1)]
public class MousePlayerData : ScriptableObject
{
    [Header("Physics")]
    [SerializeField] private bool m_overridePhysics;
    public bool OverridePhysics
    {
        get { return m_overridePhysics; }
        set { m_overridePhysics = value; }
    }

    [SerializeField] private float m_mass = 2f;
    public float Mass
    {
        get { return m_mass; }
    }
    [SerializeField] private float m_ascendingDrag = 0.05f;
    public float AscendingDrag
    {
        get { return m_ascendingDrag; }
    }
    [SerializeField] private float m_descendingDrag = 0.15f;
    public float DescendingDrag
    {
        get { return m_descendingDrag; }
    }
    [SerializeField] private float m_maxFallingSpeed = 30f;
    public float MaxFallingSpeed
    {
        get { return m_maxFallingSpeed; }
    }

    [Header("Left-Right Movement")]
    [SerializeField] private float m_MaxHorizontalSpeed = 10f;
    public float MaxHorizontalSpeed
    {
        get { return m_MaxHorizontalSpeed; }
    }
    [SerializeField] private float m_SpeedMultiplier = 1f;
    public float SpeedMultiplier
    {
        get { return m_SpeedMultiplier; }
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

    [SerializeField] private float m_JumpCooldown = .5f;
    public float JumpCooldown
    {
        get { return m_JumpCooldown; }
    }

    [Header("Climb")]
    [SerializeField]
    private bool m_canClimb = true;
    public bool CanClimb
    {
        get { return m_canClimb; }
    }
    [SerializeField] private float m_ClimbingSpeedMultiplier;
    public float ClimbSpeed
    {
        get { return m_ClimbingSpeedMultiplier; }
    }
    [SerializeField] private bool m_canClimbJump = true;
    public bool CanClimbJump
    {
        get { return m_canClimbJump; }
        set { m_canClimbJump = value; }
    }

    [Header("Push")]
    [SerializeField] private bool m_canPush = true;
    public bool CanPush
    {
        get { return m_canPush; }
    }
    [SerializeField] private float m_PushingSpeedMultiplier;
    public float PushSpeed
    {
        get { return m_PushingSpeedMultiplier; }
    }

    [Header("Crouch")]
    [SerializeField]
    private bool m_canCrouch = true;
    public bool CanCrouch
    {
        get { return m_canCrouch; }
    }
    [SerializeField] private float m_CrawlingSpeedMultiplier;
    public float CrawlingSpeed
    {
        get { return m_PushingSpeedMultiplier; }
    }
}
