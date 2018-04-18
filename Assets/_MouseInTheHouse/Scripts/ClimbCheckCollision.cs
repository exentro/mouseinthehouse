using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbCheckCollision : MonoBehaviour
{
    private void Start()
    {
        m_climbing = false;
    }
    
    private bool m_climbing;
    public bool Climbing
    {
        get { return m_climbing; }
    }
    
    private void OnTriggerStay2D(Collider2D collision)
    {
        Interactable coll = collision.gameObject.GetComponent<Interactable>();
        if(coll != null)
        {
            m_climbing = coll.Climbable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_climbing = false;
    }
}
