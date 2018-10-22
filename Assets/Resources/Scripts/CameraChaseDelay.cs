using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChaseDelay : MonoBehaviour {

    // TargetとのZ軸オフセット
    [SerializeField, Range(0f, 25f)]
    private float baseDistance = 8f;

    // TargetとのY軸オフセット
    [SerializeField, Range(0f, 25f)]
    private float baseHeight = 3f;

    // Targetへの追従速度
    [SerializeField, Range(1f, 150f)]
    private float chaseSpeed = 10f;

    private Transform player;
    private Transform cam;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        cam = this.transform;
    }

    void FixedUpdate()
    {
        // カメラの位置を設定
        var desiredPos = player.position - player.forward * baseDistance + Vector3.up * baseHeight;
        cam.position = Vector3.Lerp(cam.position, desiredPos, Time.deltaTime * chaseSpeed);

        // カメラの向きを設定(別スクリプトにてカメラを回転遅延させるならコメ必須)
        cam.LookAt(player);
    }
}
