using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ミサイルアニメーションが実行されたときにMissileを落とす
/// </summary>
public class BossMissileSystem : MonoBehaviour {

    [SerializeField] bool _bMissileOn = false;
    Animator _animator;
	// Use this for initialization
	void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_bMissileOn == true)
        {
            _animator.SetTrigger("MissilePoint");
            _bMissileOn = false;
        }
	}

    public void SetMissileAction()
    {
        _bMissileOn = true;
    }
}
