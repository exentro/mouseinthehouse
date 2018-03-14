using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbCheckCollision : MonoBehaviour
{
    private void Start()
    {
        m_climbing = false;
    }

    [SerializeField]
    private bool m_climbing;
    public bool Climbing
    {
        get { return m_climbing; }
    }

    //private void OnCollisionEnter2D(Collision2D collision) { }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Interactable coll = collision.gameObject.GetComponent<Interactable>();
        if(coll != null)
        {
            m_climbing = coll.Climbable;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        m_climbing = false;
    }
}
