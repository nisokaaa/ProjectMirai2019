using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour {
    enum STATE
    {
        NONE = 0,
        START,      //ゲームスタート演出
        BATTLE,     //バトル中の処理、追従
        END,        //退却処理
    };

    public Transform m_target = null;
    public float m_speed = 5;
    public float m_StartSpeed = 10;
    public float m_BattleSpeed = 20;
    public float m_attenuation = 0.5f;
    [SerializeField] Vector3 SeecPos;

    private Vector3 m_velocity;

    STATE state;

    BossBattleStart bossBattleStartScript;
    [SerializeField] int timeCnt;
    // Use this for initialization
    void Start () {
        if(m_target == null)
        {
            m_target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }

        bossBattleStartScript = GameObject.Find("BossBattleStart").GetComponent<BossBattleStart>();
        state = STATE.NONE;
    }
	
	// Update is called once per frame
	void Update () {
        if(bossBattleStartScript == null)
        {
            return;
        }
        switch(state)
        {
            case STATE.NONE:
                if(bossBattleStartScript.GetBossBattleStartTime() == 0)
                {
                    state = STATE.START;
                }
                break;
            case STATE.START:
                FollowingPlayer();
                timeCnt--;
                m_speed = m_StartSpeed;
                if (timeCnt <= 0)
                {
                    state = STATE.BATTLE;
                    timeCnt = 0;
                }
                break;
            case STATE.BATTLE:
                m_speed = 10;
                m_speed = m_BattleSpeed;
                FollowingPlayer();
                break;
            case STATE.END:
                break;
        }
    }

    //プレイヤーに追従する
    void FollowingPlayer()
    {
        if(m_target == null)
        {
            return;
        }
        m_velocity += ((m_target.position - SeecPos) - transform.position) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;
    }
}
