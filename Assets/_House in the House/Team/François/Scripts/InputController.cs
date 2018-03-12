using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private Movement m_movement;

    [Header("Debug")]
    [SerializeField] private bool m_debug = true;

    private void Awake()
    {
        if (m_movement == null) m_movement = GetComponent<Movement>();
        if (m_debug && m_movement == null) Debug.LogError("Can't find Component Movement");
    }

    void Update ()
    {
        bool jump = Input.GetButtonDown("Jump");
        float h = Input.GetAxis("Horizontal");

        if (jump) m_movement.Jump();
        m_movement.MoveX(h);
    }
}
