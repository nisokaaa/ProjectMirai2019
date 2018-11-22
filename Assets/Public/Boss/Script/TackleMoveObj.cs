using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TackleMoveObj : MonoBehaviour {

    public BezierPoint p1;
    public BezierPoint p2;

    public float nowTime;
    public float moveTime = 2f;

    [SerializeField]
    bool ResetPositionl = true;

    [SerializeField]
    bool Debug = false;

    [SerializeField]
    float timePointValue = 0;

    [SerializeField]
    bool bActive = false;

    public Animator _BossAnimator;
    Transform _BossPosition;
    BossController _bossController;
    GameObject _tacklePointCube;
    SetPositionSeek _setPositionSeek;
    void Start()
    {
        nowTime = 0;
        _tacklePointCube = GameObject.Find("TacklePointCube");
        _setPositionSeek = _tacklePointCube.GetComponent<SetPositionSeek>();
        _BossPosition = GameObject.Find("Boss").GetComponent<Transform>();
        _bossController = GameObject.Find("Boss").GetComponent<BossController>();
    }

    void Update()
    {
        if (bActive == false)
        {
            return;
        }

        //ボスの座標変更処理を止める
        //_bossController.SetBossControllerOff();
        _setPositionSeek.enabled = false;

        if (Debug == false)
        {
            if (ResetPositionl == true)
            {
                Vector3 currentPoint = BezierCurve.GetPoint(p1, p2, nowTime / moveTime);
                transform.position = currentPoint;
                //_BossPosition.position = currentPoint;

                nowTime += Time.deltaTime;

                if (nowTime > moveTime)
                {
                    nowTime = 0;
                    _BossAnimator.SetTrigger("TackleEnd");
                }
            }
            else
            {
                float buf = nowTime;
                Vector3 currentPoint = BezierCurve.GetPoint(p1, p2, nowTime / moveTime);
                transform.position = currentPoint;
                //_BossPosition.position = currentPoint;

                nowTime += Time.deltaTime;

                if (nowTime > moveTime)
                {
                    _BossAnimator.SetTrigger("TackleEnd");
                    nowTime = buf;
                }
            }


        }
        else
        {
            if (timePointValue > moveTime)
                return;
            Vector3 currentPoint = BezierCurve.GetPoint(p1, p2, timePointValue / moveTime);
            transform.position = currentPoint;
            //_BossPosition.position = currentPoint;
        }
    }

    private void init()
    {
        nowTime = 0;
        bActive = false;
    }

    public void SetStart()
    {
        nowTime = 0;
        bActive = true;
    }
    public void SetStop()
    {
        bActive = false;
    }
}
