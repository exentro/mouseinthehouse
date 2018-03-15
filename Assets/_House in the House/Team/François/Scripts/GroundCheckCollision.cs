using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheckCollision : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private float m_BoxCastDistance = .05f;

    private Transform m_transform;
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
        m_transform = transform;
        
        //m_boxCastSize = new Vector2(0.45f, 0.3f);        
        m_boxCastSize = new Vector2(bodyCollider.bounds.size.x, 0.1f);
    }

    private void FixedUpdate()
    {
        m_grounded = false;
        
        m_Hit = Physics2D.BoxCast(m_transform.position, m_boxCastSize, 0f, -transform.up, m_BoxCastDistance, m_WhatIsGround);
        if (m_Hit.collider != null)
        {
            m_grounded = true;
        }
    }
}
