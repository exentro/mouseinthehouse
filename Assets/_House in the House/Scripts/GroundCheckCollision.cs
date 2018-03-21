using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class GroundCheckCollision : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;

    private bool m_grounded;
    public bool Grounded
    {
        get { return m_grounded; }
    }

    private void Start()
    {
        m_grounded = false;
    }
    
    private void OnCollisionStay2D(Collision2D collision)
    {
        m_grounded = false;
        if (collision.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            Debug.Log("collision with default");
            m_grounded = true;
        }
        /*
        //https://answers.unity.com/questions/50279/check-if-layer-is-in-layermask.html
        if (m_WhatIsGround == (m_WhatIsGround | (1 << collision.gameObject.layer)))
        {
            m_grounded = true;
        }
        else Debug.Log("test");
        */
        /*
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
*/


public class GroundCheckCollision : MonoBehaviour
{
    [SerializeField] private LayerMask m_WhatIsGround;
    [SerializeField] private Collider2D bodyCollider;
    [SerializeField] private float m_BoxCastDistance = .05f;
    [SerializeField] private float m_BoxCastSizeY = .05f;
    
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
            
        m_boxCastSize = new Vector2(bodyCollider.bounds.size.x, m_BoxCastSizeY);
        
    }

    private void FixedUpdate()
    {
        m_grounded = false;
        
        m_Hit = Physics2D.BoxCast(bodyCollider.bounds.center, m_boxCastSize, 0f, -transform.up, m_BoxCastDistance, m_WhatIsGround);
        if (m_Hit.collider != null)
        {
            m_grounded = true;
        }
    }
}

