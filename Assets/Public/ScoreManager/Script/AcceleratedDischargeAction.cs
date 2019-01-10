using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「放電アクション 加速処理」
/// 作成者：志村まさき
public class AcceleratedDischargeAction : MonoBehaviour {
    PlayerModelAnimatorController playerModelAnimatorController;
    ElecBarControl elecBarControl;
    PlayerElecMode playerElecMode;
    public GameObject particleSystem;
    PlayerController _playerController;
    [SerializeField] Vector3 AcceleratedSpeed;
    [SerializeField, Range(1f, 2f)] float AcceleratorSpeed=1.1f;
    [SerializeField, Range(0f, 1f)] float AcceleratorLimit = 0.8f;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    int SeCnt = 0;
    bool _se = false;

    PlayerColliderCheck _playerColliderCheck;

    bool _Acceleration = false;

    Rigidbody rb;

    bool _RaipidFlag = false;
    int _RaipidTime = 0;

    // Use this for initialization
    void Start () {
        _playerController = GetComponent<PlayerController>();
        rb = gameObject.GetComponent<Rigidbody>();

        if (_playerColliderCheck == null)
        _playerColliderCheck = GetComponent<PlayerColliderCheck>();

        if (playerModelAnimatorController == null)
        {
            playerModelAnimatorController = GameObject.Find("PlayerModelAnimatorController").GetComponent<PlayerModelAnimatorController>();
        }
        if (elecBarControl == null)
        {
            elecBarControl = GameObject.Find("ElecBarController").GetComponent<ElecBarControl>();
        }
        if(playerElecMode == null)
        {
            playerElecMode = GetComponent<PlayerElecMode>();
        }

        particleSystem = Instantiate(particleSystem, transform.position, Quaternion.identity)as GameObject;
        particleSystem.SetActive(false);

        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤
    }
	
	// Update is called once per frame
	void Update () {
        if (elecBarControl.GetGageValue() <= 0.0f)
        {
            particleSystem.SetActive(false);
            return;
        }
        if(_playerController.GetPlayerControl() == true) { return; }
		if(Input.GetKey(KeyCode.N)|| playerElecMode.GetMode() == true)
        {
            _Acceleration = true;
            elecBarControl.Decrease();
            elecBarControl.Decrease();


            //if (Input.GetKey(KeyCode.Space))
            //{
            //    rb.up *= 2.0f;
            //}



            AcceleratedSpeed *= 0.8f;
            //加速処理
            AcceleratedSpeed = rb.velocity * AcceleratorSpeed;
            rb.AddForce(AcceleratedSpeed);
            AcceleratedSpeed *= AcceleratorLimit;

            //演出
            particleSystem.transform.position = transform.position;
            particleSystem.SetActive(true);
            playerModelAnimatorController.PlayerAtackControl(true);

            if (SeCnt >= 10)
            {
                SeCnt = 0;
                _se = false;
            }

            SeCnt++;
            if (_se == false)
            {
                _se = true;
                AudioManager.Instance.PlaySE(AUDIO.SE_ELECTRICAL);
            }
            
            if(Input.GetKeyDown(KeyCode.Space) && _playerColliderCheck.GetCollisionEnterExit() == true)
            {
                AudioManager.Instance.PlaySE(AUDIO.SE_GAME_ELECTRICAL_JUMP);
            }
        }
        else
        {
            _RaipidFlag = false;
            _Acceleration = false;
            particleSystem.SetActive(false);
            playerModelAnimatorController.PlayerAtackControl(false);
        }
    }
    /// <summary>
    /// 物理的な挙動のため，FixedUpdateを使う．
    /// </summary>
    private void FixedUpdate()
    {
        if (_Acceleration == false)
        {
            return;
        }
        //急加速
        RaipidAcceleration(10);
    }

    /// <summary>
    /// 急加速
    /// </summary>
    void RaipidAcceleration(int time)
    {
        if(_RaipidFlag == true)
        {
            return;
        }

        //急加速処理
        //rb.AddForce(rb.velocity * (AcceleratorSpeed));
        //rb.AddForce(rb.velocity);
        _RaipidTime++;

        //加速解除
        if (time < _RaipidTime)
        {
            _RaipidFlag = true;
        }
    }

    /// <summary>
    /// 加速状態の取得
    /// </summary>
    /// <returns></returns>
    public bool GetAcceleration()
    {
        return _Acceleration;
    }
}
