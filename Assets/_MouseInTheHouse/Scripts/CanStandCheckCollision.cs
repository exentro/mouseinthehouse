using UnityEngine;

public class CanStandCheckCollision : MonoBehaviour
{
    [SerializeField] private LayerMask m_LayersToIgnore;

    private bool m_canStand;

    public bool CanStandUp
    {
        get { return !m_canStand; }
        set { m_canStand = !value; }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!m_LayersToIgnore.Contains(collision.gameObject.layer))
        {
            m_canStand = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        m_canStand = false;
    }
}

