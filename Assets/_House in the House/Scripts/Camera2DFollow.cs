using System;
using UnityEngine;

namespace UnityStandardAssets._2D
{
    public class Camera2DFollow : MonoBehaviour
    {
        public Transform target1;
        public Transform target2;
        public float damping = 1;
        public float lookAheadFactor = 3;
        public float lookAheadReturnSpeed = 0.5f;
        public float lookAheadMoveThreshold = 0.1f;

        private float m_OffsetZ;
        private Vector3 m_LastTargetPosition;
        private Vector3 m_CurrentVelocity;
        private Vector3 m_LookAheadPos;
        private Vector3 m_midPoint;

        // Use this for initialization
        private void Start()
        {
            m_LastTargetPosition = m_midPoint;
            m_OffsetZ = (transform.position - m_midPoint).z;
            transform.parent = null;
        }


        // Update is called once per frame
        private void Update()
        {
            GetMidPoint();
            // only update lookahead pos if accelerating or changed direction
            float xMoveDelta = (m_midPoint - m_LastTargetPosition).x;

            bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

            if (updateLookAheadTarget)
            {
                m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
            }
            else
            {
                m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
            }

            Vector3 aheadTargetPos = m_midPoint + m_LookAheadPos + Vector3.forward*m_OffsetZ;
            Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

            transform.position = newPos;

            m_LastTargetPosition = m_midPoint;
        }

        private void GetMidPoint()
        {
            m_midPoint.x = target1.position.x + (target2.position.x - target1.position.x) * .5f;
            m_midPoint.y = target1.position.y + (target2.position.y - target1.position.y) * .5f;
        }
    }
}
