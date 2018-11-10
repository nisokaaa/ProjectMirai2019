using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player移動クラス
/// GakuMoriya
/// </summary>
public class PlayerController : MonoBehaviour
{
    // 速度
    [SerializeField, Range(1f, 150f)]
    float speed = 50f;
    float speedCurrent = 0.0f;

    // 減衰率
    [SerializeField, Range(1f, 200f)]
    float moveForceMultiplier = 20f;

    // アクセル状態
    bool bAccelerator = false;

    // デバッグ用
    bool bBack = false;

    void Update()
    {
        // アクセルボタンInput
        bAccelerator = Input.GetKey("joystick button 5") ? true : false;
        bAccelerator = Input.GetKey(KeyCode.W) ? true : false;
        bAccelerator = Input.GetKey(KeyCode.Joystick1Button0) ? true : false;

        bBack = Input.GetKey(KeyCode.S) ? true : false;

        // 回転
        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("L_Stick_H"));
        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Horizontal"));
        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Horizontal 1"));

        // forwardのRay
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }

    void FixedUpdate()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        // アクセル押下してたら速度代入
        speedCurrent = bAccelerator ? speed : 0.0f;
        if(!bAccelerator) speedCurrent = bBack ? -speed : 0.0f;

        Vector3 moveVector = Vector3.zero;
        moveVector = speedCurrent * transform.forward;

        // ボタン非押下で勝手に速度減衰
        rb.AddForce(moveForceMultiplier * (moveVector - rb.velocity));

        // Rayが地面にあたってないときは速度減衰をやわらげよう・・・・
        //Debug.Log(rb.velocity);
    }
}
