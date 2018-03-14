using System;
using UnityEngine;

public class Camera2DFollow : MonoBehaviour
{
    public Transform player1;
    public Transform player2;
    public float damping = 1;
    public float lookAheadFactor = 3;
    public float lookAheadReturnSpeed = 0.5f;
    public float lookAheadMoveThreshold = 0.1f;
    public Camera m_camera2;  

    private float m_OffsetZ;
    private Vector3 m_LastTargetPosition;
    private Vector3 m_CurrentVelocity;
    private Vector3 m_LookAheadPos;
    private Vector3 m_midPoint;

    private Camera m_camera1;
    private float m_screenLength;
    private float m_PlayerDistX;
    private bool m_splited;
    private bool m_merging;

    // Use this for initialization
    private void Start()
    {
        m_LastTargetPosition = m_midPoint;
        m_OffsetZ = (transform.position - m_midPoint).z;
        transform.parent = null;
        m_screenLength = Vector2.Distance(Camera.main.ScreenToWorldPoint(new Vector2(0, 0)), Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, 0)));
        m_splited = false;
        m_merging = false;
        m_camera1 = GetComponent<Camera>();
    }


    // Update is called once per frame
    private void Update()
    {
        m_PlayerDistX = Math.Abs(player1.position.x - player2.position.x);
        if(!m_splited && m_PlayerDistX > m_screenLength * .5)
        {   // Split Screen
            m_camera2.transform.position = m_camera1.transform.position;
            m_camera1.rect = new Rect(0.0f, 0.0f, 0.5f, 1.0f);
            m_camera2.rect = new Rect(0.5f, 0.0f, 0.5f, 1.0f);
            if (player1.position.x < player2.position.x)
            {
                m_camera1.transform.position = new Vector3(player1.position.x, m_camera1.transform.position.y, m_camera1.transform.position.z);
                m_camera2.transform.position = new Vector3(player2.position.x, m_camera2.transform.position.y, m_camera2.transform.position.z);
            }
            else
            {
                m_camera1.transform.position = new Vector3(player2.position.x, m_camera1.transform.position.y, m_camera1.transform.position.z);
                m_camera2.transform.position = new Vector3(player1.position.x, m_camera2.transform.position.y, m_camera2.transform.position.z);
            }
            m_merging = false;
            m_splited = true;
        }
        else if (m_splited && m_PlayerDistX < m_screenLength * .5)
        {   // Merge Screens
            m_merging = true;
            MergeScreens();
        }

        if (!m_splited) SmoothLookPoint(m_camera1, GetMidPoint());
        else if (!m_merging) SmoothLookEachPlayer();
        // Debug.DrawLine(target1.position, target2.position, Color.blue, 0);
    }

    private void SmoothLookPoint(Camera camera, Vector3 target)
    {
        // only update lookahead pos if accelerating or changed direction
        float xMoveDelta = (target - m_LastTargetPosition).x;

        bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

        if (updateLookAheadTarget)
        {
            m_LookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
        }
        else
        {
            m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
        }

        Vector3 aheadTargetPos = target + m_LookAheadPos + Vector3.forward * m_OffsetZ;
        Vector3 newPos = Vector3.SmoothDamp(camera.transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

        camera.transform.position = newPos;

        m_LastTargetPosition = target;
    }

    private Vector2 GetMidPoint()
    {
        Vector3 target = new Vector3
        {
            x = player1.position.x + (player2.position.x - player1.position.x) * .5f,
            y = player1.position.y + (player2.position.y - player1.position.y) * .5f,
            z = 0f
        };
        return target;
    }

    private void SmoothLookEachPlayer()
    {
        if (player1.position.x < player2.position.x)
        {
            SmoothLookPoint(m_camera1, player1.position);
            SmoothLookPoint(m_camera2, player2.position);
        }
        else
        {
            SmoothLookPoint(m_camera1, player2.position);
            SmoothLookPoint(m_camera2, player1.position);
        }
    }

    private void MergeScreens()
    {
        float midVerticalDist = Math.Abs(player1.position.y - player2.position.y) *.5f;

        Vector3 player1Merge = new Vector3();
        Vector3 player2Merge = new Vector3();
        if (player1.position.y > player2.position.y)
        {
            player1Merge = new Vector3(player1.position.x, player1.position.y - midVerticalDist, 0f);
            player2Merge = new Vector3(player2.position.x, player2.position.y + midVerticalDist, 0f);
        }
        else
        {
            player1Merge = new Vector3(player1.position.x, player1.position.y + midVerticalDist, 0f);
            player2Merge = new Vector3(player2.position.x, player2.position.y - midVerticalDist, 0f);
        }

        if (player1.position.x < player2.position.x)
        {
            SmoothLookPoint(m_camera1, player1Merge);
            SmoothLookPoint(m_camera2, player2Merge);
        }
        else
        {
            SmoothLookPoint(m_camera1, player2Merge);
            SmoothLookPoint(m_camera2, player1Merge);
        }


        print(midVerticalDist);
        print(player1Merge.y);
        print(player1.position.y);
        print(player2Merge.y);
        print(player2.position.y);
        //if (Math.Round(player1Merge.y, 3) == Math.Round(m_camera1.transform.position.y, 3) && Math.Round(player2Merge.y, 3) == Math.Round(player2.position.y, 3))
        if(Math.Round(m_camera1.transform.position.y, 1) == Math.Round(m_camera2.transform.position.y, 1))
        {



            m_camera1.rect = new Rect(0.0f, 0.0f, 1.0f, 1.0f);
            m_camera2.rect = new Rect(0.0f, 0.0f, 0.0f, 0.0f);
            Vector3 midPoint = GetMidPoint();
            m_camera1.transform.position = new Vector3(midPoint.x, midPoint.y, m_camera1.transform.position.z);
            m_splited = false;
            m_merging = false;
        }
    }

    private void OnGUI()
    {
        GUILayout.Button(m_PlayerDistX + "");
    }
}
