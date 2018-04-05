using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint: MonoBehaviour
{
    #region Public Members

    public int m_checkpointNumber;
    public bool m_player0Triggered;
    public bool m_player1Triggered;

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
        int id = collision.gameObject.GetComponentInParent<MousePlayer>().PlayerID;
        // print(gameObject.name + " triggered by player " + id);
        if (id == 0)
            m_player0Triggered = true;
        else if (id == 1)
            m_player1Triggered = true;
    }

    private void OnGUI()
    {
        
    }

    #endregion

    #region Private an Protected Members

    #endregion
}
