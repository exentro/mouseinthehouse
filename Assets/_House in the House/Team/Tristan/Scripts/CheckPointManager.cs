using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    #region Public Members

    public GameObject[] m_checkpoints;
    public GameObject m_player0;
    public GameObject m_player1;

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

    public void Retry()
    {
        GameObject selectedPoint = m_checkpoints[0];
        int checkpointNumber = 0;
        foreach(GameObject point in m_checkpoints)
        {
            CheckPoint checkpoint = point.GetComponent<CheckPoint>();
            if(checkpoint.m_player0Triggered == true && checkpoint.m_player1Triggered == true)
            {
                if(checkpoint.m_checkpointNumber > checkpointNumber)
                {
                    selectedPoint = point;
                    checkpointNumber = checkpoint.m_checkpointNumber;
                }
            }
        }
        //TODO: Go to selectedPoint
        CheckPointContainer.RespawnPoint = selectedPoint.transform.position;
        print("Retry on point " + checkpointNumber);
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Retry"))
            Retry();
    }

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    #endregion
}

public static class CheckPointContainer
{
    public static Vector3 RespawnPoint { get; set; }
}