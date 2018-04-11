using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBodyCollision : MonoBehaviour
{
    [SerializeField] private MousePlayer m_mouse;
    [SerializeField] private LayerMask m_WhatIsDanger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_WhatIsDanger.Contains(collision.gameObject.layer))
        {
            Vector2 force = collision.gameObject.GetComponent<ScriptDanger>().PushBackVelocity;
            m_mouse.Movement.SenseDanger(force);
        }
    }
}