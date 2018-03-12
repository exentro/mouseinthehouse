using System;
using UnityEngine;
using Rewired;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        private Player m_player;
        private bool m_initialized;
        public int m_playerId;


        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
            
            m_initialized = false;
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
            else
            {
                if (!m_Jump)
                {
                    // Read the jump input in Update so button presses aren't missed.
                    m_Jump = m_player.GetButtonDown("Jump");
                }
            }
        }


        private void FixedUpdate()
        {
            // Read the inputs.
            if (m_initialized)
            {
                bool crouch = Input.GetKey(KeyCode.LeftControl);
                float h = m_player.GetAxis("MoveHorizontal");
                // Pass all parameters to the character control script.
                m_Character.Move(h, crouch, m_Jump);
                m_Jump = false;
            }
        }
    }
}
