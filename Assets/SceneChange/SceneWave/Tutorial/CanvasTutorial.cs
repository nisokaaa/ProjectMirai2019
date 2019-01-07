using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チュートリアルのアニメーション管理を行う
/// </summary>
public class CanvasTutorial : MonoBehaviour {

    [SerializeField]
    int _setStartCnt = 30;

    [SerializeField]
    int _startCnt = 0;

    [SerializeField]
    int _phase = 0;

    Animator _animator;

    bool _tutorialCheckMpve = false;
    bool _tutorialCheckJump = false;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();

        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤
    }
	
	// Update is called once per frame
	void Update () {
        switch(_phase)
        {
            case 0://チュートリアル未表示
                _startCnt++;
                if (_setStartCnt < _startCnt)
                {
                    _phase++;
                    _animator.SetInteger("Tutorial", _phase);
                    _startCnt = 0;
                    
                }
                break;
            case 1://チュートリアル表示中
                _startCnt++;
                if (Input.anyKey || _setStartCnt < _startCnt)
                {
                    _startCnt = 0;
                    _phase++;
                    _animator.SetInteger("Tutorial", _phase);
                }
                break;
            case 2://基本操作について表示 //移動とジャンプが出来たら次に飛ぶ
                //_phase++;
                //_animator.SetInteger("Tutorial", _phase);
                if (!(m_joycons.Count <= 0 || m_joycons == null))
                {
                    //ジャンプチェック
                    if (m_joyconR.GetButtonDown(Joycon.Button.DPAD_DOWN))
                    {
                        _tutorialCheckJump = true;
                    }

                    //移動チェック
                    if(m_joyconL.GetStick()[0] != 0 && m_joyconL.GetStick()[1] != 0)
                    {
                        _tutorialCheckMpve = true;
                    }

                    if(_tutorialCheckJump == true && _tutorialCheckMpve == true)
                    {
                        _phase++;
                        _animator.SetInteger("Tutorial", _phase);
                    }
                }
                break;
            case 3://放電処理について

                break;
        }

        
	}
}
