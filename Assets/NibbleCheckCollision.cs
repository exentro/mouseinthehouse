using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NibbleCheckCollision : MonoBehaviour
{
    [SerializeField] private bool m_debug = true;

    private void Start()
    {
        m_edible = false;
    }

    private bool m_edible;
    public bool NibbleEdible
    {
        get { return m_edible; }
    }

    private GameObject m_edibleObject;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != m_edibleObject)
        {
            Interactable coll = collision.gameObject.GetComponent<Interactable>();
            if (coll != null && coll.NibbleEdible)
            {
                if (m_debug && m_edibleObject != null)
                {
                    Debug.LogWarning(string.Format("New Edible object collision detected but another one still referenced. Replacing... Are they 2 edible objects close at the same time ? ({0} & {1})", m_edibleObject.name, collision.gameObject.name));
                }
                m_edible = coll.NibbleEdible;
                if (coll.NibbleEdible) m_edibleObject = collision.gameObject;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_edible = false;
        m_edibleObject = null;
    }
}
