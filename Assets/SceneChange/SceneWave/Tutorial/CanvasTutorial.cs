using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// チュートリアルのアニメーション管理を行う
/// </summary>
public class CanvasTutorial : MonoBehaviour {

    [SerializeField]
    GameObject loopSystem;

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
                    //_animator.SetInteger("Tutorial", _phase);
                    _startCnt = 0;
                    
                }
                break;
            case 1://チュートリアル表示中
                _startCnt++;
                if (Input.anyKey || _setStartCnt < _startCnt*3)
                {
                    _startCnt = 0;
                    _phase++;
                    //_animator.SetInteger("Tutorial", _phase);
                    //_animator.SetTrigger("TutorialChange");
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
                        AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                    }

                    //移動チェック
                    if(m_joyconL.GetStick()[0] != 0 && m_joyconL.GetStick()[1] != 0)
                    {
                        _tutorialCheckMpve = true;
                        AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                    }
                }
                if(Input.GetKeyDown(KeyCode.W))
                {
                    _tutorialCheckMpve = true;
                    AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                }

                if(Input.GetKeyDown(KeyCode.Space))
                {
                    _tutorialCheckJump = true;
                    AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                }

                //シーン切り替え
                if (_tutorialCheckJump == true && _tutorialCheckMpve == true)
                {
                    _tutorialCheckJump = false;
                    _tutorialCheckMpve = false;
                    _phase++;
                    _animator.SetTrigger("TutorialChange");
                    _animator.SetInteger("Tutorial", _phase);
                }
                break;
            case 3://放電処理について
                //ジョイコン操作
                if (!(m_joycons.Count <= 0 || m_joycons == null))
                {
                    //横ダッシュ
                    if (m_joyconR.GetButtonDown(Joycon.Button.SHOULDER_1)|| m_joyconL.GetButtonDown(Joycon.Button.SHOULDER_1))
                    {
                        _tutorialCheckJump = true;
                        AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                    }

                    //移動チェック
                    if (m_joyconR.GetButtonDown(Joycon.Button.SHOULDER_2) || m_joyconL.GetButtonDown(Joycon.Button.SHOULDER_2))
                    {
                        _tutorialCheckMpve = true;
                        AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                    }
                }
                //キーボード操作
                //放電ダッシュ
                if(Input.GetKeyDown(KeyCode.Q)|| Input.GetKeyDown(KeyCode.E))
                {
                    _tutorialCheckJump = true;
                    AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                }
                if (Input.GetKeyDown(KeyCode.N))
                {
                    _tutorialCheckMpve = true;
                    AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                }

                //シーン切り替え
                if (_tutorialCheckJump == true && _tutorialCheckMpve == true)
                {
                    _tutorialCheckJump = false;
                    _tutorialCheckMpve = false;
                    _phase++;
                    _animator.SetTrigger("TutorialChange");
                    _animator.SetInteger("Tutorial", _phase);
                }
                break;
            case 4://大ジャンプについて
                //ジョイコン操作
                if (!(m_joycons.Count <= 0 || m_joycons == null))
                {
                    //横ダッシュ
                    if (m_joyconR.GetButtonDown(Joycon.Button.DPAD_LEFT))
                    {
                        _tutorialCheckJump = true;
                        AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                    }

                    //移動チェック
                    if (m_joyconR.GetButtonDown(Joycon.Button.DPAD_DOWN) )
                    {
                        loopSystem.SetActive(false);
                        _tutorialCheckMpve = true;
                        AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                    }
                }

                if (Input.GetKeyDown(KeyCode.M))
                {
                    _tutorialCheckJump = true;
                    AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    loopSystem.SetActive(false);
                    _tutorialCheckMpve = true;
                    AudioManager.Instance.PlaySE(AUDIO.SE_TITLE_SELECTION);
                }

                //シーン切り替え
                if (_tutorialCheckJump == true && _tutorialCheckMpve == true)
                {
                    _tutorialCheckJump = false;
                    _tutorialCheckMpve = false;
                    _phase++;
                    _animator.SetTrigger("TutorialChange");
                    _animator.SetInteger("Tutorial", _phase);
                }
                break;
            case 5://目的、落下復活、チャージについて
                break;
        }

        
	}

    public void SetFuse(int value)
    {
        _phase = value;
    }
}
