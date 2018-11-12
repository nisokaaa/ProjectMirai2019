/*
このプログラムは次の機能を有します。
• カメラはプレイヤーの操作によって向きを変えられる。水平方向は際限なく、垂直方向は限界値を設ける。
• カメラは 常に画面の中心にキャラが見えるよう向きを調整する。
• カメラは 常に一定の距離を保ってキャラを追尾するよう位置を調整する。
• カメラは Collidor を避けなければならない。つまり壁や柱の中に入り込むことはできない。
• カメラとキャラの間に Collidor があったとき、カメラは一時的に近づいてキャラが見えなくなることを防止する。
*/
using UnityEngine;
using System.Collections;

public class Action3dCam : MonoBehaviour
{
    // カメラが追従する対象となる GameObject：Inspectorでキャラのオブジェクトをここに指定
    public Transform target;

    // カメラと視線を遮る GameObjectのフラグのマスク：全てのオブジェクトを対象にする
    private LayerMask lineOfSightMask = -1;

    // カメラが追従する対象からのオフセット：キャラの中心は足元にあるため1.5m上に調整している
    public Vector3 targetOffset = new Vector3(0.0f, 1.5f, 0.0f);

    // ターゲットとカメラの現在の距離：遮るものがあるとこの距離は縮まる
    public float currentDistance = 3.0f;

    // ターゲットとカメラの距離：遮るものがない時の距離
    public float distance = 3.0f;

    // カメラの視点の角度
    private float x = 0.0f;
    private float y = 0.0f;

    // ターゲットとカメラの距離が変更されるスピード（メソッドが返す値を格納する変数）
    private float distanceVelocity = 0.0f;

    void Start()
    {
        // 配置されたカメラの視点の角度（初期値）を問い合わせる
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
    }

    // カメラの更新は他の更新よりも後に行うべきであるため、LateUpdate() を使う
    void LateUpdate()
    {
        if (Input.GetMouseButton(1))    // マウスの右ボタンが押されているかどうか
        {
            x += Input.GetAxis("Mouse X") * 4.0f; // マウスのX軸の移動：補正を加えている
            y -= Input.GetAxis("Mouse Y") * 1.6f; // マウスのY軸の移動：補正を加えている
        }
        //x += Input.GetAxis("Stick X"); // コントローラのスティックX軸の角度：名称はInput Managerに設定が必要
        //y += Input.GetAxis("Stick Y"); // コントローラのスティックY軸の角度：名称はInput Managerに設定が必要

        // カメラのY方向に制限を加える
        y = Mathf.Clamp(y, -20, 80);

        // 入力された値から視点の角度を計算する
        Quaternion rotation = Quaternion.Euler(y, x, 0);

        // ターゲットオブジェクトの中心点にオフセットを足したものをターゲットとする：キャラの頭辺りになる
        Vector3 targetPos = target.position + targetOffset;

        // 視点の角度から方向を求める
        Vector3 direction = rotation * -Vector3.forward;

        // カメラとターゲットの距離を算出するメソッドをよぶ
        float targetDistance = AdjustLineOfSight(targetPos, direction);

        // カメラとターゲットの距離をスムースに変更する：遮るものがあったときに機能する
        currentDistance = Mathf.SmoothDamp(currentDistance, targetDistance, ref distanceVelocity, 0.06f);

        // カメラの角度を設定
        transform.rotation = rotation;

        // カメラの位置を設定
        transform.position = targetPos + direction * currentDistance;
    }

    // カメラとターゲットの距離を算出するメソッド
    private float AdjustLineOfSight(Vector3 target, Vector3 direction)
    {
        RaycastHit hit; // レイキャストを飛ばしてヒットすると設定される変数
            
        // ターゲットからカメラに向かってレイキャストを飛ばし、途中で遮るものがあったらその情報を返す
        if (Physics.Raycast(target, direction, out hit, distance, lineOfSightMask.value, QueryTriggerInteraction.UseGlobal ))
            //  途中で遮るものがあったとき、そこまでの距離から若干引いて返す
            return hit.distance - 0.2f; // Closer Radius
        else
            // 遮るものがないなら、そのままカメラとターゲットの距離を返す
            return distance;
    }
}