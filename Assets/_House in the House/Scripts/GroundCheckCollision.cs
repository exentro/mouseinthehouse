using UnityEngine;

public class GroundCheckCollision : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Collider2D m_GroundCollider;
    [SerializeField] private float m_BoxCastDistance = .05f;
    
    private RaycastHit2D m_Hit;
    private Vector2 m_boxCastSize;

    private bool m_grounded;
    public bool Grounded
    {
        get { return m_grounded; }
    }

    private void Start()
    {
        m_grounded = false;
            
        m_boxCastSize = new Vector2(m_GroundCollider.bounds.extents.x, m_GroundCollider.bounds.extents.y);        
    }

    private void FixedUpdate()
    {
        m_grounded = false;
        
        m_Hit = Physics2D.BoxCast(m_GroundCollider.bounds.center, m_boxCastSize, 0f, -transform.up, m_BoxCastDistance, m_WhatIsGround);
        if (m_Hit.collider != null)
        {
            Debug.DrawLine(m_GroundCollider.bounds.center, m_Hit.point, Color.red);
            m_grounded = true;
        }
    }
}

