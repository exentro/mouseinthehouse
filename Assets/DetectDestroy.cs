using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectDestroy : MonoBehaviour
{
	#region Public Members

	#endregion

	#region Public Function

	#endregion

	#region System

	private void Start() 
	{
        m_children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            print(child);
            m_children.Add(child.gameObject);
        }

    }
	
	private void Update()
	{
        if (!m_destroyed)
        {
            foreach (GameObject child in m_children)
            {
                if(child == null)
                {
                    gameObject.layer = 10;
                    m_destroyed = true;
                }
            }
            if (m_destroyed)
            {
                foreach(Transform child in transform)
                {
                    child.gameObject.layer = 10;
                }
            }
        }


	}

	private void FixedUpdate()
	{
		
	}

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    List<GameObject> m_children;
    bool m_destroyed = false;

	#endregion
}
