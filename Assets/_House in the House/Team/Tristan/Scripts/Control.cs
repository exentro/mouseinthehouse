using System;
using UnityEngine;
using Rewired;

public class Control : MonoBehaviour
{
    private Player m_player;
    private Movement m_movement;
    private bool m_initialized;
    public int m_playerId;
    private float m_moveX;
    private float m_moveY;
    private bool m_jump;
    private bool m_crouch;

    private void Awake()
    {
        m_initialized = false;
        m_movement = new Movement();
    }

    private void Initialize()
    {
        // Get the Rewired Player object for this player.
        m_player = ReInput.players.GetPlayer(m_playerId);
        m_initialized = true;
    }
    
    private void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!m_initialized) Initialize(); // Reinitialize after a recompile in the editor
        else GetInput();
    }

    private void FixedUpdate()
    {
        if (m_initialized)
        {
            m_moveX = m_player.GetAxis("MoveHorizontal");
            m_moveY = m_player.GetAxis("MoveVertical");
            Move();
            //{
            //    float h = m_player.GetAxis("MoveHorizontal");
            //    // Pass all parameters to the character control script.
            //    m_Character.Move(h, crouch, m_jump);
            //    m_jump = false;
            //}
        }
    }

    private void GetInput()
    {
        if (!m_jump) m_jump = m_player.GetButtonDown("Jump");
        m_crouch = m_player.GetButtonDown("Crouch");
    }

    private void Move()
    {
        m_movement.MoveX(m_moveX);
        m_movement.MoveY(m_moveY);
        if (m_jump)
        {
            m_movement.Jump();
            m_jump = false;
        }
    }
}
