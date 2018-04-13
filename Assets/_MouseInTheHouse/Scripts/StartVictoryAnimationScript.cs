using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartVictoryAnimationScript : MonoBehaviour
{
    [SerializeField] private LayerMask m_PlayersLayer;
    List<int> m_alreadyTriggeredForTheseID;

    private void Start()
    {
        if (m_alreadyTriggeredForTheseID == null) m_alreadyTriggeredForTheseID = new List<int>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (m_PlayersLayer.Contains(collision.gameObject.layer))
        {
            MousePlayer m_mouse = collision.gameObject.GetComponentInParent<MousePlayer>();
            if (m_mouse != null)
            {
                if (!m_alreadyTriggeredForTheseID.Contains(m_mouse.PlayerID))
                {
                    m_alreadyTriggeredForTheseID.Add(m_mouse.PlayerID);
                    m_mouse.Animator.SetTrigger(m_mouse.AnimatorParameterMapper.VictoryDance);
                }
            }
        }
    }

}