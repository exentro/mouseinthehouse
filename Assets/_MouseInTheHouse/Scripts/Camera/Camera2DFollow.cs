using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    #region Public Members

    public Transform m_focusPlayer1;
    public Transform m_focusPlayer2;
    public float m_damping = 0.2f;
    public Camera m_camera2;
    public Camera m_camera3;
    //public AnimationCurve m_animCurve;
    public float m_maxHeight;
    public float m_minHeight;
    public float m_offsetZ = -10f;
    public Vector3 m_onMenuPos;
    public Transform m_endGame;
    public Transform m_gardenCamPos;
    public float m_gardenCamSize;
    public float m_sizeTransitionSpeed;

    #endregion

    private void Start()
    {
        //m_offsetZ = transform.position.z;
        m_screenLength = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        m_screenHeight = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));
        m_camera1 = GetComponent<Camera>();
        m_cameraSize = m_camera1.orthographicSize;
        m_camera2Follow = m_camera2.GetComponent<Camera2DAuxFollow>();
        m_camera3Follow = m_camera3.GetComponent<Camera2DAuxFollow>();
        m_camera2.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
        m_camera3.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
        if(m_onMenu)
            transform.position = m_onMenuPos;
        m_camera1.orthographicSize = 28;
    }

    public void MenuOn()
    {
        m_onMenu = true;
        m_camSavedPos = transform.position;
        transform.position = m_onMenuPos;
        m_camera1.orthographicSize = 28;
        m_camera1.depth = 1;
    }

    public void MenuOff()
    {
        m_onMenu = false;
        m_camera1.orthographicSize = 13.8f;
        if(m_splited)
            m_camera1.depth = -1;
        else
            m_camera1.depth = 1;
        transform.position = m_camSavedPos;
        m_screenLength = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        m_screenHeight = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(0, Screen.height)));
    }

    public void EndCinematic()
    {
        m_camera1.depth = 1;
        m_onGardenTransition = true;
    }

    public void EndGame()
    {
        m_camera1.orthographicSize = m_cameraSize;
        m_onMenu = true;
        m_onGardenTransition = false;
        Vector3 camPos = m_endGame.position;
        camPos.z = m_offsetZ;
        transform.position = camPos;
    }

   
    private void LateUpdate()
    {
        if (m_onGardenTransition)
        {
            t += Time.deltaTime;
            //m_camera1.orthographicSize = Mathf.SmoothStep(m_cameraSize, m_gardenCamSize, t);
            m_camera1.orthographicSize = Mathf.SmoothStep(m_cameraSize, m_gardenCamSize, t * m_sizeTransitionSpeed);

            Vector3 aheadTargetPos = m_gardenCamPos.position + Vector3.forward * m_offsetZ;
            Vector3 newPos = Vector3.Lerp(m_camera1.transform.position, aheadTargetPos, m_damping * Time.deltaTime*.15f);
            m_camera1.transform.position = newPos;
        }
        else if (!m_onMenu)
        {
            float m_deltaX = Math.Abs(m_focusPlayer1.position.x - m_focusPlayer2.position.x);

            if (!m_splited && m_deltaX > m_screenLength * .5)
                SplitScreen();
            else if (m_splited)
            {
                if (m_deltaX < m_screenLength * .5f)
                    MergeScreens();
                else
                    m_merging = false;
            }

            SmoothLookPoint(m_camera1, GetMidPoint(m_focusPlayer1.position, m_focusPlayer2.position));
            if (!m_merging) // 2 players on differents screens
                SmoothLookEachPlayer();
        }

    }

    private void SmoothLookPoint(Camera camera, Vector3 target)
    {
        Vector3 aheadTargetPos = target + Vector3.forward * m_offsetZ;
        Vector3 newPos = Vector3.Lerp(camera.transform.position, aheadTargetPos, m_damping * Time.deltaTime);
        newPos.y = Mathf.Min(newPos.y, m_maxHeight - m_screenHeight * .5f);
        newPos.y = Mathf.Max(newPos.y, m_minHeight + m_screenHeight * .5f);
        camera.transform.position = newPos;
    }

    private Vector2 GetMidPoint(Vector3 point1, Vector3 point2)
    {
        Vector3 target = new Vector3
        {
            x = point1.x + (point2.x - point1.x) * .5f,
            y = point1.y + (point2.y - point1.y) * .5f,
            z = point1.z + (point2.z - point1.z)
        };
        return target;
    }

    private void SmoothLookEachPlayer()
    {   // Camera2 always follow player on the left
        if (m_focusPlayer1.position.x < m_focusPlayer2.position.x)
        {
            m_camera2Follow.SmoothLookPoint(m_camera2, m_focusPlayer1.position, m_screenLength, m_camera3, m_screenHeight, m_minHeight, m_maxHeight);
            m_camera3Follow.SmoothLookPoint(m_camera3, m_focusPlayer2.position, m_screenLength, m_camera2, m_screenHeight, m_minHeight, m_maxHeight);
        }
        else
        {
            m_camera2Follow.SmoothLookPoint(m_camera2, m_focusPlayer2.position, m_screenLength, m_camera3, m_screenHeight, m_minHeight, m_maxHeight);
            m_camera3Follow.SmoothLookPoint(m_camera3, m_focusPlayer1.position, m_screenLength, m_camera2, m_screenHeight, m_minHeight, m_maxHeight);
        }
    }

    private void SplitScreen()
    {
        m_splited = true;
        m_merging = false;
        m_camera2.transform.position = m_camera1.transform.position - new Vector3(m_screenLength * .25f, 0, 0);
        m_camera3.transform.position = m_camera1.transform.position + new Vector3(m_screenLength * .25f, 0, 0);
        m_camera1.depth = -1;
    }

    private void MergeScreens()
    {
        m_merging = true;
        float halfDeltaY = Math.Abs(m_focusPlayer1.position.y - m_focusPlayer2.position.y) * 0.5f;
        float xPos1;
        float xPos2;

        Vector3 player1Merge = new Vector3();
        Vector3 player2Merge = new Vector3();
        float xPos = GetMidPoint(m_focusPlayer1.position, m_focusPlayer2.position).x;
        if (m_focusPlayer1.position.x > m_focusPlayer2.position.x)
        {
            xPos1 = xPos + m_screenLength * .25f;
            xPos2 = xPos - m_screenLength * .25f;
        }
        else
        {
            xPos1 = xPos - m_screenLength * .25f;
            xPos2 = xPos + m_screenLength * .25f;
        }
        if (m_focusPlayer1.position.y > m_focusPlayer2.position.y)
        {
            player1Merge = new Vector3(xPos1, m_focusPlayer1.position.y - halfDeltaY, 0f);
            player2Merge = new Vector3(xPos2, m_focusPlayer2.position.y + halfDeltaY, 0f);
        }
        else
        {
            player1Merge = new Vector3(xPos1, m_focusPlayer1.position.y + halfDeltaY, 0f);
            player2Merge = new Vector3(xPos2, m_focusPlayer2.position.y - halfDeltaY, 0f);
        }

        if (m_focusPlayer1.position.x < m_focusPlayer2.position.x)
        {
            m_camera2Follow.SmoothLookPoint(m_camera2, player1Merge, m_screenLength, m_camera3, m_screenHeight, m_minHeight, m_maxHeight);
            m_camera3Follow.SmoothLookPoint(m_camera3, player2Merge, m_screenLength, m_camera2, m_screenHeight, m_minHeight, m_maxHeight);
        }
        else
        {
            m_camera2Follow.SmoothLookPoint(m_camera2, player2Merge, m_screenLength, m_camera3, m_screenHeight, m_minHeight, m_maxHeight);
            m_camera3Follow.SmoothLookPoint(m_camera3, player1Merge, m_screenLength, m_camera2, m_screenHeight, m_minHeight, m_maxHeight);
        }

        if(Math.Round(m_camera1.transform.position.y, 2) == Math.Round(m_camera2.transform.position.y, 2))
        {
            m_splited = false;
            m_merging = false;
            m_camera1.depth = 1;
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("click"))
        {
            EndCinematic();
        }
    }


    #region Private an Protected Members

    //private float m_offsetZ;
    private Vector3 m_currentVelocity;

    private Camera m_camera1;
    private float m_cameraSize;
    private Camera2DAuxFollow m_camera2Follow;
    private Camera2DAuxFollow m_camera3Follow;
    private float m_screenLength;
    private float m_screenHeight;
    private bool m_splited = false;
    private bool m_merging = false;
    private bool m_onMenu = true;
    private bool m_onGardenTransition = false;
    private Vector3 m_camSavedPos;
    private float t = 0;

    #endregion

}
