using UnityEngine;

public class GroundCheckCollision : MonoBehaviour
{
    [SerializeField] private Movement m_movement;
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private BoxCollider2D m_GroundColliderBox;
    [SerializeField] private CircleCollider2D m_GroundColliderCircle;
    [SerializeField] private float m_CastDistance = .05f;
    [SerializeField] private bool m_debug = true;

    private RaycastHit2D m_Hit;

    private bool m_groundedPreviousFrame = false;
    private bool m_groundedThisFrame;
    public bool Grounded
    {
        get { return m_groundedThisFrame && m_groundedPreviousFrame; }
    }

    private void Awake()
    {
        if (m_GroundColliderBox == null) m_GroundColliderBox = GetComponent<BoxCollider2D>();
        if (m_GroundColliderCircle == null) m_GroundColliderCircle = GetComponent<CircleCollider2D>();
        if (m_GroundColliderBox == null && m_GroundColliderCircle == null) Debug.LogError("No collider attached for ground detection.");
    }

    private void Start()
    {
        m_groundedThisFrame = false;
    }

    private void OnDrawGizmos()
    {
        if (m_debug)
        {
            Vector2 position = getPosition();
            position.y -= m_CastDistance;
            if (m_GroundColliderCircle != null) Gizmos.DrawWireSphere(position, m_GroundColliderCircle.radius);
            if (m_GroundColliderBox != null) Gizmos.DrawCube(position, m_GroundColliderBox.size);
        }
    }

    private void Update()
    {
        m_groundedPreviousFrame = m_groundedThisFrame;
        m_groundedThisFrame = false;
        
        if (m_GroundColliderCircle != null)
        {
            m_Hit = Physics2D.CircleCast(getPosition(), m_GroundColliderCircle.radius, -transform.up, m_CastDistance, m_WhatIsGround);
        }
        else if (m_GroundColliderBox != null)
        {
            m_Hit = Physics2D.BoxCast(getPosition(), m_GroundColliderBox.bounds.size, 0f, -transform.up, m_CastDistance, m_WhatIsGround);
        }

        if (m_Hit.collider != null)
        {
            m_groundedThisFrame = true;
            //Time.timeScale = 0f;
        }
    }

    private Vector2 getPosition()
    {
        if (m_GroundColliderBox != null)
            return new Vector2(
                m_GroundColliderBox.transform.position.x + (m_movement.FacingRight ? m_GroundColliderBox.offset.x : -m_GroundColliderBox.offset.x),
                m_GroundColliderBox.transform.position.y + m_GroundColliderBox.offset.y
            );
        else if (m_GroundColliderCircle != null)
        {
            return new Vector2(
                m_GroundColliderCircle.transform.position.x + (m_movement.FacingRight ? m_GroundColliderCircle.offset.x : -m_GroundColliderCircle.offset.x),
                m_GroundColliderCircle.transform.position.y + m_GroundColliderCircle.offset.y
            );
        }
        else return new Vector2(0f, 0f);
    }
}

