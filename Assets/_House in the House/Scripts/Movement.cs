﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool m_debug = true;
    [SerializeField] [ReadOnly] private E_MouseState m_currentState;
    public E_MouseState State
    {
        get { return m_currentState; }
        set
        {
            m_colliders.EnableCollider(value);
            m_currentState = value;
        }
    }

    [SerializeField][ReadOnly]
    private PlayerMovementInput m_MovementInput;
    public PlayerMovementInput MovementInput
    {
        get { return m_MovementInput;}
    }

    [Header("Dependencies")]
    [SerializeField] private MousePlayer m_player;
    [SerializeField] [ReadOnly] private Transform m_transform;
    [SerializeField] [ReadOnly] private Rigidbody2D m_rigidbody2d;
    [SerializeField] [ReadOnly] private Animator m_animator;
    [SerializeField] private CollidersProvider m_colliders;
    [SerializeField] private AnimatorParameterMapper m_animatorParameters;

    #region System
    private void Awake()
    {
        m_MovementInput = new PlayerMovementInput();
    }

    private void Start()
    {
        if (m_colliders == null && m_debug) Debug.LogError("Reference to \"CollidersProvider\" script is not setted");
        if (m_animatorParameters == null && m_debug) Debug.LogError("Reference to \"AnimatorParameterMapper\" script is not setted");

        if (m_player == null)
        {
            if(m_debug) Debug.LogError("MousePlayer not set!");
        }
        else
        {
            m_transform = m_player.Transform;
            m_rigidbody2d = m_player.Rigidbody2D;
            m_animator = m_player.Animator;
        }

        jumpdCooldownTimer = 0f;
    }

    private void FixedUpdate()
    {
        CheckJump();
        CheckClimb();
        CheckPush();
        CheckCrouch();

        m_animator.SetFloat(m_animatorParameters.HorizontalSpeed, m_rigidbody2d.velocity.x);
        m_animator.SetFloat(m_animatorParameters.VerticalSpeed, m_rigidbody2d.velocity.y);
    }
    #endregion

    #region Run
    private bool m_FacingRight = true;

    public void Run()
    {
        MoveX(m_MovementInput.InputHorizontal * m_player.PlayerData.SpeedMultiplier);
    }
    private void MoveX(float speed)
    {
        float maxSpeed = m_player.PlayerData.MaxHorizontalSpeed;

        m_rigidbody2d.velocity = new Vector2(Mathf.Clamp(speed, -maxSpeed, maxSpeed), m_rigidbody2d.velocity.y);

        if (m_MovementInput.InputHorizontal > 0 && !m_FacingRight) Flip();
        else if (m_MovementInput.InputHorizontal < 0 && m_FacingRight) Flip();
    }

    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 theScale = m_transform.localScale;
        theScale.x *= -1;
        m_transform.localScale = theScale;
    }
    #endregion

    #region Jump
    private float jumpdCooldownTimer;
    private void CheckJump()
    {
        jumpdCooldownTimer += Time.fixedDeltaTime;

        m_animator.SetBool(m_animatorParameters.Ground, m_colliders.CollidingGround());

        bool jump = !m_animator.GetBool(m_animatorParameters.Ground) && !m_animator.GetBool(m_animatorParameters.Climb);
        m_animator.SetBool(m_animatorParameters.Jump, jump);
    }

    public void Jump()
    {
        if (m_MovementInput.Jump)
        {
            if(jumpdCooldownTimer > m_player.PlayerData.JumpCooldown)
            {
                m_rigidbody2d.AddForce(new Vector2(0f, m_player.PlayerData.JumpForce));
                m_animator.SetBool(m_animatorParameters.Ground, false);
                m_animator.SetBool(m_animatorParameters.Jump, true);
                jumpdCooldownTimer = 0f;
            }
            m_MovementInput.Jump = false;
        }
    }
    #endregion

    #region Push
    private void CheckPush()
    {
        bool IsPushing = m_player.PlayerData.CanPush 
            && m_colliders.CollidingPushable()
            && (m_animator.GetFloat(m_animatorParameters.HorizontalSpeed) > 0.1f || m_animator.GetFloat(m_animatorParameters.HorizontalSpeed) < -0.1f);
        
        m_animator.SetBool(m_animatorParameters.Push, IsPushing);
    }

    public void Push()
    {
        Transform obj = m_colliders.CollidingPushableObjectTransform();
        if (obj != null)
        {
            Vector3 objectPosition = obj.position;
            objectPosition.x += (m_MovementInput.InputHorizontal * m_player.PlayerData.PushSpeed * Time.deltaTime);
            obj.position = objectPosition;
        }
        MoveX(m_MovementInput.InputHorizontal * m_player.PlayerData.PushSpeed * 1.5f);
    }
    #endregion

    #region Climb
    private void CheckClimb()
    {
        if (m_player.PlayerData.CanClimb)
        {
            m_animator.SetBool(m_animatorParameters.Climb, m_colliders.CollidingClimbable());
        }
    }

    public void Climb()
    {
        Vector3 newPosition = m_transform.position;
        newPosition.y += m_MovementInput.InputVertical * m_player.PlayerData.ClimbSpeed;
        m_transform.position = newPosition;
    }
    #endregion

    #region Crouch
    private void CheckCrouch()
    {
        bool crouched = m_player.PlayerData.CanCrouch
            && m_MovementInput.Crouch
            && !m_animator.GetBool(m_animatorParameters.Climb)
            && !m_animator.GetBool(m_animatorParameters.Jump)
            && !m_animator.GetBool(m_animatorParameters.Push);

        m_animator.SetBool(m_animatorParameters.Crouch, crouched);
    }
    public void Crawl()
    {
        MoveX(m_MovementInput.InputHorizontal * m_player.PlayerData.CrawlingSpeed);
    }
    #endregion
}

[System.Serializable]
public class PlayerMovementInput
{
    public void Reset()
    {
        InputVertical = 0f;
        InputHorizontal = 0f;
        Jump = false;
    }

    [SerializeField] private float m_inputVertical;
    public float InputVertical
    {
        get { return m_inputVertical; }
        set { m_inputVertical = value; }
    }

    [SerializeField] private float m_inputHorizontal;
    public float InputHorizontal
    {
        get { return m_inputHorizontal; }
        set { m_inputHorizontal = value; }
    }

    [SerializeField] private bool m_jump;
    public bool Jump
    {
        get { return m_jump; }
        set { m_jump = value; }
    }

    [SerializeField] private bool m_crouch;
    public bool Crouch
    {
        get { return m_crouch; }
        set { m_crouch = value; }
    }
}