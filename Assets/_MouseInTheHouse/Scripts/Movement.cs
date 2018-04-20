using System.Collections;
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

    [SerializeField][ReadOnly] private PlayerMovementInput m_MovementInput;
    public PlayerMovementInput MovementInput
    {
        get { return m_MovementInput;}
    }

    [Header("Dependencies")]
    [SerializeField] private MousePlayer m_player;
    private Transform m_transform;
    private Rigidbody2D m_rigidbody2d;
    private Animator m_animator;
    private CollidersProvider m_colliders;
    private AnimatorParameterMapper m_animatorParameters;

    #region System
    private void Awake()
    {
        m_MovementInput = new PlayerMovementInput();
    }
    private void Start()
    {
        if (m_player == null)
        {
            if (m_debug) Debug.LogError("MousePlayer not set!");
        }
        else
        {
            m_transform = m_player.Transform;
            if (m_transform == null && m_debug) Debug.LogError("Reference to Component \"Transform\" is not setted");

            m_rigidbody2d = m_player.Rigidbody2D;
            if (m_rigidbody2d == null && m_debug) Debug.LogError("Reference to Component \"Rigidbody2D\" is not setted");

            m_animator = m_player.MouseAnimator;
            if (m_animator == null && m_debug) Debug.LogError("Reference to Component \"Animator\" is not setted");

            m_animatorParameters = m_player.AnimatorParameterMapper;
            if (m_animatorParameters == null && m_debug) Debug.LogError("Reference to \"AnimatorParameterMapper\" script is not setted");

            m_colliders = m_player.CollidersProvider;
            if (m_colliders == null && m_debug) Debug.LogError("Reference to \"CollidersProvider\" script is not setted");

            jumpdCooldownTimer = 0f;

            if (m_player.PlayerData.OverridePhysics)
            {
                m_rigidbody2d.useAutoMass = false;
                m_rigidbody2d.mass = m_player.PlayerData.Mass;
                m_rigidbody2d.gravityScale = 1f;
            }
        }
    }
    private void FixedUpdate()
    {
        CheckJump();
        CheckClimb();
        CheckPush();
        CheckCrouch();

        CheckKinematic();

        AffectPhysics();

        SetAnimatorParameters();
    }
    private void Update()
    {
        m_animator.SetBool(m_animatorParameters.Ground, m_colliders.CollidingGround());
        jumpdCooldownTimer += Time.deltaTime;
    }
    #endregion

    private void CheckKinematic()
    {
        m_rigidbody2d.isKinematic = m_animator.GetBool(m_animatorParameters.Climb) && !m_animator.GetBool(m_animatorParameters.Jump) && !m_animator.GetBool(m_animatorParameters.Ground);
        
        if (m_rigidbody2d.isKinematic)
        {
            m_rigidbody2d.velocity = Vector2.zero;
        }
    }
    private void AffectPhysics()
    {
        if(m_player.PlayerData.OverridePhysics)
        {
            Vector2 velocity = m_rigidbody2d.velocity;

            if (velocity.y < 0f)
            {
                velocity.y += velocity.y * m_player.PlayerData.DescendingDrag;
                velocity.y = Mathf.Clamp(velocity.y, -m_player.PlayerData.MaxFallingSpeed, 0f);
            }
            else if (velocity.y > 0f)
            {
                velocity.y -= velocity.y * m_player.PlayerData.AscendingDrag;
            }
            m_rigidbody2d.velocity = velocity;
        }
    }
    private void SetAnimatorParameters()
    {
        m_animator.SetFloat(m_animatorParameters.HorizontalInput, m_MovementInput.InputHorizontal);
        m_animator.SetFloat(m_animatorParameters.VerticalInput, m_MovementInput.InputVertical);

        m_animator.SetFloat(m_animatorParameters.HorizontalSpeed, Mathf.Clamp(Mathf.Abs(m_rigidbody2d.velocity.x), 0f, 1f));
        m_animator.SetFloat(m_animatorParameters.VerticalSpeed, Mathf.Clamp(Mathf.Abs(m_rigidbody2d.velocity.y), 0f, 1f)); 
        m_animator.SetFloat(m_animatorParameters.VerticalVelocity, m_rigidbody2d.velocity.y);
    }
    
    #region Run
    private bool m_FacingRight = true;
    public bool FacingRight
    {
        get { return m_FacingRight; }
    }
    public void Run()
    {
        MoveX(m_MovementInput.InputHorizontal * m_player.PlayerData.SpeedMultiplier);
    }
    private void MoveX(float speed)
    {
        if (m_MovementInput.InputHorizontal > 0 && !m_FacingRight) Flip();
        else if (m_MovementInput.InputHorizontal < 0 && m_FacingRight) Flip();

        if ((m_colliders.CollidingPushable() && m_player.PlayerData.CanPush) || !m_colliders.CollidingPushable())
        {
            m_rigidbody2d.isKinematic = false;
            m_rigidbody2d.velocity = new Vector2(speed, m_rigidbody2d.velocity.y);
        }
        else
        {
            m_rigidbody2d.velocity = new Vector2(0f, m_rigidbody2d.velocity.y);
        }
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
        bool jump = !m_animator.GetBool(m_animatorParameters.Ground) && !m_animator.GetBool(m_animatorParameters.Climb);
        m_animator.SetBool(m_animatorParameters.Jump, jump);
    }    
    public bool IsJumping
    {
        get { return m_animator.GetBool(m_animatorParameters.Jump); }
    }
    public void Jump()
    {
        if (m_MovementInput.Jump)
        {
            if (jumpdCooldownTimer > m_player.PlayerData.JumpCooldown)
            {
                if (m_animator.GetBool(m_animatorParameters.Climb))
                {
                    if( (!FacingRight && m_MovementInput.InputHorizontal > 0.3f) || 
                        (FacingRight && m_MovementInput.InputHorizontal < -0.3f)    )
                    {
                        m_animator.SetBool(m_animatorParameters.Climb, false);
                        m_rigidbody2d.isKinematic = false;
                        m_animator.SetBool(m_animatorParameters.Jump, true);
                        m_rigidbody2d.AddForce(new Vector2(0f, m_player.PlayerData.JumpForce));
                    }
                    Run();
                }
                else if (m_animator.GetBool(m_animatorParameters.Ground))
                {
                    m_animator.SetBool(m_animatorParameters.Ground, false);
                    m_rigidbody2d.isKinematic = false;
                    m_animator.SetBool(m_animatorParameters.Jump, true);
                    m_rigidbody2d.AddForce(new Vector2(0f, m_player.PlayerData.JumpForce));
                }
                jumpdCooldownTimer = 0f;
            }
            else Debug.Log("Jump cooldown not ready.");
            
            m_MovementInput.Jump = false;
        }
    }
    #endregion

    #region Push
    private void CheckPush()
    {
        bool IsPushing = m_player.PlayerData.CanPush && m_colliders.CollidingPushable();
        m_animator.SetBool(m_animatorParameters.Push, IsPushing);
    }
    public bool IsPushing
    {
        get { return m_animator.GetBool(m_animatorParameters.Push); }
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

            m_animator.ResetTrigger(m_animatorParameters.ClimbEnd);
            if (m_colliders.ClimbingEnd())
            {
                m_animator.SetTrigger(m_animatorParameters.ClimbEnd);
            }         
        }
    }
    public void Climb()
    {
        if(!m_colliders.CollidingWithCeiling() || m_MovementInput.InputVertical < 0f)
        {
            Vector3 newPosition = m_transform.position;
            newPosition.y += m_MovementInput.InputVertical * m_player.PlayerData.ClimbSpeed * Time.deltaTime;
            m_transform.position = newPosition;
        }
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

        m_animator.SetBool(m_animatorParameters.CanStandUpFromCrouch, m_colliders.CanStandUpFromCrouch());
    }
    public bool IsCrawling
    {
        get { return m_animator.GetBool(m_animatorParameters.Crouch); }
    }
    public void Crawl()
    {
        MoveX(m_MovementInput.InputHorizontal * m_player.PlayerData.CrawlingSpeed);
    }
    #endregion

    #region Danger
    public void SenseDanger(Vector2 pushbackVelocity)
    {
        m_animator.SetBool(m_animatorParameters.Danger, true);
        pushbackVelocity.x *= FacingRight ? -1 : 1;
        m_rigidbody2d.isKinematic = false;
        m_rigidbody2d.velocity = pushbackVelocity;
    }
    #endregion
}

[System.Serializable]
public class PlayerMovementInput
{
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