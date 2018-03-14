using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class GroundCheckCollision : MonoBehaviour
{
    private void Start()
    {
        m_grounded = false;
    }

    private bool m_grounded;
    public bool Grounded
    {
        get { return m_grounded; }
    }

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
}
