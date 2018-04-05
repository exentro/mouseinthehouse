using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        print(CheckPointContainer.RespawnPoint);
        //m_player0.transform.position = CheckPointContainer.RespawnPoint;
        //m_player1.transform.position = CheckPointContainer.RespawnPoint;
    }
	
	private void Update()
	{
		
	}

	private void FixedUpdate()
	{
		
	}

    public void Retry()
    {
        GameObject selectedPoint = null;
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
        SceneManager.LoadScene(0);
        //m_player0.transform.position = CheckPointContainer.RespawnPoint;
        //m_player1.transform.position = CheckPointContainer.RespawnPoint;
        //print("Retry on point " + checkpointNumber);
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