using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMA_InputController : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private MousePlayer m_player;
    private PlayerMovementInput m_playerMovementInput;

    [Header("Debug")]
    [SerializeField] private bool m_debug = true;

    private void Awake()
    {
        if (m_player == null) m_player = GetComponent<MousePlayer>();
        if (m_debug && m_player == null) Debug.LogError("Can't find Component MousePlayer");
    }
    
    private void Start()
    {
        if (m_debug && m_player.Movement == null) Debug.LogError("Can't find Component Movement");
        m_playerMovementInput = m_player.Movement.MovementInput;
        if (m_debug && m_playerMovementInput == null) Debug.LogError("Can't retrieve Movement.MovementInput reference");
    }

    void Update ()
    {
        m_playerMovementInput.Jump = Input.GetButtonDown("Jump");
        m_playerMovementInput.InputHorizontal = Input.GetAxis("Horizontal");
        m_playerMovementInput.InputVertical = Input.GetAxis("Vertical");
    }
}
