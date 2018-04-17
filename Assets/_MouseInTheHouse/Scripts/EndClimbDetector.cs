using UnityEngine;

public class EndClimbDetector : MonoBehaviour
{
    [SerializeField] private MousePlayer m_mouse;
    [SerializeField] private LayerMask m_WhatIsBlocking;
    [SerializeField] private BoxCollider2D m_GroundColliderBox;
    [SerializeField] private float m_CastDistance = .05f;
    [SerializeField] private bool m_drawGizmo = true;
    [SerializeField] private bool m_boxCastEnabled = false;

    private RaycastHit2D m_Hit;

    private void Start()
    {
        m_canClimbTheTop = false;
    }

    private void Awake()
    {
        if (m_GroundColliderBox == null) m_GroundColliderBox = GetComponent<BoxCollider2D>();
        if (m_GroundColliderBox == null) Debug.LogError("No collider attached for ground detection.");
    }

    private void OnDrawGizmos()
    {
        if (m_drawGizmo)
        {
            Vector2 position = getPosition();
            position.y -= m_CastDistance;
            if (m_GroundColliderBox != null) Gizmos.DrawCube(position, m_GroundColliderBox.size);
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

            m_Hit = Physics2D.BoxCast(getPosition(), m_GroundColliderBox.bounds.size, 0f, -transform.up, m_CastDistance, m_WhatIsBlocking);

            if (m_Hit.collider == null)
            {
                m_canClimbTheTop = true;
            }
        }
    }

    private Vector2 getPosition()
    {
        return new Vector2(
            m_GroundColliderBox.transform.position.x + (m_mouse.Movement.FacingRight ? m_GroundColliderBox.offset.x : -m_GroundColliderBox.offset.x),
            m_GroundColliderBox.transform.position.y + m_GroundColliderBox.offset.y
        );
    }
}
