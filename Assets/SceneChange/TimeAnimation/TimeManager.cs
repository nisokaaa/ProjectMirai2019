using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour {

    //　Time.timeScaleに設定する値
    [SerializeField]
    private float timeScale = 0.1f;
    //　時間を遅くしている時間
    [SerializeField]
    private float slowTime = 1f;

    [SerializeField]//　経過時間
    private float elapsedTime = 0f;

    //　時間を遅くしているかどうか
    private bool isSlowDown = false;

    [SerializeField]
    Rigidbody _player;

    [SerializeField]
    CapsuleCollider _playerCollider;

    [SerializeField]
    bool _bDebug = false;

    private void Start()
    {
    }
    void Update()
    {
        if(_bDebug == true)
        {
            SlowDown();
        }

        //　スローダウンフラグがtrueの時は時間計測
        if (isSlowDown)
        {
            elapsedTime += Time.unscaledDeltaTime;
            if (elapsedTime >= slowTime)
            {
                _bDebug = false;
                SetNormalTime();
            }
        }
    }
    //　時間を遅らせる処理
    public void SlowDown()
    {
        _bDebug = false;
        elapsedTime = 0f;
        Time.timeScale = timeScale;
        isSlowDown = true;
        _player.interpolation = RigidbodyInterpolation.Interpolate;
        //_playerCollider.enabled = false;
    }

    //　時間を元に戻す処理
    public void SetNormalTime()
    {
        Time.timeScale = 1f;
        isSlowDown = false;
        _player.interpolation = RigidbodyInterpolation.None;
        //_playerCollider.enabled = true;
    }

    public bool GetSlowFlag()
    {
        return isSlowDown;
    }
}
