using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start2 : StateMachineBehaviour {
    Animator animator;
    BossBattlePlayerStartPosition bossBattlePlayerStartPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        animator = GameObject.Find("Boss").GetComponent<Animator>();
        bossBattlePlayerStartPosition = GameObject.Find("BossBattlePlayerStartPosition").GetComponent<BossBattlePlayerStartPosition>();
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    if (animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
      {
          animator.SetTrigger("BossBattle");
      }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        bossBattlePlayerStartPosition.SetPlayerStart();
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
