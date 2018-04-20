using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateBubbleDoNothing : StateMachineBehaviour {



    private MousePlayer m_mousePlayer;
    [SerializeField] bool IsStartBubble;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (m_mousePlayer == null)
            m_mousePlayer = MousePlayer.GetPlayer(animator.GetInteger("PlayerId"));

        if (IsStartBubble)
        {
            m_mousePlayer.BubbleEndAnimator.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            m_mousePlayer.BubbleStartAnimator.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            m_mousePlayer.BubbleEndAnimator.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            m_mousePlayer.BubbleStartAnimator.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	//override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        if (IsStartBubble)
        {
            m_mousePlayer.BubbleStartAnimator.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            m_mousePlayer.BubbleEndAnimator.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            m_mousePlayer.BubbleStartAnimator.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            m_mousePlayer.BubbleEndAnimator.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }

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
