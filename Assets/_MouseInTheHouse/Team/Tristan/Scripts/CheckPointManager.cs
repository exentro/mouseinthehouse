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
    public bool m_onRetry = false;
    public bool m_gameEnded = false;

    #endregion

    #region Public Function

    #endregion

    #region System

    private void Awake() 
	{
        m_onRetry = CheckPointContainer.OnRetry;
    }

	private void Start() 
	{
        
        if (m_activateCheckpoint)
        {
            Vector3 player0Pos = CheckPointContainer.RespawnPoint;
            Vector3 player1Pos = CheckPointContainer.RespawnPoint;
            player0Pos.x += 1;
            player1Pos.x -= 1;
            m_player0.transform.position = player0Pos;
            m_player1.transform.position = player1Pos;
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
        CheckPointContainer.RespawnPoint = selectedPoint.transform.position;
        CheckPointContainer.OnRetry = true;
        if (m_activateCheckpoint)
        {
            LoadScene();
            Vector3 player0Pos = CheckPointContainer.RespawnPoint;
            Vector3 player1Pos = CheckPointContainer.RespawnPoint;
            Vector3 camPos = CheckPointContainer.RespawnPoint;
            player0Pos.x += 1;
            player1Pos.x -= 1;
            camPos.z -= 10;
            m_player0.transform.position = player0Pos;
            m_player1.transform.position = player1Pos;
            m_camera.transform.position = camPos;
            print("Retry on point " + checkpointNumber);
        }
    }

    public void LoadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void ResetGame()
    {
        if (m_gameEnded)
        {
            ResetCheckPoint();
            m_gameEnded = false;
            LoadScene();
        }
    }

    public void ResetCheckPoint()
    {
        foreach (GameObject point in m_checkpoints)
        {
            CheckPoint checkpoint = point.GetComponent<CheckPoint>();
            checkpoint.m_player0Triggered = false;
            checkpoint.m_player1Triggered = false;
        }
    }

    public void ResetPlayerPos()
    {
        m_player0.transform.position = Vector3.zero;
        m_player1.transform.position = Vector3.zero;
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
    public static bool OnRetry { get; set; }
}