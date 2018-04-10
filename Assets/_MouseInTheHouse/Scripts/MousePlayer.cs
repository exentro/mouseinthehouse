﻿using System.Collections.Generic;
using UnityEngine;

public class MousePlayer : MonoBehaviour
{
    private static MousePlayer[] m_players;
    public static MousePlayer GetPlayer(int id)
    {
        return m_players[id];
    }
    
    private void Awake()
    {
        if (m_debug && m_Animator == null) Debug.LogError("Can't find Component Animator");
        if (m_debug && m_Rigidbody2D == null) Debug.LogError("Can't find Component Rigidbody2D");
        if (m_debug && m_transform == null) Debug.LogError("Can't find Component Transform");
        if (m_debug && m_movement == null) Debug.LogError("Can't find Component Movement");
        if (m_debug && m_action == null) Debug.LogError("Can't find Component PlayerAction");

        if (m_Animator != null)
        {
            m_Animator.SetInteger("PlayerId", m_data.ID);
        }
    }

    private void Start()
    {
        if (m_players == null) m_players = new MousePlayer[2];
        m_players[m_data.ID] = this;
        m_playerId = m_data.ID;
        m_name = m_data.Name;

        if(m_debug) Debug.Log(string.Format("{0} (id:{1}) loaded.", m_name, m_playerId));
    }

    [Header("Debug")]
    [SerializeField] [ReadOnly] private string m_name;
    [SerializeField] [ReadOnly] private int m_playerId;
    public int PlayerID
    {
        get { return m_data.ID; }
    }
    [SerializeField] private bool m_debug = true;

    [Header("Dependencies")]
    [SerializeField] private MousePlayerData m_data;
    public MousePlayerData PlayerData
    {
        get { return m_data; }
        set { m_data = value; }
    }

    [SerializeField] private Animator m_Animator;
    public Animator Animator
    {
        get { return m_Animator; }
    }

    [SerializeField] private AnimatorParameterMapper m_animatorParameterMapper;
    public AnimatorParameterMapper AnimatorParameterMapper
    {
        get { return m_animatorParameterMapper; }
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

    [SerializeField] private PlayerAction m_action;
    public PlayerAction Action
    {
        get { return m_action; }
    }

    [SerializeField] private CollidersProvider m_collidersProvider;
    public CollidersProvider CollidersProvider
    {
        get { return m_collidersProvider; }
    }
}