using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    
    [SerializeField, Range(0.01f, 20.0f)]
    private float moveSpeed = 10f;
    private Vector3 prePosition;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
       
        // ホイールクリックした時
        if (Input.GetMouseButtonDown(2))
        {
            prePosition = Input.mousePosition;
        }
        // ホイールドラッグした時
        else if (Input.GetMouseButton(2))
        {
            // 差分計算
            Vector3 axis = Input.mousePosition - prePosition;

            // 補正
            if (axis.magnitude < Vector3.kEpsilon)
                return;

            // 平行移動
            transform.Translate(-axis * Time.deltaTime * moveSpeed);
            
            // 保存
            prePosition = Input.mousePosition;
        }
    }
}
