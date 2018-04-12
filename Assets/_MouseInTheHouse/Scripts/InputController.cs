using System;
using UnityEngine;
using Rewired;

public class InputController : MonoBehaviour
{
    private Player m_player;
    private PlayerMovementInput m_playerMovementInputs;
    private PlayerActionInput m_playerActiontInputs;

    private bool m_allowInputs;
    public bool AllowPlayerInput
    {
        get { return m_allowInputs; }
        set { m_allowInputs = value; }
    }

    [SerializeField] private MousePlayer m_mousePlayer;

    private bool m_initialized;
    private bool m_crouch;

    private void Awake()
    {
        m_initialized = false;

        if (m_mousePlayer == null) gameObject.GetComponent<MousePlayer>();
        if (m_mousePlayer == null) Debug.LogError("Can't find Component \"MousePlayer\"");
    }

    private void Start()
    {
        m_playerMovementInputs = m_mousePlayer.Movement.MovementInput;
        m_playerActiontInputs = m_mousePlayer.Action.ActionInput;
        m_allowInputs = true;
    }

    private void Initialize()
    {
        m_player = ReInput.players.GetPlayer(m_mousePlayer.PlayerID);
        m_initialized = true;
    }
    
    private void Update()
    {
        if (!ReInput.isReady) return; // Exit if Rewired isn't ready. This would only happen during a script recompile in the editor.
        if (!m_initialized) Initialize(); // Reinitialize after a recompile in the editor
        else if(m_allowInputs) GetInput();
    }

    private void GetInput()
    {
        m_playerMovementInputs.InputHorizontal = m_player.GetAxis("MoveHorizontal");
        m_playerMovementInputs.InputVertical = m_player.GetAxis("MoveVertical");

        m_playerMovementInputs.Jump = m_player.GetButtonDown("Jump");
        m_playerMovementInputs.Crouch = m_player.GetButton("Crouch");
        
        m_playerActiontInputs.Nibble = m_player.GetButtonDown("Nibble");
        m_playerActiontInputs.Retry = m_player.GetButtonDown("Retry");
        
        m_playerActiontInputs.CameraHorizontal = m_player.GetAxis("CameraHorizontal");
        m_playerActiontInputs.CameraVertical = m_player.GetAxis("CameraVertical");

        m_playerActiontInputs.Zoom = m_player.GetAxis("Zoom");
        m_playerActiontInputs.Unzoom = m_player.GetAxis("Unzoom");
    }
}
