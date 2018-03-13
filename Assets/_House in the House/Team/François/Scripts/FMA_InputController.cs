using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMA_InputController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private MousePlayer m_player;
    private Movement m_movement;

    [Header("Debug")]
    [SerializeField] private bool m_debug = true;

    private void Awake()
    {
        if (m_player == null) m_player = GetComponent<MousePlayer>();
        if (m_debug && m_player == null) Debug.LogError("Can't find Component MousePlayer");
    }
    
    private void Start()
    {
        m_movement = m_player.Movement;
        if (m_debug && m_movement == null) Debug.LogError("Can't find Component Movement");
    }

    void Update ()
    {
        bool jump = Input.GetButtonDown("Jump");
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (jump) m_movement.Jump();
        m_movement.MoveX(h);
        m_movement.MoveY(v);
    }
}
