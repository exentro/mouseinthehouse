﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCheckCollision : MonoBehaviour
{
    [SerializeField] private bool m_debug = true;

    private void Start()
    {
        m_pushing = false;
    }

    private bool m_pushing;
    public bool Pushing
    {
        get { return m_pushing; }
    }

    private GameObject m_pushableObject;
    public Transform PushableObject
    {
        get
        {
            if (m_pushableObject == null) return null;
            return m_pushableObject.transform;
        }
    }
    
    //private void OnCollisionStay2D(Collision2D collision)
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject != m_pushableObject)
        {
            if (m_debug && m_pushableObject != null) Debug.LogWarning("New pushable object collision detected but another one still referenced. Replacing... Are they 2 pushable objects close at the same time ?");

            Interactable coll = collision.gameObject.GetComponent<Interactable>();
            if (coll != null)
            {
                m_pushing = coll.Pushable;
                if(coll.Pushable) m_pushableObject = collision.gameObject;
            }
        }
    }
    //private void OnCollisionExit2D(Collision2D collision)
    private void OnTriggerExit2D(Collider2D collision)
    {
        m_pushing = false;
        m_pushableObject = null;
    }
}
