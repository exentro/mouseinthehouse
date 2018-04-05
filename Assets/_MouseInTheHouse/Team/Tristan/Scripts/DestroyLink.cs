using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyLink : MonoBehaviour
{
	#region Public Members

    public HingeJoint2D[] m_hingeJointList;

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
        print("Link collision");
        JointAngleLimits2D limits;
        for (int i = 0; i < m_hingeJointList.Length; i++)
            {
            limits = m_hingeJointList[i].limits;
            limits.min = -90;
            limits.max = 90;
            m_hingeJointList[i].limits = limits;
        }
        Destroy(gameObject);
    }

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    #endregion
}
