using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChaseDelay : MonoBehaviour {

    // TargetとのZ軸オフセット
    [SerializeField, Range(0f, 100f)]
    private float baseDistance = 40f;

    // TargetとのY軸オフセット
    [SerializeField, Range(0f, 100f)]
    private float baseHeight = 20f;

    // Targetへの追従速度
    [SerializeField, Range(1f, 150f)]
    private float chaseSpeed = 10f;

    private Transform player;
    private Transform cam;

    [SerializeField, Range(1f, 150f)]
    private int ScalarCamera = 1;

    public float HeightM = 1.2f;            // 注視点の高さ[m]
    public GameObject targetObject; // 注視したいオブジェクトをInspectorから入れておく

    public bool _bBossPosRot = false;
    CameraRotateDelay _cameraRotateDelay;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = this.transform;
        _cameraRotateDelay = GetComponent<CameraRotateDelay>();
    }

    private void Update()
    {
        
    }
    void FixedUpdate()
    {
        // カメラの位置を設定
        var desiredPos = player.position - player.forward * baseDistance + Vector3.up * baseHeight;

        cam.position = Vector3.Lerp(cam.position, desiredPos, Time.deltaTime * chaseSpeed);

        if (_bBossPosRot == true)
        {
            _cameraRotateDelay.enabled = false;
            // 補完スピードを決める
            float speed = 0.1f;
            // ターゲット方向のベクトルを取得
            Vector3 relativePos = targetObject.transform.position - this.transform.position;
            // 方向を、回転情報に変換
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            // 現在の回転情報と、ターゲット方向の回転情報を補完する
            transform.rotation = Quaternion.Slerp(this.transform.rotation, rotation, speed);

            return;
        }



        var lookAt = player.position + Vector3.up * HeightM;

        // カメラの向きを設定(別スクリプトにてカメラを回転遅延させるならコメ必須)
        //cam.LookAt(player - player.forward);
        cam.LookAt(lookAt);
    }

    public void SetBossBattle()
    {
        _bBossPosRot = true;
    }
}
