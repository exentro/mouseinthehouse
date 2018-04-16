using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BlackScreenScript : MonoBehaviour
{
    #region Members
    [SerializeField] private UIScript m_ui;
    [SerializeField] private Color m_colorTransparent;
    [SerializeField] private Color m_colorOpaque;
    [SerializeField] private Image m_backgroundImage;
    [SerializeField] private float m_fadeDuration = 1f;
    public float FadingDuration
    {
        get { return m_fadeDuration; }
        set { m_fadeDuration = value; }
    }
    private float m_transition;
    private bool m_inTransition;
    private bool m_hiding;
    private GameObject m_gameObject;
    #endregion

    #region System
    private void Awake()
    {
        m_gameObject = gameObject;
    }
    
    private void Update()
    {
        if (!m_inTransition) return;
        if (m_fadeDuration == 0) m_fadeDuration = 1f;

        m_transition += m_hiding ? Time.unscaledDeltaTime * (1 / m_fadeDuration) : -Time.unscaledDeltaTime * (1 / m_fadeDuration);
        m_backgroundImage.color = Color.Lerp(m_colorTransparent, m_colorOpaque, m_transition);

        if (m_transition > 1 || m_transition < 0) m_inTransition = false;
    }
    
    #endregion

    #region Public Methods
    public void StartFade(bool hide)
    {
        m_transition = hide ? 0f : 1f;
        m_hiding = hide;
        m_inTransition = true;
    }
    #endregion
}