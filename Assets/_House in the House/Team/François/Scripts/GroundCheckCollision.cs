using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GroundCheckCollision : MonoBehaviour
{
    private Transform m_transform;

    private int m_layerMask;
    private Vector2 m_boxCastSize;

    private void Start()
    {
        m_grounded = false;
        m_transform = transform;

        m_layerMask = LayerMask.GetMask("Default");
        m_boxCastSize = new Vector2(0.45f, 0.3f);
    }

    [SerializeField]
    private bool m_grounded;
    public bool Grounded
    {
        get { return m_grounded; }
    }

    /*
    //private void OnCollisionEnter2D(Collision2D collision) { }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            m_grounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.gameObject.layer == LayerMask.NameToLayer("Default"))
        {
            m_grounded = false;
        }
    }
    */
    
    private RaycastHit2D m_Hit;
    private void FixedUpdate()
    {
        m_grounded = false;
        m_Hit = Physics2D.BoxCast(m_transform.position, m_boxCastSize, 0f, -transform.up, .05f, m_layerMask);

        if (m_Hit.collider != null)
        {
            m_grounded = true;
        }
    }


}
