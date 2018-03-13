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

    private void Start()
    {
        if (m_player == null) Debug.LogError("MousePlayer not set!");
        else
        {
            m_transform = m_player.Transform;
            m_rigidbody2d = m_player.Rigidbody2D;
            m_animator = m_player.Animator;
        }
    }

    [Header("Dependencies")]
    [SerializeField] private MousePlayer m_player;

    //Moving
    private bool m_FacingRight = true;

    //Jumping
    [SerializeField] private Transform m_GroundCheck;
    private bool m_Grounded = true;
    const float k_GroundedRadius = .15f;

    //Climbing
    [SerializeField] private Transform m_ClimbFaceCheck;
    [SerializeField] private Transform m_ClimbFeetCheck;
    private bool m_FaceOnClimbable; 
    private bool m_FeetOnClimbable;
    const float k_ClimbingCheckRadius = .05f;

    private void Awake()
    {
        if (m_GroundCheck == null) m_GroundCheck = transform.Find("GroundCheck");
        if (m_debug && m_GroundCheck == null) Debug.LogError("Can't find Transform \"GroundCheck\"");

        if (m_ClimbFaceCheck == null) m_ClimbFaceCheck = transform.Find("ClimbFaceCheck");
        if (m_debug && m_ClimbFaceCheck == null) Debug.LogError("Can't find Transform \"FaceCheck\"");

        if (m_ClimbFeetCheck == null) m_ClimbFeetCheck = transform.Find("ClimbFeetCheck");
        if (m_debug && m_ClimbFeetCheck == null) Debug.LogError("Can't find Transform \"ClimbFeetCheck\"");
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        CheckClimbable();
        m_animator.SetFloat("vSpeed", m_rigidbody2d.velocity.y);
    }

    private void CheckGrounded()
    {
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_player.PlayerData.WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }

        m_animator.SetBool("Ground", m_Grounded);
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

        if (m_FeetOnClimbable && !m_FaceOnClimbable) Debug.Log("Climbing done, top of the wall reached!");

        //m_Anim.SetBool("Climbing", m_LeanOnClimbable);
    }

    public void MoveX(float move)
    {
        if (m_Grounded || m_player.PlayerData.AirControl)
        {
            m_animator.SetFloat("Speed", Mathf.Abs(move));

            m_rigidbody2d.velocity = new Vector2(move * m_player.PlayerData.HorizontalSpeed, m_rigidbody2d.velocity.y);
            
            if (move > 0 && !m_FacingRight)
            {
                Flip();
            }
            else if (move < 0 && m_FacingRight)
            {
                Flip();
            }
        }
    }
    
    public void MoveY(float move)
    {
        if(m_FeetOnClimbable)
        {
            Vector3 newPosition = m_transform.position;

            newPosition.y += move * m_player.PlayerData.ClimbSpeed;
            m_transform.position = newPosition;
        }
    }

    public void Jump()
    {
        if (m_player.PlayerData.CanJump && m_Grounded && m_animator.GetBool("Ground"))
        {
            m_Grounded = false;
            m_animator.SetBool("Ground", false);
            m_animator.SetBool("Jump", true);
            m_rigidbody2d.AddForce(new Vector2(0f, m_player.PlayerData.JumpForce));
        }
    }
    
    private void Flip()
    {
        m_FacingRight = !m_FacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
