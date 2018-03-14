using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailDisplay : MonoBehaviour
{
    #region Public Members

    public bool m_trail;
    public float m_trailDuration;
    public Color m_trailColor = Color.red;

	#endregion

	#region Public Function

	#endregion

	#region System

	private void Awake() 
	{
		
	}

	private void Start() 
	{
        m_feetCollider = GetComponent<CircleCollider2D>();
        m_prevPos = transform.position;
        m_prevPos.y = m_prevPos.y + m_feetCollider.offset.y - m_feetCollider.radius;
    }

    private void Update()
    {
        if (m_trail)
        {
            Vector3 pos = transform.position;
            pos.y = pos.y + m_feetCollider.offset.y - m_feetCollider.radius;
            Debug.DrawLine(m_prevPos, pos, m_trailColor, m_trailDuration);
            m_prevPos = pos;
        }
    }

    private void FixedUpdate()
	{
		
	}

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    private Vector3 m_prevPos;
    private CircleCollider2D m_feetCollider;

	#endregion
}
