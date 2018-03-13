using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollider : MonoBehaviour
{
    #region Public Members

    public float colDepth = 4f;
    public float zPosition = 0f;
    public BoxCollider2D m_cameraEdge;

    #endregion

    #region Public Function

    #endregion

    #region System

	private void Start() 
	{
        SetEdgeCollider();
	}
	
    #endregion

    #region Class Methods

    private void SetEdgeCollider()
    {
        // Generate our empty objects
        topCollider = Instantiate(m_cameraEdge, transform);
        bottomCollider = Instantiate(m_cameraEdge, transform);
        rightCollider = Instantiate(m_cameraEdge, transform);
        leftCollider = Instantiate(m_cameraEdge, transform);

        // Generate world space point information for position and scale calculations
        cameraPos = Camera.main.transform.position;
        screenSize.x = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0))) * 0.5f;
        screenSize.y = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height))) * 0.5f;

        // Change our scale and positions to match the edges of the screen...   
        rightCollider.transform.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        rightCollider.transform.position = new Vector3(cameraPos.x + screenSize.x + (rightCollider.transform.localScale.x * 0.5f), cameraPos.y, zPosition);
        leftCollider.transform.localScale = new Vector3(colDepth, screenSize.y * 2, colDepth);
        leftCollider.transform.position = new Vector3(cameraPos.x - screenSize.x - (leftCollider.transform.localScale.x * 0.5f), cameraPos.y, zPosition);
        topCollider.transform.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        topCollider.transform.position = new Vector3(cameraPos.x, cameraPos.y + screenSize.y + (topCollider.transform.localScale.y * 0.5f), zPosition);
        bottomCollider.transform.localScale = new Vector3(screenSize.x * 2, colDepth, colDepth);
        bottomCollider.transform.position = new Vector3(cameraPos.x, cameraPos.y - screenSize.y - (bottomCollider.transform.localScale.y * 0.5f), zPosition);
    }
    
    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    private Vector2 screenSize;
    private BoxCollider2D topCollider;
    private BoxCollider2D bottomCollider;
    private BoxCollider2D leftCollider;
    private BoxCollider2D rightCollider;
    private Vector3 cameraPos;

    #endregion
}
