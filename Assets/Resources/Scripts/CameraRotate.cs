using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotate : MonoBehaviour {

    [SerializeField, Range(0.01f, 0.4f)]
    private float rotationSpeedX = 0.2f;

    [SerializeField, Range(0.01f, 0.4f)]
    private float rotationSpeedY = 0.2f;
    
    private Vector2 prePosition;
    private Vector2 newAngle;

	// Use this for initialization
	void Start () {
    }

    // Update is called once per frame
    void Update()
    {
        // 右クリックした時
        if (Input.GetMouseButtonDown(1))
        {
            newAngle = transform.localEulerAngles;
            prePosition = Input.mousePosition;
        }
        // 右ドラッグしている時
        else if (Input.GetMouseButton(1))
        {
            // Y軸の回転量を求める
            newAngle.y += (Input.mousePosition.x - prePosition.x) * rotationSpeedY;

            // X軸の回転量を求める
            newAngle.x -= (Input.mousePosition.y - prePosition.y) * rotationSpeedX;

            // カメラに回転量代入
            transform.localEulerAngles = new Vector3(newAngle.x, newAngle.y, 0f);

            // 現フレームのマウス座標保存
            prePosition = Input.mousePosition;
        }
    }
}
