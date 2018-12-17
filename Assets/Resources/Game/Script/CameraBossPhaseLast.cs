using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ボスバトル中のカメラワーク変更
/// shimura masaki
/// </summary>
public class CameraBossPhaseLast : MonoBehaviour {

    [SerializeField]
    bool _BossBattleCamera = false;

    CameraMove _CameraMove;
    CameraRotate _CameraRotate;
    CameraZoom _CameraZoom;
    CameraChaseDelay _CameraChaseDelay;
    CameraRotateDelay _CameraRotateDelay;

    enum STEP
    {
        _0 = 0,
        _1,
        _2,
        _3,
        _4,
        _5,
        _6,
        _7,
        _8,
        _9,
    };

    STEP _step = STEP._0;

    //一秒あたりの回転角度
    public float _angle = 30.0f;

    //回転の中心に使うために使う変数
    private Vector3 _targetPos;



    // Use this for initialization
    void Start () {
        //コンポーネント取得処理
        _CameraMove         = GetComponent<CameraMove>();
        _CameraRotate       = GetComponent<CameraRotate>();
        _CameraZoom         = GetComponent<CameraZoom>();
        _CameraChaseDelay   = GetComponent<CameraChaseDelay>();
        _CameraRotateDelay  = GetComponent<CameraRotateDelay>();

        //===============
        // 回転用処理
        //===============
        Transform target = GameObject.Find("Player").transform;
        _targetPos = target.position;

        //自分の向きをターゲットに向ける
        transform.LookAt(target);

        //自分をZ軸を中心に0～360でランダムに回転させる
        //transform.Rotate(new Vector3(0,0,Random.Range(0,360)),Space.World);
	}
	
	// Update is called once per frame
	void Update () {
        if (_BossBattleCamera == false)
        {
            return;
        }

        switch(_step)
        {
                //初期化
            case STEP._0:
                CameraComponentAllOff();
                transform.rotation = Quaternion.identity;
                _step = STEP._1;
                break;

                //カメラの移動アニメーション
            case STEP._1:
                //プレイヤーを中心に自分を現在の上方向に、毎秒angle分だけ回転する。
                Vector3 axis = transform.TransformDirection(Vector3.up);
                transform.RotateAround(_targetPos, axis, _angle * Time.deltaTime);
                //transform.RotateAround(_targetPos, new Vector3(0.0f , 1.0f ,0.0f), 0.0f);
                Debug.Log("axis :" + axis);
                Debug.Log("回転情報" + _angle * Time.deltaTime);
                break;

                //プレイヤー
            case STEP._2:
                break;
        }
        
    }

    /// <summary>
    /// カメラのコンポーネントをすべて切る
    /// </summary>
    void CameraComponentAllOff()
    {
        _CameraMove.enabled = false;
        _CameraRotate.enabled = false;
        _CameraZoom.enabled = false;
        _CameraChaseDelay.enabled = false;
        _CameraRotateDelay.enabled = false;
    }
}
