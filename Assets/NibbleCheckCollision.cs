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
    public GameObject EdibleObject
    {
        get
        {
            if (m_edibleObject == null) return null;
            return m_edibleObject;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject != m_edibleObject)
        {
            Interactable coll = collision.gameObject.GetComponent<Interactable>();
            if (coll != null && coll.NibbleEdible)
            {
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
