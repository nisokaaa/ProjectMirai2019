using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : MonoBehaviour {

    public int time;
    public int BeamStartTime;
    public int MissileStartTime;
    public int TackleStartTime;
    public bool bActive = false;
    [SerializeField] MissileAttack _Missile;


    public enum BOSS_PHASE
    {
        PHASE_NONE = 0,
        PHASE_BEAM,
        PHASE_TACKLE,
        PHASE_MISSILE,
    };
    public BOSS_PHASE _fhase = BOSS_PHASE.PHASE_NONE;

    Animator _animator;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void Update () {
        //_Beam.Stop();
        if (bActive == false)
        {
            return;
        }
        time++;

        ////ステートの切り替え
        //switch(_fhase)
        //{
        //    //100カウントしたらビーム処理に遷移する
        //    case BOSS_PHASE.PHASE_NONE:
        //        if(time > 100)
        //        {
        //            time = 0;
        //            _fhase = BOSS_PHASE.PHASE_BEAM;
        //        }
        //        break;
        //
        //    case BOSS_PHASE.PHASE_BEAM:
        //        //_animator.SetTrigger("Beam");
        //        _fhase = BOSS_PHASE.PHASE_MISSILE;
        //        break;
        //
        //    case BOSS_PHASE.PHASE_MISSILE:
        //        break;
        //
        //    case BOSS_PHASE.PHASE_TACKLE:
        //        break;
        //
        //}

        //ステートの実行処理
       // if (BeamStartTime < time && _fhase == BOSS_PHASE.PHASE_BEAM)
       // {
       //     _animator.SetTrigger("Beam");
       //     _fhase = BOSS_PHASE.PHASE_MISSILE;
       // }
       // if (MissileStartTime < time && _fhase == BOSS_PHASE.PHASE_MISSILE)
       // {
       //     time = 0;
       //     _animator.SetTrigger("Missile");
       //     _fhase = BOSS_PHASE.PHASE_TACKLE;
       // }
       // if (TackleStartTime < time && _fhase == BOSS_PHASE.PHASE_TACKLE)
       // {
       //     //_animator.SetTrigger("TackleStart");
       // }
    }

    public void SetBossBattleStart()
    {
        _fhase = BOSS_PHASE.PHASE_BEAM;
    }

    public void SetActive()
    {
        bActive = true;
    }
}
