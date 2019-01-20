using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボスコントローラーのアニメーションフラグを操作をするクラス
/// </summary>
public class BossAnimationController : MonoBehaviour {

    Animator _animator;
    [SerializeField]
    bool _DebugAttack = false;

    [SerializeField]
    bool _DebugMissile = false;

    [SerializeField]
    bool _DebugBeam = false;

    [SerializeField]
    bool _DebugDefault = false;

    [SerializeField]
    bool _Debug = false;

    //[SerializeField]
    //GameObject _DefaultEffect;

    // Use this for initialization
    void Start () {
        //_DefaultEffect.SetActive(false);
        _animator = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        Debug();
    }

    void Debug()
    {
        if (_Debug == false) return;
        if(_DebugDefault)
        {
            
        }
        if (_DebugAttack)
        {
            _animator.SetTrigger("Attack");
        }
        if (_DebugBeam)
        {
            _animator.SetTrigger("Beam");
        }
        if (_DebugMissile)
        {
            _animator.SetTrigger("Missile");
        }
    }

    public void SetAttack()
    {
        _animator.SetTrigger("Attack");
    }
    public void SetBeam()
    {
        _animator.SetTrigger("Beam");
    }
    public void SetMissile()
    {
        _animator.SetTrigger("Missile");
    }
}
