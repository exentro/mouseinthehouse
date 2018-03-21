using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersProvider : MonoBehaviour
{
    [System.Serializable]
    public class ColliderScriptsReferences
    {
        [SerializeField] public GameObject Container; 
        [SerializeField] [ReadOnly] public PushCheckCollision Push;
        [SerializeField] [ReadOnly] public GroundCheckCollision Ground;
        [SerializeField] [ReadOnly] public ClimbCheckCollision Climb;
    }
    [Header("Debug")]
    [SerializeField] bool m_debug = true;

    [Header("State Colliders")]
    [SerializeField] ColliderScriptsReferences idle;
    [SerializeField] ColliderScriptsReferences run;
    [SerializeField] ColliderScriptsReferences push;
    [SerializeField] ColliderScriptsReferences climb;
    [SerializeField] ColliderScriptsReferences jump;
    [SerializeField] ColliderScriptsReferences crouch;

    private ColliderScriptsReferences m_activeColliders;
    private Dictionary<E_MouseState, ColliderScriptsReferences> m_map;

    private void Awake()
    {
        if (idle.Container == null) Debug.LogError("Missing Idle container");
        else
        {
            idle.Ground = idle.Container.GetComponent<GroundCheckCollision>();
            idle.Push = idle.Container.GetComponent<PushCheckCollision>();
            idle.Climb = idle.Container.GetComponent<ClimbCheckCollision>();
        }
        if (run.Container == null) Debug.LogError("Missing Run container");
        else
        {
            run.Ground = run.Container.GetComponent<GroundCheckCollision>();
            run.Push = run.Container.GetComponent<PushCheckCollision>();
            run.Climb = run.Container.GetComponent<ClimbCheckCollision>();
        }
        if (push.Container == null) Debug.LogError("Missing Push container");
        else
        {
            push.Ground = push.Container.GetComponent<GroundCheckCollision>();
            push.Push = push.Container.GetComponent<PushCheckCollision>();
            push.Climb = push.Container.GetComponent<ClimbCheckCollision>();
        }
        if (climb.Container == null) Debug.LogError("Missing Climb container");
        else
        {
            climb.Ground = climb.Container.GetComponent<GroundCheckCollision>();
            climb.Push = climb.Container.GetComponent<PushCheckCollision>();
            climb.Climb = climb.Container.GetComponent<ClimbCheckCollision>();
        }
        if (jump.Container == null) Debug.LogError("Missing Jump container");
        else
        {
            jump.Ground = jump.Container.GetComponent<GroundCheckCollision>();
            jump.Push = jump.Container.GetComponent<PushCheckCollision>();
            jump.Climb = jump.Container.GetComponent<ClimbCheckCollision>();
        }
        if (crouch.Container == null) Debug.LogError("Missing Crouch container");
        else
        {
            crouch.Ground = crouch.Container.GetComponent<GroundCheckCollision>();
            crouch.Push = crouch.Container.GetComponent<PushCheckCollision>();
            crouch.Climb = crouch.Container.GetComponent<ClimbCheckCollision>();
        }
    }

    private void Start()
    {
        m_map = new Dictionary<E_MouseState, ColliderScriptsReferences>();
        m_map.Add(E_MouseState.Idle, idle);
        m_map.Add(E_MouseState.Run, run);
        m_map.Add(E_MouseState.Push, push);
        m_map.Add(E_MouseState.Climb, climb);
        m_map.Add(E_MouseState.Jump, jump);
        m_map.Add(E_MouseState.Crouch, crouch);
    }

    public void DisableCollider(E_MouseState key)
    {
        m_map[key].Container.SetActive(false);
        m_activeColliders = null;
    }
    public void EnableCollider(E_MouseState key)
    {
        m_map[key].Container.SetActive(true);
        m_activeColliders = m_map[key];
    }

    public bool CollidingGround()
    {
        if (m_activeColliders != null)
            return m_activeColliders.Ground.Grounded;
        else if (m_debug) Debug.LogError("No active colliders.");
        return false;
    }
    public bool CollidingPushable()
    {
        if (m_activeColliders != null)
            return m_activeColliders.Push.Pushing;
        else if (m_debug) Debug.LogError("No active colliders.");
        return false;
    }
    public bool CollidingClimbable()
    {
        if (m_activeColliders != null)
            return m_activeColliders.Climb.Climbing;
        else if (m_debug) Debug.LogError("No active colliders.");
        return false;
    }
    public Transform CollidingPushableObjectTransform()
    {
        if (m_activeColliders != null)
            return m_activeColliders.Push.PushableObject;
        else if (m_debug) Debug.LogError("No active colliders.");
        return null;
    }
}
