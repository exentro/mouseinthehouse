using UnityEngine;

public class EndClimbDetector : MonoBehaviour
{
    [SerializeField] private MousePlayer m_mouse;
    [SerializeField] private LayerMask m_WhatIsBlocking;
    [SerializeField] private LayerMask m_LayerClimbable;
    [SerializeField] private BoxCollider2D m_AreaToTeleport;
    [SerializeField] private BoxCollider2D m_topClimbable;
    [SerializeField] private float m_CastDistance = .05f;
    [SerializeField] private bool m_drawGizmo = false;
    [SerializeField] private bool m_boxCastEnabled = false;

    private RaycastHit2D m_HitAreaToTeleport;
    private RaycastHit2D m_HitTopClimbable;

    private void Start()
    {
        m_canClimbTheTop = false;
    }

    private void Awake()
    {
        if (m_AreaToTeleport == null) Debug.LogError("Missing collider.");
        if (m_topClimbable == null) Debug.LogError("Missing collider.");
    }

    private void OnDrawGizmos()
    {
        if (m_drawGizmo && m_boxCastEnabled)
        {
            Vector2 position1 = getPosition(m_AreaToTeleport);
            position1.y -= m_CastDistance;
            if (m_AreaToTeleport != null) Gizmos.DrawCube(position1, m_AreaToTeleport.size);

            Vector2 position2 = getPosition(m_topClimbable);
            position2.y -= m_CastDistance;
            if (m_topClimbable != null) Gizmos.DrawCube(position2, m_topClimbable.size);
        }
    }
    private bool m_canClimbTheTop;
    public bool CanClimbTheTop
    {
        get { return m_canClimbTheTop; }
    }
    private void Update()
    {
        if(m_boxCastEnabled)
        {
            m_canClimbTheTop = false;

            m_HitAreaToTeleport = Physics2D.BoxCast(getPosition(m_AreaToTeleport), m_AreaToTeleport.bounds.size, 0f, -transform.up, m_CastDistance, m_WhatIsBlocking);
            m_HitTopClimbable = Physics2D.BoxCast(getPosition(m_topClimbable), m_topClimbable.bounds.size, 0f, -transform.up, m_CastDistance, m_LayerClimbable);
            
            if (m_HitAreaToTeleport.collider == null)
            {
                if(m_HitTopClimbable.collider != null)
                {
                    Interactable tmp = m_HitTopClimbable.collider.gameObject.GetComponent<Interactable>();
                    if (tmp != null)
                    {
                        if(tmp.Climbable) m_canClimbTheTop = true;
                    }
                }                
            }
        }
    }

    private Vector2 getPosition(BoxCollider2D collider)
    {
        return new Vector2(
            collider.transform.position.x + (m_mouse.Movement.FacingRight ? collider.offset.x : -collider.offset.x),
            collider.transform.position.y + collider.offset.y
        );
    }
}
