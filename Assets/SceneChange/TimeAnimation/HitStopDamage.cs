using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitStopDamage : MonoBehaviour {

    [SerializeField]
    private GameObject damagePrefab;

    [SerializeField]
    private TimeManager timeManager;

    

    bool bStop = false;
    int Timecnt;
    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    PlayerColliderCheck _colliderCheck;
    ElecBarControl _elecBarControl;

    private void Start()
    {
        bStop = false;
        _elecBarControl = GameObject.Find("ElecBarController").GetComponent<ElecBarControl>();
        _colliderCheck = GameObject.Find("Player").GetComponent<PlayerColliderCheck>();
        //joycon
        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤
    }

    private void Update()
    {
        if(_colliderCheck.GetCollisionEnterExit() == false) { return; }
        if(_elecBarControl.GetGageValue() <= 0) { return; }

        if(Input.GetKeyDown(KeyCode.M))
        {
            timeManager.SlowDown();
        }

        if (!(m_joycons.Count <= 0 || m_joycons == null))
        {
            if (m_joyconR.GetButtonDown(Joycon.Button.DPAD_LEFT))
            {
                timeManager.SlowDown();
            }
        }

        //if(bStop == true)
        //{
        //    var damageParticle = GameObject.Instantiate(damagePrefab, transform.position, Quaternion.identity) as GameObject;
        //    //　全体のタイムスケールを変更する
        //    timeManager.SlowDown();
        //}
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Obstacle")
        {
            var damageParticle = GameObject.Instantiate(damagePrefab, col.ClosestPointOnBounds(col.transform.position), Quaternion.identity) as GameObject;
            //　全体のタイムスケールを変更する
            timeManager.SlowDown();
        }
    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Obstacle")
        {
            //var damageParticle = GameObject.Instantiate(damagePrefab, transform.position, Quaternion.identity) as GameObject;
            ////　全体のタイムスケールを変更する
            //timeManager.SlowDown();
        }
    }
}
