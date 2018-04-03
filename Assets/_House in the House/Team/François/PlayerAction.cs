using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] private bool m_debug = true;

    [SerializeField][ReadOnly] private PlayerActionInput m_actionInput;
    public PlayerActionInput ActionInput
    {
        get { return m_actionInput; }
    }

    [Header("Dependencies")]
    [SerializeField] private MousePlayer m_player;
    private Animator m_animator;
    [SerializeField] private CollidersProvider m_colliders;
    [SerializeField] private AnimatorParameterMapper m_animatorParameters;

    #region System
    private void Awake()
    {
        m_actionInput = new PlayerActionInput();
    }

    private void Start()
    {
        if (m_colliders == null && m_debug) Debug.LogError("Reference to \"CollidersProvider\" script is not setted");
        if (m_animatorParameters == null && m_debug) Debug.LogError("Reference to \"AnimatorParameterMapper\" script is not setted");

        if (m_player == null)
        {
            if (m_debug) Debug.LogError("MousePlayer not set!");
        }
        else
        {
            m_animator = m_player.Animator;
        }
    }

    private void FixedUpdate()
    {
        CheckRetry();
        /*
        CheckZoom();
        CheckUnZoom();

        CheckCameraHorizontal();
        CheckCameraVertical();
        */
    }
    #endregion

    #region Checks
    private void CheckRetry()
    {
        if (m_actionInput.Retry)
        {
            Retry();
            m_actionInput.Retry = false;
        }
    }

    private void CheckZoom()
    {

    }
    private void CheckUnZoom()
    {

    }

    private void CheckCameraHorizontal()
    {

    }
    private void CheckCameraVertical()
    {

    }
    #endregion

    #region PlayerActions
    
    public void Nibble()
    {
        if (m_actionInput.Nibble)
        {
            if (m_colliders.CollidingNibbleEdible())
            {
                Debug.Log("Nibble");
            }
            m_actionInput.Nibble = false;
        }
    }

    public void Retry()
    {
        Debug.Log("Retry");
    }

    public void Unzoom()
    {
        Debug.Log("Unzoom");
    }

    public void Zoom()
    {
        Debug.Log("Zoom");
    }

    public void CameraHorizontal()
    {
        Debug.Log("CameraHorizontal");
    }

    public void CameraVertical()
    {
        Debug.Log("CameraVertical");
    }
    #endregion
}
[System.Serializable]
public class PlayerActionInput
{
    [SerializeField]
    private bool m_nibble;
    public bool Nibble
    {
        get { return m_nibble; }
        set { m_nibble = value; }
    }

    [SerializeField]
    private bool m_retry;
    public bool Retry
    {
        get { return m_retry; }
        set { m_retry = value; }
    }

    [SerializeField]
    private float m_unzoom;
    public float Unzoom
    {
        get { return m_unzoom; }
        set { m_unzoom = value; }
    }

    [SerializeField]
    private float m_zoom;
    public float Zoom
    {
        get { return m_zoom; }
        set { m_zoom = value; }
    }

    [SerializeField]
    private float m_cameraHorizontal;
    public float CameraHorizontal
    {
        get { return m_cameraHorizontal; }
        set { m_cameraHorizontal = value; }
    }

    [SerializeField]
    private float m_cameraVertical;
    public float CameraVertical
    {
        get { return m_cameraVertical; }
        set { m_cameraVertical = value; }
    }
}