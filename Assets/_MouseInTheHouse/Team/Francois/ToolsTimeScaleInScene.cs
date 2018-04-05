using UnityEngine;

public class ToolsTimeScaleInScene : MonoBehaviour
{
    [SerializeField] float m_timeScale = 1f;
    private float previousValue;

	void Start ()
    {
        previousValue = Time.timeScale;
        Time.timeScale = m_timeScale;
    }

    private void OnDisable()
    {
        Time.timeScale = previousValue;
    }
}
