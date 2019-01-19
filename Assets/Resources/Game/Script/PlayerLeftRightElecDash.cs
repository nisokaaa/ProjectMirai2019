using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「左右電気ダッシュ」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．手順
/// ２．手順
/// ３．手順
/// 作成者：志村まさき
/// </summary>
public class PlayerLeftRightElecDash : MonoBehaviour {

    [SerializeField]
    bool _rightJump = false;

    [SerializeField]
    bool _leftJump = false;

    [SerializeField]
    int _timeMax = 60;

    ElecBarControl elecBarControl;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    PlayerController _playerController;

    int _dashCnt = 0;
    // Use this for initialization
    void Start () {
        _playerController = GetComponent<PlayerController>();
        if (elecBarControl == null)
        {
            elecBarControl = GameObject.Find("ElecBarController").GetComponent<ElecBarControl>();
        }

        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤
    }
	
	// Update is called once per frame
	void Update () {
        if (_playerController.GetPlayerControl() == true) { return; }
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        if (elecBarControl.GetGageValue() <= 0.0f)
            return;

        //ジョイコン
        if (!(m_joycons.Count <= 0 || m_joycons == null))
        {
            //ジャンプチェック
            if (m_joyconR.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
                _rightJump = true;
            }
            if (m_joyconL.GetButtonDown(Joycon.Button.SHOULDER_2))
            {
                _leftJump = true;
            }
        }

        //キーボード
        if (Input.GetKeyDown(KeyCode.Q) || _leftJump == true)
        {
            _leftJump = true;
        }
        if (Input.GetKeyDown(KeyCode.E) || _rightJump == true)
        {
            _rightJump = true;
        }

        //右加速
        if(_rightJump == true && _leftJump == false)
        {
            _dashCnt++;
            elecBarControl.Decrease();
            rb.AddForce(transform.right * 1500.0f);

            if (_dashCnt > _timeMax)
            {
                
                rb.velocity = Vector3.zero;
                _dashCnt = 0;
                _rightJump = false;
                rb.AddForce(transform.forward * 500);
            }
        }

        //左加速
        if(_leftJump == true && _rightJump == false)
        {
            _dashCnt++;
            elecBarControl.Decrease();
            rb.AddForce(transform.right * -1500.0f);
            if (_dashCnt > _timeMax)
            {
                rb.velocity = Vector3.zero;
                _dashCnt = 0;
                _leftJump = false;
                rb.AddForce(transform.forward * 500);
            }
        }

        if(_leftJump == true && _rightJump == true)
        {
            _dashCnt = 0;
            _leftJump = false;
            _rightJump = false;
        }
    }
}
