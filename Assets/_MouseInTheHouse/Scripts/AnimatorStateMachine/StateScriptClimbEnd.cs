using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateScriptClimbEnd : StateMachineBehaviour {

    [SerializeField] Vector2 TeleportationDelta;

    private MousePlayer m_mousePlayer;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (m_mousePlayer == null)
            m_mousePlayer = MousePlayer.GetPlayer(animator.GetInteger("PlayerId"));
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool(m_mousePlayer.AnimatorParameterMapper.Climb, false);

        Vector3 position = m_mousePlayer.Transform.position;
        position.x += m_mousePlayer.Movement.FacingRight ? TeleportationDelta.x : -TeleportationDelta.x;
        position.y += TeleportationDelta.y;
        m_mousePlayer.Transform.position = position;
    }

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
