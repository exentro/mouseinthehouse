using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDanger : MonoBehaviour
{
    #region Members
    private bool m_active;
    private SpriteRenderer m_spriteRender;
    private Collider2D m_collider;

    [SerializeField] private float m_toggleDelay = 5f;
    private float m_cooldown = 0f;

    [SerializeField] private Vector2 m_pushForceVelocity = new Vector2(8f, 6f);
    public Vector2 PushBackVelocity
    {
        get { return m_pushForceVelocity; }
    }

    #endregion

    #region Public Methods
    private void Toggle()
    {
        m_active = !m_active;

        if(m_active)
        {
            m_spriteRender.color = Color.red;
            m_collider.enabled = true;
        }
        else
        {
            m_spriteRender.color = Color.green;
            m_collider.enabled = false;
        }
    }
    #endregion

    #region System

    void Awake()
    {
        m_spriteRender = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        m_active = true;
        Toggle();
    }

    void Update()
    {
        m_cooldown += Time.deltaTime;
        if(m_cooldown >= m_toggleDelay)
        {
            Toggle();
            m_cooldown -= m_toggleDelay;
        }
    }  
    #endregion
}