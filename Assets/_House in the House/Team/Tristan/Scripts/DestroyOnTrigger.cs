using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTrigger : MonoBehaviour
{
    #region Public Members

    public GameObject m_door;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(m_door);
    }
    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    #endregion
}
