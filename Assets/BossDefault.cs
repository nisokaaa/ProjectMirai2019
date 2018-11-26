using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDefault : StateMachineBehaviour {
    public ParticleSystem _Beam;
    public int time;
    public int BeamStartTime;
    public int TackleStartTime;
    public int MissileStartTime;
    public bool bActive;
    public enum BOSS_PHASE
    {
        PHASE_NONE = 0,
        PHASE_BEAM,
        PHASE_TACKLE,
        PHASE_MISSILE,
    };
    public BOSS_PHASE _fhase = BOSS_PHASE.PHASE_NONE;

    Animator animator;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        _Beam = GameObject.Find("Particle System Beam").GetComponent<ParticleSystem>();
        animator = GameObject.Find("BossModelAnimatorController").GetComponent<Animator>();
        _Beam.Stop();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
        //_Beam.Stop();
        if(bActive == false)
        {
            return;
        }
        time++;
        if(BeamStartTime < time && _fhase == BOSS_PHASE.PHASE_BEAM)
        {
            animator.SetTrigger("Beam");
            _fhase = BOSS_PHASE.PHASE_TACKLE;
        }
        if (TackleStartTime < time && _fhase == BOSS_PHASE.PHASE_TACKLE)
        {
            animator.SetTrigger("Rush");
            _fhase = BOSS_PHASE.PHASE_MISSILE;
        }
        if (MissileStartTime < time && _fhase == BOSS_PHASE.PHASE_MISSILE)
        {
            time = 0;
            animator.SetTrigger("Missile");
            _fhase = BOSS_PHASE.PHASE_BEAM;
        }
    }

    public void SetBossBattleStart()
    {
        _fhase = BOSS_PHASE.PHASE_BEAM;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
    //
    //}
}
