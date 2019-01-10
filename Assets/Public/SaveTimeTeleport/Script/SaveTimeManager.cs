using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//フラグが入ったときに、時間を記録する。
public class SaveTimeManager : MonoBehaviour {

    //現在時間取得
    [SerializeField]
    TimerScript _timerScript;

    //確認用
    [SerializeField]
    Text _text00;

    [SerializeField]
    Text _text01;

    [SerializeField]
    Text _text02;

    [SerializeField]
    Text _text03;

    [SerializeField]
    Text _text04;

    [SerializeField]
    bool _SaveTime = false;

    int _setTexrNo = 0;

    Animator _animator;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(_SaveTime == true)
        {
            _SaveTime = false;
            switch (_setTexrNo)
            {
                case 0:
                    _text00.text = _timerScript.GetTime();
                    _animator.SetTrigger("SavePoint001");
                    break;
                case 1:
                    _text01.text = _timerScript.GetTime();
                    _animator.SetTrigger("SavePoint002");
                    break;
                case 2:
                    _text02.text = _timerScript.GetTime();
                    _animator.SetTrigger("SavePoint003");
                    break;
                case 3:
                    _text03.text = _timerScript.GetTime();
                    _animator.SetTrigger("SavePoint004");
                    break;
                case 4:
                    _text04.text = _timerScript.GetTime();
                    _animator.SetTrigger("SavePoint005");
                    _setTexrNo = -1;
                    break;
            }
            _setTexrNo++;
        }
	}

    public void SetTimeSave()
    {
        _SaveTime = true;
    }
}
