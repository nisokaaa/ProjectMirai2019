using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierMovingObject : MonoBehaviour
{
    // ベジェ線の入ったPrefabをセット
    public BezierCurve Curve;
    // 移動にかける時間（秒）
    public float MoveTime = 2f;

    // 現在の地点を保持
    private int _currentPoint;
    // 経過時間
    private float _currentTime;
    // 移動完了フラグ
    private bool _isComplete;

    void Update()
    {
        // こういうのが嫌ならCorutine使ってwhile(true)で回して、
        // _isCompleteの条件があったらbreakでいいと思う。
        if (_isComplete) return;

        // カーブに沿って移動
        // ポイント１とポイント２の間に作られるカーブの、xx%時点での位置を取得する
        transform.position =
            BezierCurve.GetPoint(Curve[_currentPoint],
                                 Curve[_currentPoint + 1],
                                 _currentTime / MoveTime);
        _currentTime += Time.deltaTime;
        // 現在時間が移動時間を超えたら100%過ぎてるので次のカーブへ行く
        if (_currentTime > MoveTime)
        {
            if (++_currentPoint == Curve.pointCount - 1) _isComplete = true;
            _currentTime = 0;
        }
    }

}