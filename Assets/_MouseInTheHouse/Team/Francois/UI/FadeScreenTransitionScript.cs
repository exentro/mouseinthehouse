using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeScreenTransitionScript : MonoBehaviour
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
    #endregion

    #region System
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
    private void StartFade(bool hide)
    {
        m_transition = hide ? 0f : 1f;
        m_hiding = hide;
        m_inTransition = true;
    }

    public void StartTransition(float FadeHideTimeTransition, function WhatToDoBetween, float FadeUnhideTimeTransition)
    {
        StartCoroutine(WaitThenCall(FadeHideTimeTransition, WhatToDoBetween, FadeUnhideTimeTransition));
        m_fadeDuration = FadeHideTimeTransition;
        StartFade(true);
    }
    public void StartTransition(float FadeHideTimeTransition, function WhatToDoBetween)
    {
        StartTransition(FadeHideTimeTransition, WhatToDoBetween, FadeHideTimeTransition);
    }
    public void StartTransition(float FadeHideTimeTransition, float FadeUnhideTimeTransition)
    {
        StartTransition(FadeHideTimeTransition, null, FadeHideTimeTransition);
    }
    public void StartTransition(float TransitionTime)
    {
        StartTransition(TransitionTime/2, null, TransitionTime/2);
    }
    public void StartTransition(function WhatToDoBetween)
    {
        StartTransition(m_fadeDuration, WhatToDoBetween, m_fadeDuration);
    }

    public delegate void function();
    IEnumerator WaitThenCall(float time, function callback, float FadeOutDuration)
    {
        yield return new WaitForSecondsRealtime(time);
        if(callback != null) callback.Invoke();
        m_fadeDuration = FadeOutDuration;
        StartFade(false);
    }
    #endregion
}