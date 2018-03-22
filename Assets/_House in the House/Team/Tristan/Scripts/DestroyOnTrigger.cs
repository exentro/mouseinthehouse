using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    #region Public Members

    public GameObject m_door;

	#endregion

	#region System

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(m_door);
    }

    #endregion

}
