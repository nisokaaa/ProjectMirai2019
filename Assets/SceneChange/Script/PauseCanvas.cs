using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ポーズしているかどうか判断して、アニメーションを表示させる
public class PauseCanvas : MonoBehaviour {

    [SerializeField]
    Pausable _pausable;

    bool _pauseAnimation = false;
    bool _oldState = false;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //現在の状況と過去の状況を比較して違う場合のみ処理を通す。
        if (_pausable.GetPauseFlag() == true && _pauseAnimation == false)
        {
            _pauseAnimation = true;
            GetComponent<Animator>().SetTrigger("Start");
        }
        if (_pausable.GetPauseFlag() == false && _pauseAnimation == true)
        {
            _pauseAnimation = false;
            GetComponent<Animator>().SetTrigger("End");
        }
    }
}
