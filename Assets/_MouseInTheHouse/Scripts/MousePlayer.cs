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
        if (m_Anim == null) m_Anim = GetComponent<Animator>();
        if (m_debug && m_Anim == null) Debug.LogError("Can't find Component Animator");
        if (m_Anim != null)
        {
            m_Anim.SetInteger("PlayerId", m_data.ID);
        }

        if (m_Rigidbody2D == null) m_Rigidbody2D = GetComponent<Rigidbody2D>();
        if (m_debug && m_Rigidbody2D == null) Debug.LogError("Can't find Component Rigidbody2D");

        if (m_transform == null) m_transform = GetComponent<Transform>();
        if (m_debug && m_transform == null) Debug.LogError("Can't find Component Transform");

        if (m_movement == null) m_movement = GetComponent<Movement>();
        if (m_debug && m_movement == null) Debug.LogError("Can't find Component Movement");

        if (m_action == null) m_action = GetComponent<PlayerAction>();
        if (m_debug && m_action == null) Debug.LogError("Can't find Component PlayerAction");
    }

    private void Start()
    {
        if (m_players == null) m_players = new MousePlayer[2];
        m_players[m_data.ID] = this;
        m_playerId = m_data.ID;
        m_name = m_data.Name;
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

    [SerializeField] private PlayerAction m_action;
    public PlayerAction Action
    {
        get { return m_action; }
    }
}