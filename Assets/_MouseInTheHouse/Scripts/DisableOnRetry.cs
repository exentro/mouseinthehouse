using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnRetry : MonoBehaviour
{
    #region Public Members

    public CheckPointManager m_checkPointManager;

	#endregion

	#region Public Function

	#endregion

	#region System


	private void Start() 
	{
        if (m_checkPointManager.m_onRetry)
        {
            BoxCollider2D[] boxes;

            boxes = GetComponentsInChildren<BoxCollider2D>();
            foreach(BoxCollider2D box in boxes)
            {
                box.isTrigger = false;
                box.enabled = false;
                Destroy(box);
            }
        }
	}
	
	private void Update()
	{
		
	}

	private void FixedUpdate()
	{
		
	}

	#endregion

	#region Tools Debug And Utility

	#endregion

	#region Private an Protected Members

	#endregion
}
