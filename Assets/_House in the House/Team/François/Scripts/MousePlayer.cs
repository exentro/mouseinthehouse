using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePlayer : MonoBehaviour
{
    private static List<MousePlayer> m_players;
    public static MousePlayer GetPlayer(int id)
    {
        return m_players[id];
    }
    private void Awake()
    {
        if (m_players == null) m_players = new List<MousePlayer>();
        m_players.Add(this);
        m_playerId = m_players.IndexOf(this);

        if (m_Anim == null) m_Anim = GetComponent<Animator>();
        if (m_debug && m_Anim == null) Debug.LogError("Can't find Component Animator");
        if (m_Anim != null)
        {
            m_Anim.SetInteger("PlayerId", m_playerId);
        }

        if (m_Rigidbody2D == null) m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (m_debug && m_Rigidbody2D == null) Debug.LogError("Can't find Component Rigidbody2D");

        if (m_transform == null) m_transform = GetComponent<Transform>();
        if (m_debug && m_transform == null) Debug.LogError("Can't find Component Transform");

        if (m_movement == null) m_movement = GetComponent<Movement>();
        if (m_debug && m_movement == null) Debug.LogError("Can't find Component Movement");
    }

    [SerializeField] private int m_playerId;
    public int PlayerID
    {
        get { return m_playerId; }
    }
    [SerializeField] private bool m_debug = true;

    [Header("Dependencies")]
    [SerializeField] private MousePlayerData m_data;
    public MousePlayerData PlayerData
    {
        get { return m_data; }
        set { m_data = value; }
    }

    [SerializeField] private Animator m_Anim;
    public Animator Animator
    {
        get { return m_Anim; }
    }
    
    [SerializeField] private Rigidbody2D m_Rigidbody2D;
    public Rigidbody2D Rigidbody2D
    {
        get { return m_Rigidbody2D; }
    }

    [SerializeField] private Transform m_transform;
    public Transform Transform
    {
        get { return m_transform; }
    }

    [SerializeField] private Movement m_movement;
    public Movement Movement
    {
        get { return m_movement; }
    }
}
