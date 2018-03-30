using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    #region Public Members

    public BoxCollider2D[] m_checkpoints;

	#endregion

	#region Public Function

	#endregion

	#region System

	private void Awake() 
	{
		
	}

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
        print("trig");
        foreach(BoxCollider2D checkPoint in m_checkpoints)
        {
            if(collision.gameObject == checkPoint.gameObject)
            {
                print("Check point triggered");
            }
        }
    }

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    #endregion
}
