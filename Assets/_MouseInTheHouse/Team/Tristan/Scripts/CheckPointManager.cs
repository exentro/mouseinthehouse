using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPointManager : MonoBehaviour
{
    #region Public Members

    public bool m_activateCheckpoint;
    public GameObject[] m_checkpoints;
    public GameObject m_player0;
    public GameObject m_player1;
    public Camera m_camera;

    #endregion

    #region Public Function

    #endregion

    #region System

    private void Awake() 
	{
		
	}

	private void Start() 
	{
        if (m_activateCheckpoint)
        {
            
            m_player0.transform.position = CheckPointContainer.RespawnPoint;
            m_player1.transform.position = CheckPointContainer.RespawnPoint;
            m_camera.transform.position = CheckPointContainer.RespawnPoint;
        }
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
        print(selectedPoint.transform.position);
        //TODO: Go to selectedPoint
        CheckPointContainer.RespawnPoint = selectedPoint.transform.position;
        if (m_activateCheckpoint)
        {
            //Vector3 cameraPos = m_camera.transform.position;
            SceneManager.LoadScene(0);
            m_player0.transform.position = CheckPointContainer.RespawnPoint;
            m_player1.transform.position = CheckPointContainer.RespawnPoint;
            m_camera.transform.position = CheckPointContainer.RespawnPoint;
            print("Retry on point " + checkpointNumber);
        }
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