using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint: MonoBehaviour
{
    #region Public Members

    public int m_checkpointNumber;
    public bool m_player0Triggered;
    public bool m_player1Triggered;

    #endregion

    #region Public Function

    #endregion

    #region System

    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MousePlayer mousePlayer = collision.gameObject.GetComponentInParent<MousePlayer>();
        if (mousePlayer != null)
        {
            int id = mousePlayer.PlayerID;
            // print(gameObject.name + " triggered by player " + id);
            if (id == 0 && m_player0Triggered == false)
            {
                m_player0Triggered = true;
                Flip();
            }
            else if (id == 1 && m_player1Triggered == false)
            {
                m_player1Triggered = true;
                Flip();
            }
        }
    }

    private void Flip()
    {
        if (m_player0Triggered == true ^ m_player1Triggered == true)
        {
            m_animator.SetBool("First Contact", true);
            print("first contact");
        }
        else if (m_player0Triggered == true && m_player1Triggered == true)
        {
            m_animator.SetBool("Second Contact", true);
            print("second contact");
        }
    }

    private void OnGUI()
    {
        
    }

    #endregion

    #region Private an Protected Members

    private Animator m_animator;

    #endregion
}
