using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    #region Public Members

    public GameObject m_breakable;

	#endregion

	#region System

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(m_breakable);
    }

    #endregion

}
