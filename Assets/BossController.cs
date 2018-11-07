using System.Collections;
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



    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        m_velocity += ((m_target.position - SeecPos) - transform.position) * m_speed;
        m_velocity *= m_attenuation;
        transform.position += m_velocity *= Time.deltaTime;
    }
}
