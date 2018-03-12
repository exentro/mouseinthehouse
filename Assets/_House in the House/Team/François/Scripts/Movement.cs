using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Animator m_Anim;
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    [SerializeField] private Transform m_transform;
    [SerializeField] private Transform m_GroundCheck;
    [SerializeField] private Transform m_ClimbFaceCheck;
    [SerializeField] private Transform m_ClimbFeetCheck;

    [Header("Debug")]
    [SerializeField] private bool m_debug = true;

    [Header("Left-Right Movement")]
    [SerializeField] private float m_MaxSpeed = 10f;
    private bool m_FacingRight = true;
    
    [Header("Jump")]
    [SerializeField] private bool m_CanJump = true;
    [SerializeField] private bool m_AirControl = false;
    [SerializeField] private float m_JumpForce = 400f;
    [SerializeField] private LayerMask m_WhatIsGround;
    private bool m_Grounded;
    const float k_GroundedRadius = .25f;

    [Header("Climb")]
    [SerializeField] private bool m_FaceOnClimbable; 
    [SerializeField] private bool m_FeetOnClimbable;
    [SerializeField] [Range(0.01f, 0.1f)] private float m_ClimbingSpeedMultiplier;
    const float k_ClimbingCheckRadius = .05f;

    private void Awake()
    {
        if (m_GroundCheck == null) m_GroundCheck = transform.Find("GroundCheck");
        if (m_debug && m_GroundCheck == null) Debug.LogError("Can't find Transform \"GroundCheck\"");

        if (m_ClimbFaceCheck == null) m_ClimbFaceCheck = transform.Find("ClimbFaceCheck");
        if (m_debug && m_ClimbFaceCheck == null) Debug.LogError("Can't find Transform \"FaceCheck\"");

        if (m_ClimbFeetCheck == null) m_ClimbFeetCheck = transform.Find("ClimbFeetCheck");
        if (m_debug && m_ClimbFeetCheck == null) Debug.LogError("Can't find Transform \"ClimbFeetCheck\"");

        if (m_Anim == null) m_Anim = GetComponent<Animator>();
        if (m_debug && m_Anim == null) Debug.LogError("Can't find Component Animator");

        if (m_Rigidbody2D == null) m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (m_debug && m_Rigidbody2D == null) Debug.LogError("Can't find Component Rigidbody2D");

        if (m_transform == null) m_transform = GetComponent<Transform>();
        if (m_debug && m_transform == null) Debug.LogError("Can't find Component Transform");
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        CheckClimbable();
        m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);
    }

    private void CheckGrounded()
    {
        m_Grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
                m_Grounded = true;
        }

        m_Anim.SetBool("Ground", m_Grounded);
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
        if (m_Grounded || m_AirControl)
        {
            m_Anim.SetFloat("Speed", Mathf.Abs(move));
            
            m_Rigidbody2D.velocity = new Vector2(move * m_MaxSpeed, m_Rigidbody2D.velocity.y);
            
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
        //if(m_FaceOnClimbable)
        if(m_FeetOnClimbable)
        {
            Vector3 newPosition = m_transform.position;

            newPosition.y += move * m_ClimbingSpeedMultiplier;
            m_transform.position = newPosition;
        }
    }

    public void Jump()
    {
        if (m_CanJump && m_Grounded && m_Anim.GetBool("Ground"))
        {
            m_Grounded = false;
            m_Anim.SetBool("Ground", false);
            m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
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
