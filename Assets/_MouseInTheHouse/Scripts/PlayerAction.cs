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
    private CollidersProvider m_colliders;
    private AnimatorParameterMapper m_animatorParameters;
    [SerializeField] private CheckPointManager m_checkPointManager;
    private MenuManager m_menuManager;

    private IEnumerator m_nibbleCoroutine;

    #region System
    private void Awake()
    {
        m_actionInput = new PlayerActionInput();
    }

    private void Start()
    {
        if (m_player == null)
        {
            if (m_debug) Debug.LogError("MousePlayer not set!");
        }
        else
        {
            m_animator = m_player.Animator;
            if (m_animator == null && m_debug) Debug.LogError("Reference to Component \"Animator\" is not setted");

            m_animatorParameters = m_player.AnimatorParameterMapper;
            if (m_animatorParameters == null && m_debug) Debug.LogError("Reference to \"AnimatorParameterMapper\" script is not setted");

            m_colliders = m_player.CollidersProvider;
            if (m_colliders == null && m_debug) Debug.LogError("Reference to \"CollidersProvider\" script is not setted");

            m_menuManager = m_player.MenuManager;
            if (m_menuManager == null && m_debug) Debug.LogError("Reference to \"Menu Manager\" script is not setted");
        }

        m_nibbleCoroutine = DoNibbleCoroutine();
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
            if(m_player.PlayerData.CanNibble)
            {
                m_animator.SetTrigger(m_animatorParameters.Nibble);
                if (m_colliders.CollidingNibbleEdible())
                {
                    StartCoroutine(m_nibbleCoroutine);
                }
                m_actionInput.Nibble = false;
            }
        }
    }
    private IEnumerator DoNibbleCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(m_colliders.CollidingNibbleEdibleGameObject());
        m_nibbleCoroutine = DoNibbleCoroutine();
    }

    public void Retry()
    {
        if(m_debug) Debug.Log("Retry");
        if(m_checkPointManager != null) m_checkPointManager.Retry();
        else if (m_debug) Debug.Log("m_checkPointManager is not setted");
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

    #region Menu
    public void MenuEnter()
    {
        if(m_player.PlayerID == 0)
        {
            m_menuManager.MenuIsActive = true;
        }
    }
    public void MenuSelect()
    {
        if (m_player.PlayerID == 0)
        {
            m_menuManager.Execute();
        }
    }
    public void MenuNext()
    {
        if (m_player.PlayerID == 0)
        {
            m_menuManager.Next();
        }
    }
    public void MenuPrevious()
    {
        if (m_player.PlayerID == 0)
        {
            m_menuManager.Previous();
        }
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