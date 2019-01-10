using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBossTackle : MonoBehaviour {

    [SerializeField]
    GameObject _JumpPointObject;

    [SerializeField]
    bool _Jump = false;

    [SerializeField]
    GameObject objB;

    [SerializeField]
    float dis;

    [SerializeField]
    private TimeManager timeManager;

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    PlayerController _playerController;
    PlayerColliderCheck _playerColliderCheck;

    // Use this for initialization
    void Start () {
        _playerColliderCheck = GameObject.Find("Player").GetComponent<PlayerColliderCheck>();
        _JumpPointObject = Instantiate(_JumpPointObject, transform.position, Quaternion.identity)as GameObject;
        _JumpPointObject.SetActive(false);
        _playerController = GetComponent<PlayerController>();
        //joycon
        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤
    }
	
	// Update is called once per frame
	void Update () {
        //if()
        //{
        //    _Jump = true;
        //}
        if (_playerController.GetPlayerControl() == true) { return; }
        if (_playerColliderCheck.GetCollisionEnterExit() == false) { return; }

        if ((timeManager.GetSlowFlag() == true) && Input.GetKeyDown(KeyCode.Space))
        {
            _JumpPointObject.transform.position = transform.position;
            _JumpPointObject.transform.localRotation = transform.localRotation;

            _JumpPointObject.SetActive(true);
            _Jump = false;
            timeManager.SetNormalTime();
        }
        if (!(m_joycons.Count <= 0 || m_joycons == null))
        {
            if ((timeManager.GetSlowFlag() == true) && m_joyconR.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                _JumpPointObject.transform.position = transform.position;
                _JumpPointObject.transform.localRotation = transform.localRotation;
                _JumpPointObject.SetActive(true);
                _Jump = false;
                timeManager.SetNormalTime();
            }
        }
        
        //Vector3 Apos = transform.position;
        //Vector3 Bpos = objB.transform.position;
        //dis = Vector3.Distance(Apos, Bpos);
    }
}
