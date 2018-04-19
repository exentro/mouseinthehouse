using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMovementToLeftStart : MonoBehaviour
{
    [SerializeField] private LayerMask m_PlayersLayer;
    [SerializeField] [ReadOnly] List<int> m_alreadyTriggeredForTheseID;

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
                    m_mouse.ForcePlayerToMoveRight = true;
                }
            }
        }
        Vector3 _temp = transform.localScale;
        _temp.x = 150f;
        transform.localScale = _temp;
    }
}