using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptDanger : MonoBehaviour
{
    #region Members
    private bool m_active = false;
    private float m_cooldown = 0f;
   // private SpriteRenderer m_spriteRender;
    private Collider2D m_collider;
    
    [SerializeField] private bool m_startAsDanger = true;
    [SerializeField] private bool m_loop = true;
    private bool m_stop = false;

    [SerializeField] private float m_dangerTime = 5f;
    public float DangerTime
    {
        get { return m_dangerTime; }
        set { m_dangerTime = value; }
    }

    [SerializeField] private float m_safeTime = 5f;
    public float SafeTime
    {
        get { return m_safeTime; }
        set { m_safeTime = value; }
    }

    [SerializeField] private Vector2 m_pushForceVelocity = new Vector2(8f, 6f);
    public Vector2 PushBackVelocity
    {
        get { return m_pushForceVelocity; }
    }

    #endregion

    #region Class Methods
    private void Toggle()
    {
        m_active = !m_active;

        if(m_active)
        {
        //    m_spriteRender.color = Color.red;
            m_collider.enabled = true;
        }
        else
        {
         //   m_spriteRender.color = Color.green;
            m_collider.enabled = false;
        }
    }

    public void Activate(bool startAsDangerous, bool loop, float dangerTime, float safeTime)
    {
        m_dangerTime = dangerTime;
        m_safeTime = safeTime;
        m_loop = loop;
        m_active = !startAsDangerous;
        Toggle();
        m_stop = false;
    }
    public void Activate(bool startAsDangerous = true, bool loop = false)
    {
        Activate(startAsDangerous, loop, m_dangerTime, m_safeTime);
    }
    #endregion

    #region System
    void Awake()
    {
        //m_spriteRender = GetComponent<SpriteRenderer>();
        m_collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        if (!m_startAsDanger) m_active = true;
        Toggle();
    }

    void Update()
    {
        if (!m_stop)
        {
            m_cooldown += Time.deltaTime;

            if(m_active)
            {
                if (m_cooldown >= m_dangerTime)
                {
                    Toggle();
                    m_cooldown = 0f;
                    if(!m_loop) m_stop = true;
                }
            }
            else
            {
                if (m_cooldown >= m_safeTime)
                {
                    Toggle();
                    m_cooldown = 0f;
                    if (!m_loop) m_stop = true;
                }
            }
        }
    }  
    #endregion
}