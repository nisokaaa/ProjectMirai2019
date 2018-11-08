﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {

    [SerializeField] GameObject PlayerGameObject;

    enum STATE
    {
        NONE = 0,
        START,      //ゲームスタート演出
        BATTLE,     //バトル中の処理、追従
        END,        //退却処理
    };

    public Transform m_target = null;
    public float m_speed = 5;
    public float m_attenuation = 0.5f;
    [SerializeField] Vector3 SeecPos;

    private Vector3 m_velocity;

    STATE state;

    BossBattleStart bossBattleStartScript;
    [SerializeField] int timeCnt;
    // Use this for initialization
    void Start () {
        bossBattleStartScript = GameObject.Find("BossBattleStart").GetComponent<BossBattleStart>();
        timeCnt = 0;
    }
	
	// Update is called once per frame
	void Update () {
        switch(state)
        {
            case STATE.NONE:
                if(bossBattleStartScript.GetBossBattleStartTime() == 0)
                {
                    state = STATE.START;
                }
                break;
            case STATE.START:
                m_speed = 10;
                FollowingPlayer();
                
                timeCnt--;
                if(timeCnt <= 0)
                {
                    state = STATE.BATTLE;
                }
                break;
            case STATE.BATTLE:
                FollowingPlayer();
                break;
            case STATE.END:
                break;
        }
    }

    //プレイヤーに追従する
    void FollowingPlayer()
    {
        m_velocity += ((m_target.position - SeecPos) - transform.position) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;
    }
}
