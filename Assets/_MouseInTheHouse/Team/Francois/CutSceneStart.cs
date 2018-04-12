using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneStart : MonoBehaviour
{
    #region Members
    [SerializeField] private LayerMask m_PlayersLayer;
    [SerializeField] [ReadOnly] List<int> m_alreadyTriggeredForTheseID;
    #endregion

    #region Public Methods
    #endregion

    #region System
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_PlayersLayer.Contains(collision.gameObject.layer))
        {
            MousePlayer m_mouse = collision.gameObject.GetComponentInParent<MousePlayer>();
            if (m_mouse != null)
            {
                Debug.Log("triggerStart - player");
                if (!m_alreadyTriggeredForTheseID.Contains(m_mouse.PlayerID))
                {
                    Debug.Log("triggerStart - player first time");
                    m_alreadyTriggeredForTheseID.Add(m_mouse.PlayerID);
                    m_mouse.AllowPlayerInput = false;
                }
            }
        }
    }
    #endregion

    #region Class Methods
    #endregion

    #region Tools Debug And Utilities
    #endregion


}