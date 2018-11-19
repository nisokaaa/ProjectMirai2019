using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileStart : StateMachineBehaviour {

    public ParticleSystem _Missile;
    public ParticleSystem _MissileCircle;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _Missile = GameObject.Find("Particle System Missile Sub").GetComponent<ParticleSystem>();
        _MissileCircle = GameObject.Find("Particle System Missile").GetComponent<ParticleSystem>();
        _MissileCircle.Play();
        _Missile.Play();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _Missile.Stop();
        _MissileCircle.Stop();
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
