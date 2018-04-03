using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollidersProvider : MonoBehaviour
{
    [System.Serializable]
    public class ColliderScriptsReferences
    {
        [SerializeField] public GameObject Container; 
        [SerializeField] [ReadOnlyOnPlay] public PushCheckCollision Push;
        [SerializeField] [ReadOnlyOnPlay] public GroundCheckCollision Ground;
        [SerializeField] [ReadOnlyOnPlay] public ClimbCheckCollision Climb;
        [SerializeField] [ReadOnlyOnPlay] public NibbleCheckCollision Nibble;
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
            idle.Ground = idle.Container.GetComponentInChildren<GroundCheckCollision>();
            idle.Push = idle.Container.GetComponentInChildren<PushCheckCollision>();
            idle.Climb = idle.Container.GetComponentInChildren<ClimbCheckCollision>();
            idle.Nibble = idle.Container.GetComponentInChildren<NibbleCheckCollision>();
        }
        if (run.Container == null) Debug.LogError("Missing Run container");
        else
        {
            run.Ground = run.Container.GetComponentInChildren<GroundCheckCollision>();
            run.Push = run.Container.GetComponentInChildren<PushCheckCollision>();
            run.Climb = run.Container.GetComponentInChildren<ClimbCheckCollision>();
            run.Nibble = run.Container.GetComponentInChildren<NibbleCheckCollision>();
        }
        if (push.Container == null) Debug.LogError("Missing Push container");
        else
        {
            push.Ground = push.Container.GetComponentInChildren<GroundCheckCollision>();
            push.Push = push.Container.GetComponentInChildren<PushCheckCollision>();
            push.Climb = push.Container.GetComponentInChildren<ClimbCheckCollision>();
            push.Nibble = null;
        }
        if (climb.Container == null) Debug.LogError("Missing Climb container");
        else
        {
            climb.Ground = climb.Container.GetComponentInChildren<GroundCheckCollision>();
            climb.Push = climb.Container.GetComponentInChildren<PushCheckCollision>();
            climb.Climb = climb.Container.GetComponentInChildren<ClimbCheckCollision>();
            climb.Nibble = null;
        }
        if (jump.Container == null) Debug.LogError("Missing Jump container");
        else
        {
            jump.Ground = jump.Container.GetComponentInChildren<GroundCheckCollision>();
            jump.Push = jump.Container.GetComponentInChildren<PushCheckCollision>();
            jump.Climb = jump.Container.GetComponentInChildren<ClimbCheckCollision>();
            jump.Nibble = null;
        }
        if (crouch.Container == null) Debug.LogError("Missing Crouch container");
        else
        {
            crouch.Ground = crouch.Container.GetComponentInChildren<GroundCheckCollision>();
            crouch.Push = crouch.Container.GetComponentInChildren<PushCheckCollision>();
            crouch.Climb = crouch.Container.GetComponentInChildren<ClimbCheckCollision>();
            crouch.Nibble = crouch.Container.GetComponentInChildren<NibbleCheckCollision>();
        }
    }

    private void Start()
    {
        m_map = new Dictionary<E_MouseState, ColliderScriptsReferences>
        {
            { E_MouseState.Idle, idle },
            { E_MouseState.Run, run },
            { E_MouseState.Push, push },
            { E_MouseState.Climb, climb },
            { E_MouseState.Jump, jump },
            { E_MouseState.Crouch, crouch }
        };
        
        m_activeColliders = m_map[E_MouseState.Idle];
    }
    public void EnableCollider(E_MouseState key)
    {
        if (m_activeColliders != m_map[key])
        {
            m_activeColliders.Container.SetActive(false);
            m_activeColliders = m_map[key];
            m_activeColliders.Container.SetActive(true);
        }
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
    public bool CollidingNibbleEdible()
    {
        if (m_activeColliders != null)
        {
            if (m_activeColliders.Nibble != null)
            {
                return m_activeColliders.Nibble.NibbleEdible;
            }
        }
        else
        {
            if (m_debug) Debug.LogError("No active colliders.");
        }
        return false;
    }
}
