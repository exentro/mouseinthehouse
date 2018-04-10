using UnityEngine;

public class EndClimbDetector : MonoBehaviour
{
    private void Start()
    {
        m_climbableDetection = false;
    }

    private bool m_climbableDetection;
    public bool EndOfClimb
    {
        get { return m_climbableDetection; }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Interactable coll = collision.gameObject.GetComponent<Interactable>();
        if (coll != null)
        {
            m_climbableDetection = coll.Climbable;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_climbableDetection = false;
    }
}
