using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アニメーターで制御する
/// </summary>
public class BossBattleController : MonoBehaviour {

    public Transform m_target = null;
    private Vector3 m_velocity;
    public float m_attenuation = 0.5f;
    [SerializeField] Vector3 SeecPos;
    public float m_speed = 5;

    CameraChaseDelay _cameraChaseDelay;
    int cnt = 0;
    bool _bCameraBoss = false;

    [SerializeField] bool _bMissile = false;
    [SerializeField] bool _bBeam = false;
    [SerializeField] bool _bAttack = false;

    BossMissileSystem _bossMissileSystem;
    [SerializeField]
    Animator _modelanimator;

    // Use this for initialization
    void Start () {

        if (m_target == null)
        {
            m_target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        }
        _cameraChaseDelay = Camera.main.GetComponent<CameraChaseDelay>();
        _bossMissileSystem = GameObject.Find("BattleMissileSystem").GetComponent<BossMissileSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if(_bCameraBoss == false)
        {
           _cameraChaseDelay.SetBossBattle();
        }
        
        //追従処理
        FollowingPlayer();

        if (_bAttack == true) {
            _bAttack = false;
        }
        if (_bBeam == true) {
            _bBeam = false;
        }
        if (_bMissile == true)
        {
            _bossMissileSystem.SetMissileAction();
            _modelanimator.SetTrigger("Missile");
            _bMissile = false;
        }
    }

    public void SetAttack()
    {
        _bAttack = true;
    }

    public void SetBeam()
    {
        _bBeam = true;
    }

    public void SetMissile()
    {
        _bMissile = true;
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

    public void SetCameraGole()
    {
        Camera.main.GetComponent<CameraChaseDelay>().SetGoal();
    }
}
