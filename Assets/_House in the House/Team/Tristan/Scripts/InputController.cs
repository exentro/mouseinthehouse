using System;
using UnityEngine;
using Rewired;

public class InputController : MonoBehaviour
{
    private Player m_player;
    private PlayerMovementInput m_playerMovementInputs;

    [SerializeField] private MousePlayer m_mousePlayer;

    private bool m_initialized;
    /*
    private float m_moveX;
    private float m_moveY;
    private bool m_jump;
    */
    private bool m_crouch;

    private void Awake()
    {
        m_initialized = false;

        if (m_mousePlayer == null) GetComponent<MousePlayer>();
        if (m_mousePlayer == null) Debug.LogError("Can't find Component \"MousePlayer\"");

        m_playerMovementInputs = m_mousePlayer.Movement.MovementInput;
    }

    private void Initialize()
    {
        // Get the Rewired Player object for this player.
        m_player = ReInput.players.GetPlayer(m_mousePlayer.PlayerID);
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
            m_playerMovementInputs.InputHorizontal = m_player.GetAxis("MoveHorizontal");
            m_playerMovementInputs.InputVertical = m_player.GetAxis("MoveVertical");
        }
    }

    private void GetInput()
    {
        //m_playerMovementInputs.Reset();

        //if (!m_playerMovementInputs.Jump)
            m_playerMovementInputs.Jump = m_player.GetButtonDown("Jump");
        
        //m_crouch = m_player.GetButtonDown("Crouch");
    }
}
