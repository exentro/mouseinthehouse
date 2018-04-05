using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHinge : MonoBehaviour
{
    #region Public Members

    public Collider2D m_triggerZone;
    public GameObject m_objectToDestroy;

	#endregion

	#region Public Function

	#endregion

	#region System

	private void Start() 
	{
		
	}
	
	private void Update()
	{
		
	}

	private void FixedUpdate()
	{
		
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        m_objectNumber++;
        print(m_objectNumber + "");
        if(m_objectNumber == 2)
        {
            m_objectToDestroy.GetComponent<HingeJoint2D>().enabled = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_objectNumber--;
    }

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    private int m_objectNumber = 0;

    #endregion
}
