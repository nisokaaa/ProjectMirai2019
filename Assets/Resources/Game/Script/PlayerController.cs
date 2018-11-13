using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    
    public float gravity = 20.0F;   //重力の強さ

    // アクセル状態
    bool bAccelerator = false;

    // デバッグ用
    [SerializeField]
    bool bBack = false;

    [SerializeField, Range(0f, 5000f)]
    int jumpPower = 100;
    bool jump = false;

    PlayerColliderCheck playerColliderCheck;            //bJumpアニメーション用スクリプト

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    
    void Start()
    {
        if (playerColliderCheck == null)
        {
            playerColliderCheck = GetComponent<PlayerColliderCheck>();
        }

        //joycon
        //ジョイコンのインスタンスを取得する
        m_joycons = JoyconManager.Instance.j;

        if (m_joycons == null || m_joycons.Count <= 0) return;

        m_joyconL = m_joycons.Find(c => c.isLeft);      //ジョイコンL　緑
        m_joyconR = m_joycons.Find(c => !c.isLeft);     //ジョイコンR・赤
    }

        void Update()
    {
        // アクセルボタンInput
        bAccelerator = Input.GetKey("joystick button 5") ? true : false;
        bAccelerator = Input.GetKey(KeyCode.W) ? true : false;
        //bAccelerator = Input.GetKey(KeyCode.Joystick1Button0) ? true : false;
        
        
         

        bBack = Input.GetKey(KeyCode.S) ? true : false;

        if (!(m_joycons.Count <= 0 || m_joycons == null))
        {
            if (m_joyconR.GetButtonDown(Joycon.Button.DPAD_DOWN))
            {
                jump = true;
            }
            transform.Rotate(new Vector3(0, 1, 0), m_joyconL.GetStick()[0]);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if (playerColliderCheck.GetCollisionEnterExit() == false)
        {
            
            jump = false;
        }

        
        // 回転
        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("L_Stick_H"));
        transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Horizontal"));

        
        //transform.Rotate(new Vector3(0, 1, 0), Input.GetAxis("Horizontal 1"));

        if (bBack == false)
        {
            return;
        }
        // forwardのRay
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }

    void FixedUpdate()
    {
        Rigidbody rb = gameObject.GetComponent<Rigidbody>();

        // アクセル押下してたら速度代入
        speedCurrent = bAccelerator ? speed : 0.0f;
        if(!bAccelerator) speedCurrent = bBack ? -speed : 0.0f;     //アクセルが押されてないとき
        if(!(m_joycons.Count <= 0 || m_joycons== null))
        {
            if (m_joyconL.GetStick()[1] > 0.3f)
            {
                speedCurrent = speed * m_joyconL.GetStick()[1];
            }
        }

        Vector3 moveVector = Vector3.zero;
        moveVector = speedCurrent * transform.forward;

        //重力処理
        moveVector.y -= gravity * Time.deltaTime; //マイナスのY方向（下向き）に重力を与える

        if (jump == true)
        {
            jump = false;
            rb.AddForce(Vector3.up * jumpPower);
        }

        // ボタン非押下で勝手に速度減衰
        rb.AddForce(moveForceMultiplier * (moveVector - rb.velocity));

        // Rayが地面にあたってないときは速度減衰をやわらげよう・・・・
        //Debug.Log(rb.velocity);
    }
}
