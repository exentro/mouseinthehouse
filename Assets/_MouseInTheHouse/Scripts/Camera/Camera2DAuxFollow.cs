﻿using UnityEngine;

public class Camera2DAuxFollow : MonoBehaviour
{
    #region Public Members

    public float m_damping = 0.2f;

    #endregion

    #region Public Function

    #endregion

    #region System

	private void Start() 
	{
        m_OffsetZ = transform.position.z;
    }
	
    public void SmoothLookPoint(Camera camera, Vector3 target, float screenLength, Camera otherCamera, float screenHeight, float minHeight, float maxHeight)
    {
        Vector3 aheadTargetPos = target + Vector3.forward * m_OffsetZ;
        // Vector3 newPos = Vector3.SmoothDamp(camera.transform.position, aheadTargetPos, ref m_currentVelocity, m_damping);
        Vector3 newPos = Vector3.Lerp(camera.transform.position, aheadTargetPos, m_damping * Time.deltaTime);
        if (camera.transform.position.x > otherCamera.transform.position.x)
            newPos.x = Mathf.Max(newPos.x, otherCamera.transform.position.x + screenLength * 0.5f);
        else
            newPos.x = Mathf.Min(newPos.x, otherCamera.transform.position.x - screenLength * 0.5f);
        newPos.y = Mathf.Min(newPos.y, maxHeight - screenHeight * .5f);
        newPos.y = Mathf.Max(newPos.y, minHeight + screenHeight * .5f);
        camera.transform.position = newPos;
    }

    #endregion

    #region Tools Debug And Utility

    #endregion

    #region Private an Protected Members

    private float m_OffsetZ;
    private Vector3 m_currentVelocity;

    #endregion
}
