﻿using System.Collections;
using System.Collections.Generic;
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
        if (m_debug && m_MouseAnimator == null) Debug.LogError("Can't find Component Animator for the mouse");
        if (m_debug && m_bubbleStartAnimator == null) Debug.LogError("Can't find Component Animator for the bubble (start)");
        if (m_debug && m_bubbleEndAnimator == null) Debug.LogError("Can't find Component Animator for the bubble (end)");
        if (m_debug && m_Rigidbody2D == null) Debug.LogError("Can't find Component Rigidbody2D");
        if (m_debug && m_transform == null) Debug.LogError("Can't find Component Transform");
        if (m_debug && m_movement == null) Debug.LogError("Can't find Component Movement");
        if (m_debug && m_action == null) Debug.LogError("Can't find Component PlayerAction");
        if (m_debug && m_inputController == null) Debug.LogError("Can't find script \"InputController\"");

        if (m_menuManager == null) m_menuManager = FindObjectOfType<MenuManager>();
        if (m_debug && m_menuManager == null) Debug.LogError("Can't find \"MenuManager\"");

        if (m_MouseAnimator != null)
        {
            m_MouseAnimator.SetInteger("PlayerId", m_data.ID);
        }

        m_bubbleStartAnimator.SetInteger("PlayerId", m_data.ID);
        m_bubbleEndAnimator.SetInteger("PlayerId", m_data.ID);
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

    [SerializeField] private Animator m_MouseAnimator;
    public Animator MouseAnimator
    {
        get { return m_MouseAnimator; }
    }
    [SerializeField] private Animator m_bubbleStartAnimator;
    public Animator BubbleStartAnimator
    {
        get { return m_bubbleStartAnimator; }
    }
    [SerializeField] private Animator m_bubbleEndAnimator;
    public Animator BubbleEndAnimator
    {
        get { return m_bubbleEndAnimator; }
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

    [SerializeField] private MenuManager m_menuManager;
    public MenuManager MenuManager
    {
        get { return m_menuManager; }
    }

    [SerializeField] private CollidersProvider m_collidersProvider;
    public CollidersProvider CollidersProvider
    {
        get { return m_collidersProvider; }
    }

    [SerializeField] private InputController m_inputController;
    /*
    public InputController InputController
    {
        get { return m_inputController; }
    }
    */
    /*
    [SerializeField] private GameObject m_bubbleStartGameObject;
    public GameObject BubbleStartGameObject
    {
        get { return m_bubbleStartGameObject; }
    }
    [SerializeField] private GameObject m_bubbleEndGameObject;
    public GameObject BubbleEndGameObject
    {
        get { return m_bubbleEndGameObject; }
    }
    */
    private IEnumerator m_coroutine;
    public bool ForcePlayerToMoveRight
    {
        set
        {
            if(value)
            {
                m_inputController.AllowPlayerInput = false;
                m_Rigidbody2D.velocity = new Vector2(0f, 0f);

                m_coroutine = MoveToLeftCoRoutine();
                StartCoroutine(m_coroutine);
            }
            else
            {
                m_inputController.AllowPlayerInput = true;

                if(m_coroutine != null) StopCoroutine(m_coroutine);
                m_movement.MovementInput.InputHorizontal = 0f;
            }
        }
    }
    
    private IEnumerator MoveToLeftCoRoutine()
    {
        m_movement.MovementInput.InputHorizontal = 1f;
        yield return null;
    }
}