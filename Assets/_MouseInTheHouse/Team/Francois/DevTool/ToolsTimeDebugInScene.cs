using UnityEngine;

public class ToolsTimeDebugInScene : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField] bool m_pauseAtFirstFrame = false;
    [SerializeField] bool m_pauseAtSecondFrame = false;
    private bool m_firstFrameSpent = false;
    private bool m_secondFrameSpent = false;

    [SerializeField] bool m_pauseAtFirstFixedUpdate = false;
    [SerializeField] bool m_pauseAtSecondFixedUpdatee = false;
    private bool m_firstFixedUpdateSpent = false;
    private bool m_secondFixedUpdateSpent = false;

    [SerializeField] float m_timeScale = 1f;
    private float m_previousTimeScale;

    private int frameCount = 0;

	void Start ()
    {
        m_previousTimeScale = Time.timeScale;
        Time.timeScale = m_timeScale;
    }

    private void Update()
    {
        frameCount++;

        if (!m_firstFrameSpent && m_pauseAtFirstFrame) UnityEditor.EditorApplication.isPaused = true;
        m_firstFrameSpent = true;

        if (m_pauseAtSecondFrame && !m_secondFrameSpent && m_firstFrameSpent)
        {
            m_secondFrameSpent = true;
            UnityEditor.EditorApplication.isPaused = true;
        }
    }

    private void FixedUpdate()
    {
        if (!m_firstFixedUpdateSpent && m_pauseAtFirstFixedUpdate) UnityEditor.EditorApplication.isPaused = true;
        m_firstFixedUpdateSpent = true;

        if (m_pauseAtSecondFixedUpdatee && !m_secondFixedUpdateSpent && m_firstFixedUpdateSpent)
        {
            m_secondFixedUpdateSpent = true;
            UnityEditor.EditorApplication.isPaused = true;
        }
    }

    private void OnDisable()
    {
        Time.timeScale = m_previousTimeScale;
    }

    private void OnGUI()
    {
        GUI.Button(new Rect(0, 0, 70, 30), "frame:" + frameCount.ToString());
    }
#endif
}
