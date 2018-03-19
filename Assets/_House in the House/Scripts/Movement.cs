using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField]
    private bool m_debug = true;
    private Transform m_transform;
    private Rigidbody2D m_rigidbody2d;
    private Animator m_animator;
    const string animator_velocityX = "HorizontalSpeed";
    const string animator_velocityY = "VerticalSpeed";
    const string animator_ground = "Ground";
    const string animator_jump = "Jump";
    const string animator_push = "Push";
    const string animator_climb = "Climb";
    const string animator_crouch = "Crouch";

    [SerializeField][ReadOnly]
    private PlayerMovementInput m_MovementInput;
    public PlayerMovementInput MovementInput
    {
        get { return m_MovementInput; }
    }

    [Header("Dependencies")]
    [SerializeField] private MousePlayer m_player;
    [SerializeField] GroundCheckCollision m_groundCheckCollisionScript;
    [SerializeField] PushCheckCollision m_PushCheckCollisionScript;
    [SerializeField] ClimbCheckCollision m_climbCheckCollisionScript;

    #region System
    private void Awake()
    {
        if (m_climbCheckCollisionScript == null && m_debug) Debug.LogError("Reference to \"ClimbCheckCollision\" script is not setted");
        if (m_groundCheckCollisionScript == null && m_debug) Debug.LogError("Reference to \"GroundCheckCollision\" script is not setted");

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
    }

    private void FixedUpdate()
    {
        CheckJump();
        CheckClimb();
        CheckPush();
        CheckCrouch();

        m_animator.SetFloat(animator_velocityX, m_rigidbody2d.velocity.x);
        m_animator.SetFloat(animator_velocityY, m_rigidbody2d.velocity.y);
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
    private void CheckJump()
    {
        m_animator.SetBool(animator_ground, m_groundCheckCollisionScript.Grounded);

        bool jump = !m_animator.GetBool(animator_ground) && !m_animator.GetBool(animator_climb);
        m_animator.SetBool(animator_jump, jump);
    }

    public void Jump()
    {
        if (m_MovementInput.Jump)
        {
            m_rigidbody2d.AddForce(new Vector2(0f, m_player.PlayerData.JumpForce));
            m_MovementInput.Jump = false;
        }
    }
    #endregion

    #region Push
    private void CheckPush()
    {
        bool IsPushing = m_player.PlayerData.CanPush 
            && m_PushCheckCollisionScript.Pushing 
            && (m_animator.GetFloat(animator_velocityX) > 0.1f || m_animator.GetFloat(animator_velocityX) < -0.1f);
        
        m_animator.SetBool(animator_push, IsPushing);
    }

    public void Push()
    {
        if (m_PushCheckCollisionScript.PushableObject != null)
        {
            Vector3 objectPosition = m_PushCheckCollisionScript.PushableObject.position;
            objectPosition.x += (m_MovementInput.InputHorizontal * m_player.PlayerData.PushSpeed * Time.deltaTime);
            m_PushCheckCollisionScript.PushableObject.position = objectPosition;
        }
        MoveX(m_MovementInput.InputHorizontal * m_player.PlayerData.PushSpeed * 1.5f);
    }
    #endregion

    #region Climb
    private void CheckClimb()
    {
        if (m_player.PlayerData.CanClimb)
        {
            m_animator.SetBool(animator_climb, m_climbCheckCollisionScript.Climbing);
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
            && !m_animator.GetBool(animator_climb)
            && !m_animator.GetBool(animator_jump)
            && !m_animator.GetBool(animator_push);

        m_animator.SetBool(animator_crouch, crouched);
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