using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBattleController : MonoBehaviour {

    public Transform m_target = null;
    private Vector3 m_velocity;
    public float m_attenuation = 0.5f;
    [SerializeField] Vector3 SeecPos;
    public float m_speed = 5;

    CameraChaseDelay _cameraChaseDelay;
    int cnt = 0;
    bool _bCameraBoss = false;
    // Use this for initialization
    void Start () {

        if (m_target == null)
        {
            m_target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        _cameraChaseDelay = Camera.main.GetComponent<CameraChaseDelay>();

    }
	
	// Update is called once per frame
	void Update () {
        if(_bCameraBoss == false)
        {
           _cameraChaseDelay.SetBossBattle();
        }
        
        //追従処理
        FollowingPlayer();

    }

    void Attack()
    {

    }

    void Beam()
    {

    }

    void Missile()
    {

    }

    //プレイヤーに追従する
    void FollowingPlayer()
    {
        if (m_target == null)
        {
            return;
        }
        m_velocity += ((m_target.position - SeecPos) - transform.position) * m_speed;
        m_velocity *= m_attenuation;

        //移動量にXの情報を省く
        m_velocity.x = 0.0f;

        transform.position += m_velocity *= Time.deltaTime;
    }
}
