using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushCheckCollision : MonoBehaviour
{
    private void Start()
    {
        m_pushing = false;
    }

    private bool m_pushing;
    public bool Pushing
    {
        get { return m_pushing; }
    }

    //private void OnCollisionEnter2D(Collision2D collision) { }
    private void OnCollisionStay2D(Collision2D collision)
    {
        Interactable coll = collision.gameObject.GetComponent<Interactable>();
        if (coll != null)
        {
            m_pushing = coll.Pushable;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        m_pushing = false;
    }
}
