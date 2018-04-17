using UnityEngine;

public class CeilingCheckCollision : MonoBehaviour
{
    [SerializeField] private MousePlayer m_mouse;
    [SerializeField] private LayerMask m_WhatIsBlocking;
    [SerializeField] private CircleCollider2D m_GroundColliderCircle;
    [SerializeField] private float m_CastDistance = .05f;
    [SerializeField] private bool m_DraxwGizmo = true;

    private RaycastHit2D m_Hit;

    private bool m_touchingCeiling;
    public bool TouchingCeiling
    {
        get { return m_touchingCeiling; }
    }

    private void Awake()
    {
        if (m_GroundColliderCircle == null) m_GroundColliderCircle = GetComponent<CircleCollider2D>();
        if (m_GroundColliderCircle == null) Debug.LogError("No collider attached for ground detection.");
    }

    private void Start()
    {
        m_touchingCeiling = false;
    }

    private void OnDrawGizmos()
    {
        if (m_DraxwGizmo)
        {
            Vector2 position = getPosition();
            position.y += m_CastDistance;
            if (m_GroundColliderCircle != null) Gizmos.DrawWireSphere(position, m_GroundColliderCircle.radius);
        }
    }

    private void Update()
    {
        m_touchingCeiling = false;
        m_Hit = m_Hit = Physics2D.CircleCast(getPosition(), m_GroundColliderCircle.radius, -transform.up, m_CastDistance, m_WhatIsBlocking);

        if (m_Hit.collider != null)
        {
            m_touchingCeiling = true;
        }
    }

    private Vector2 getPosition()
    {
        return new Vector2(
            m_GroundColliderCircle.transform.position.x + (m_mouse.Movement.FacingRight ? m_GroundColliderCircle.offset.x : -m_GroundColliderCircle.offset.x),
            m_GroundColliderCircle.transform.position.y + m_GroundColliderCircle.offset.y
        );
    }
}
