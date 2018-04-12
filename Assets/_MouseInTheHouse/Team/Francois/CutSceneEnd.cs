using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneEnd : MonoBehaviour
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
        Debug.Log("triggerEnd");
        if (m_PlayersLayer.Contains(collision.gameObject.layer))
        {
            MousePlayer m_mouse = collision.gameObject.GetComponent<MousePlayer>();
            if (m_mouse != null)
            {
                if(!m_alreadyTriggeredForTheseID.Contains(m_mouse.PlayerID))
                {
                    m_alreadyTriggeredForTheseID.Add(m_mouse.PlayerID);
                    m_mouse.AllowPlayerInput = true;
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