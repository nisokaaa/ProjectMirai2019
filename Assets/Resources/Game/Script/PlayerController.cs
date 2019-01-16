using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

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

    [SerializeField, Range(0f, 10000f)]
    int JumpElecPower = 500;

    int jumpPowerValue = 0;
    bool jump = false;
    
    PlayerElecMode playerElecMode;

    PlayerColliderCheck playerColliderCheck;            //bJumpアニメーション用スクリプト

    private List<Joycon> m_joycons;
    private Joycon m_joyconL;
    private Joycon m_joyconR;
    private CharacterController charaCon;       // キャラクターコンポーネント用の変数
    private NavMeshAgent agent;

    public bool PlayerControlOff = false;
    PlayerModelAnimatorController playerModelAnimatorController;
    int _ChargeCnt = 0;
    bool _ElecCharge = false;
    ElecBarControl _elecBarControl;
    void Start()
    {
        _elecBarControl = GameObject.Find("ElecBarController").GetComponent<ElecBarControl>();
        playerModelAnimatorController = GameObject.Find("PlayerModelAnimatorController").GetComponent<PlayerModelAnimatorController>();
        if (playerElecMode == null)
        {
            playerElecMode = GetComponent<PlayerElecMode>();
        }
        agent = GetComponent<NavMeshAgent>();
        charaCon = GetComponent<CharacterController>(); // キャラクターコントローラーのコンポーネントを参照する
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
        if (PlayerControlOff == true) { return; }

        //充電処理
        if (!(m_joycons.Count <= 0 || m_joycons == null))
        {
            if ((m_joyconL.GetGyro().x > 3 || m_joyconL.GetGyro().x < -3) && (m_joyconR.GetGyro().x > 3 || m_joyconR.GetGyro().x < -3))
            {
                if (_ElecCharge == false)
                {
                    m_joyconL.SetRumble(160, 320, 0.6f, 200);
                    m_joyconR.SetRumble(160, 320, 0.6f, 200);
                    _ElecCharge = true;
                    _elecBarControl.SetEffectOn();
                }
            }
            if (_ElecCharge == true)
            {
                _ChargeCnt++;
                _elecBarControl.Increase();
                _elecBarControl.Increase();
                if (_ChargeCnt > 60)
                {
                    _elecBarControl.SetEffectOff();
                    _ChargeCnt = 0;
                    _ElecCharge = false;
                }
            }
        }
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

        if (playerElecMode.GetMode() == true)
        {
            jumpPowerValue += JumpElecPower;
        }
        else
        {
            jumpPowerValue = jumpPower;
        }
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
            if (PlayerControlOff == false)
            {
                if (m_joyconL.GetStick()[1] > 0.3f)
                {
                    speedCurrent = speed * m_joyconL.GetStick()[1];
                }
            }
        }
        
        Vector3 moveVector = Vector3.zero;
        moveVector = speedCurrent * transform.forward;
        

        //加算重力処理
        if (playerColliderCheck.GetCollisionEnterExit() == false)
            moveVector.y -= gravity * Time.deltaTime; //マイナスのY方向（下向き）に重力を与える

        if (jump == true)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_GAME_JUMP_001);
            jump = false;
            rb.AddForce(Vector3.up * jumpPowerValue);
        }

        // ボタン非押下で勝手に速度減衰
        rb.AddForce(moveForceMultiplier * (moveVector - rb.velocity));
        // Rayが地面にあたってないときは速度減衰をやわらげよう・・・・
        //Debug.Log(rb.velocity);
    }

    public void SetPlayerControlOff()
    {
        PlayerControlOff = true; 
    }
    public void SetPlayerControlOn()
    {
        playerModelAnimatorController.SetAnimLockOff();
        PlayerControlOff = false;
    }
    public bool GetPlayerControl()
    {
        return PlayerControlOff;
    }
}
