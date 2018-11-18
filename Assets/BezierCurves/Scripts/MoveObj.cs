using UnityEngine;
using System.Collections;

public class MoveObj : MonoBehaviour
{
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

    [SerializeField]
    Animator BossAnimator;

    void Start()
    {
        nowTime = 0;
    }

    void Update()
    {
        if(bActive == false)
        {
            return;
        }

        if (Debug == false)
        {
            if(ResetPositionl == true)
            {
                Vector3 currentPoint = BezierCurve.GetPoint(p1, p2, nowTime / moveTime);
                transform.position = currentPoint;

                nowTime += Time.deltaTime;

                if (nowTime > moveTime) nowTime = 0;
            }
            else
            {
                float buf = nowTime;
                Vector3 currentPoint = BezierCurve.GetPoint(p1, p2, nowTime / moveTime);
                transform.position = currentPoint;

                nowTime += Time.deltaTime;

                if (nowTime > moveTime)
                {
                    nowTime = buf;
                    BossAnimator.SetTrigger("Start2");
                }
            }

            
        }else
        {
            if (timePointValue > moveTime)
                return;
            Vector3 currentPoint = BezierCurve.GetPoint(p1, p2, timePointValue / moveTime);
            transform.position = currentPoint;

            
        }
    }
}