using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player移動クラス
/// GakuMoriya
/// </summary>
public class PlayerController : MonoBehaviour
{
    public float speed = 3.0f;
    public float moveX, moveZ;

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("D_Pad_H") * speed;
        moveZ = Input.GetAxis("D_Pad_V") * speed;
    }

    void FixedUpdate()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        rb.AddForce(new Vector3(moveX, 0, moveZ), ForceMode.Force);
    }
}
