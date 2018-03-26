using UnityEngine;

public class GroundCheckCollision : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private BoxCollider2D m_GroundColliderBox;
    [SerializeField] private CircleCollider2D m_GroundColliderCircle;
    [SerializeField] private float m_BoxCastDistance = .05f;
    
    private RaycastHit2D m_Hit;

    private bool m_grounded;
    public bool Grounded
    {
        get { return m_grounded; }
    }

    private void Awake()
    {
        if (m_GroundColliderBox == null) m_GroundColliderBox = GetComponent<BoxCollider2D>();
        if (m_GroundColliderCircle == null) m_GroundColliderCircle = GetComponent<CircleCollider2D>();
        if (m_GroundColliderBox == null && m_GroundColliderCircle == null) Debug.LogError("No collider attached for ground detection.");
    }

    private void Start()
    {
        m_grounded = false;
    }

    private void FixedUpdate()
    {
        m_grounded = false;

        if (m_GroundColliderCircle != null)
        {
            m_Hit = Physics2D.CircleCast(m_GroundColliderCircle.transform.position, m_GroundColliderCircle.radius, -transform.up, m_BoxCastDistance, m_WhatIsGround);
        }
        else if (m_GroundColliderBox != null)
        {
            m_Hit = Physics2D.BoxCast(m_GroundColliderBox.transform.position, m_GroundColliderBox.bounds.size, 0f, -transform.up, m_BoxCastDistance, m_WhatIsGround);
        }
        
        if (m_Hit.collider != null) m_grounded = true;
    }
}

