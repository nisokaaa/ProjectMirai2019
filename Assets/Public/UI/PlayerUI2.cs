using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI2 : MonoBehaviour {

    PlayerUIGoalSpan _playerUIGoalSpan;
    [SerializeField]
    bool _Span = false;

    Animator _animator;
    // Use this for initialization
    void Start () {
        _Span = false;
        _playerUIGoalSpan = GameObject.Find("PlayerUIGoalSpan").GetComponent<PlayerUIGoalSpan>();
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

        if ( _Span == true) { return; }
		if(_playerUIGoalSpan.GetSpanVal() <= 50.0f)
        {
            Debug.Log("通りました！");
            _Span = true;
            _animator.SetTrigger("UI001");
        }
    }
}
