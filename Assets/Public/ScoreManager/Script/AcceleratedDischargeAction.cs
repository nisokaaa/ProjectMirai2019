﻿using System.Collections;
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

    [SerializeField] Vector3 AcceleratedSpeed;
    [SerializeField, Range(1f, 2f)] float AcceleratorSpeed=1.1f;
    [SerializeField, Range(0f, 1f)] float AcceleratorLimit = 0.8f;
    ScoreManager scoreManager;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;

    int SeCnt = 0;
    bool _se = false;

    PlayerColliderCheck _playerColliderCheck;

    bool _Acceleration = false;

    // Use this for initialization
    void Start () {
        if(_playerColliderCheck == null)
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
        if (scoreManager == null)
        {
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
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

		if(Input.GetKey(KeyCode.N)|| playerElecMode.GetMode() == true)
        {
            _Acceleration = true;
            elecBarControl.Decrease();
            elecBarControl.Decrease();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            //if (Input.GetKey(KeyCode.Space))
            //{
            //    rb.up *= 2.0f;
            //}

            AcceleratedSpeed = rb.velocity * AcceleratorSpeed;
            rb.AddForce(AcceleratedSpeed);
            scoreManager.AddScoreValue(1);
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
            _Acceleration = false;
            particleSystem.SetActive(false);
            playerModelAnimatorController.PlayerAtackControl(false);
        }
    }

    public bool GetAcceleration()
    {
        return _Acceleration;
    }
}
