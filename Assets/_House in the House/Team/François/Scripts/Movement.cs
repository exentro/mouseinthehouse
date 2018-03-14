using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField]
    private bool m_debug = true;
    private Transform m_transform;
    private Rigidbody2D m_rigidbody2d;
    private Animator m_animator;

    [SerializeField]
    private PlayerMovementInput m_MovementInput;
    public PlayerMovementInput MovementInput
    {
        get { return m_MovementInput; }
    }

    [Header("Dependencies")]
    [SerializeField] private MousePlayer m_player;
    
    #region System
    private void Awake()
    {
        if (m_ClimbFaceCheck == null) m_ClimbFaceCheck = transform.Find("ClimbFaceCheck");
        if (m_debug && m_ClimbFaceCheck == null) Debug.LogError("Can't find Transform \"FaceCheck\"");

        if (m_ClimbFeetCheck == null) m_ClimbFeetCheck = transform.Find("ClimbFeetCheck");
        if (m_debug && m_ClimbFeetCheck == null) Debug.LogError("Can't find Transform \"ClimbFeetCheck\"");

        m_MovementInput = new PlayerMovementInput();
    }

    private void Start()
    {
        if (m_player == null) Debug.LogError("MousePlayer not set!");
        else
        {
            m_transform = m_player.Transform;
            m_rigidbody2d = m_player.Rigidbody2D;
            m_animator = m_player.Animator;
        }

        Transform m_GroundCheck = transform.Find("GroundCheck");
        if (m_GroundCheck == null)
        {
            if (m_debug) Debug.LogError("Can't find Transform \"GroundCheck\"");
            else
            {
                m_groundCheckCollisionScript = m_GroundCheck.GetComponent<GroundCheckCollision>();
                if (m_groundCheckCollisionScript == null && m_debug) Debug.LogError("Can't find Script Component \"GroundCheckCollision\"");
            }
        }
    }

    private void FixedUpdate()
    {
        //CheckGrounded();
        IsOnGround();
        //CheckClimbable();
        m_animator.SetFloat(animator_VelocityY, m_rigidbody2d.velocity.y);
    }
    #endregion

    #region Run
    private bool m_FacingRight = true;
    const string animator_VelocityX = "Speed";

    public void Run()
    {
        if (m_animator.GetBool(animator_ground) || (m_animator.GetBool(animator_jump) && m_player.PlayerData.AirControl))
        {
            //m_animator.SetFloat(animator_VelocityX, Mathf.Abs(m_MovementInput.InputHorizontal));
            m_animator.SetFloat(animator_VelocityX, m_MovementInput.InputHorizontal);

            m_rigidbody2d.velocity = new Vector2(m_MovementInput.InputHorizontal * m_player.PlayerData.HorizontalSpeed, m_rigidbody2d.velocity.y);

            if (m_MovementInput.InputHorizontal > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (m_MovementInput.InputHorizontal < 0 && m_FacingRight)
            {
                Flip();
            }
        }
    }
    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
    #endregion

    #region Jump
    [SerializeField] GroundCheckCollision m_groundCheckCollisionScript;
    //private Transform m_GroundCheck;
    const string animator_ground = "Ground";
    const string animator_jump = "Jump";
    const float k_GroundedRadius = .15f;

    public void Jump()
    {
        if (m_player.PlayerData.CanJump && m_animator.GetBool(animator_ground) && m_MovementInput.Jump)
        {
            m_animator.SetBool(animator_ground, false);
            m_animator.SetBool(animator_jump, true);
            m_rigidbody2d.AddForce(new Vector2(0f, m_player.PlayerData.JumpForce));
        }
    }
    private void IsOnGround()
    {
        bool grounded = m_groundCheckCollisionScript.
            Grounded;
        if(grounded)
        {
            m_animator.SetBool(animator_ground, true);
            m_animator.SetBool(animator_jump, false);
        }
        else
        {
            m_animator.SetBool(animator_ground, false);
            m_animator.SetBool(animator_jump, true);
        }
    }
    /*
    private void IsOnGround_OLD()
    {
        bool grounded = false;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_player.PlayerData.WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                m_animator.SetBool(animator_jump, false);
            }
        m_animator.SetBool(animator_ground, grounded);
    }*/
    #endregion

    #region Climb
    [SerializeField] private Transform m_ClimbFaceCheck;
    [SerializeField] private Transform m_ClimbFeetCheck;
    private bool m_FaceOnClimbable; 
    private bool m_FeetOnClimbable;
    const float k_ClimbingCheckRadius = .05f;
    const string animator_VelocityY = "vSpeed";

    public void Climb()
    {
        if (m_FeetOnClimbable)
        {
            Vector3 newPosition = m_transform.position;

            newPosition.y += m_MovementInput.InputVertical * m_player.PlayerData.ClimbSpeed;
            m_transform.position = newPosition;
        }
    }
    private void CheckClimbable()
    {
        m_FaceOnClimbable = false;
        m_FeetOnClimbable = false;

        Collider2D[] collidersFace = Physics2D.OverlapCircleAll(m_ClimbFaceCheck.position, k_ClimbingCheckRadius);
        for (int i = 0; i < collidersFace.Length; i++)
        {
            if (collidersFace[i].gameObject != gameObject)
                if (collidersFace[i].GetComponent<Climbable>() != null)
                    m_FaceOnClimbable = true;
        }

        Collider2D[] collidersFeet = Physics2D.OverlapCircleAll(m_ClimbFeetCheck.position, k_ClimbingCheckRadius);
        for (int i = 0; i < collidersFeet.Length; i++)
        {
            if (collidersFeet[i].gameObject != gameObject)
                if (collidersFeet[i].GetComponent<Climbable>() != null)
                    m_FeetOnClimbable = true;
        }

        if (m_debug && m_FeetOnClimbable && !m_FaceOnClimbable) Debug.Log("Climbing done, top of the wall reached!");
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

}