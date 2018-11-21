using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamMoveObj : MonoBehaviour {

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

    public ParticleSystem _Beam;
    public ParticleSystem _BeamCircle;
    public Animator _BossAnimator;

    void Start()
    {
        _Beam = GameObject.Find("Particle System Beam").GetComponent<ParticleSystem>();
        _BeamCircle = GameObject.Find("Particle System Beam Circle").GetComponent<ParticleSystem>();
        
        nowTime = 0;
    }

    void Update()
    {
        if (bActive == false)
        {
            return;
        }

        if (Debug == false)
        {
            if (ResetPositionl == true)
            {
                Vector3 currentPoint = BezierCurve.GetPoint(p1, p2, nowTime / moveTime);
                transform.position = currentPoint;

                nowTime += Time.deltaTime;

                if (nowTime > moveTime)
                {
                    _BeamCircle.Stop();
                    _Beam.Stop();
                    _BossAnimator.SetTrigger("Default");
                    nowTime = 0;
                }
            }
            else
            {
                float buf = nowTime;
                Vector3 currentPoint = BezierCurve.GetPoint(p1, p2, nowTime / moveTime);
                transform.position = currentPoint;

                nowTime += Time.deltaTime;

                if (nowTime > moveTime)
                {
                    _BeamCircle.Stop();
                    _Beam.Stop();
                    _BossAnimator.SetTrigger("Default");
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
