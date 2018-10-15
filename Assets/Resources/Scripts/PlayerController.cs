using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player移動クラス
/// GakuMoriya
/// </summary>
public class PlayerController : MonoBehaviour
{
    [SerializeField, Range(1f, 50f)]
    float Speed = 25f;
    float SpeedCurrent = 0.0f;

    [SerializeField, Range(1f, 100f)]
    float moveForceMultiplier = 100f;

    bool bAccelerator = false;

    Vector3 moveVector = Vector3.zero;
    
    void Update()
    {
        bAccelerator = Input.GetKey("joystick button 5") ? true : false;
        
        // 回転
        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("L_Stick_H"));

        // forwardのRay
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 5, Color.yellow);
        
    }

    void FixedUpdate()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        SpeedCurrent = bAccelerator ? Speed : 0.0f;
        
        moveVector = SpeedCurrent * transform.forward;

        rb.AddForce(moveForceMultiplier * (moveVector - rb.velocity));
    }
}
