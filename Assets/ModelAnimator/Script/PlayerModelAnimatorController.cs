﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「プレイヤーのアニメーターを操作する関数」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．プレイヤーゲームオブジェクトの下に配置する。
/// ２．アニメーションのデバックしたい場合は、デバックフラグをインスペクターで立てる。
/// ３．指定のアクションキーを押す
/// デバック操作
/// ＞スペース：ジャンプ
/// ＞B:攻撃
/// ＞N:シャガミ
/// 作成者：志村まさき
/// </summary>
public class PlayerModelAnimatorController : MonoBehaviour
{
    [Header("プレイヤーゲームオブジェクトの子としてインスペクターで配置")]
    [SerializeField] private Animator animator;         //操作するアニメーター

    //アニメーションフラグ
    [SerializeField, Range(0.0f, 1.0f)] float speed;    //歩く / 走る
    [SerializeField] bool bClimd;                        //しゃがむ
    [SerializeField] bool bJump;                         //ジャンプ
    [SerializeField] bool bWalljump;         　          //ジャンプ壁すり
    [SerializeField] bool bAttack;       　　            //普通の攻撃
    [SerializeField] bool bJumpAttack;                   //ジャンプ中のアタック
    [SerializeField] bool bSpecialAttack;                //スペシャルアタック

    [SerializeField] GameObject player;                 //プレイヤーの情報取得用
    [SerializeField] Vector3 playerVecCharacterPos;   //プレイヤーの向きベクトル取得用変数

    PlayerColliderCheck playerColliderCheck;            //bJumpアニメーション用スクリプト
    Rigidbody playerRigidbody;                          //bJump仮実装用物理演算用コンポーネント
    Vector3 charVec;                                    //走るようコンポーネント

    enum STATE_ANIM
    {
        STATE_NONE = 0,
        STATE_JUMP,
        STATE_CLIMD,
        STATE_ATTACK,
        STATE_MAX
    }

    [SerializeField] STATE_ANIM stateLock = STATE_ANIM.STATE_NONE;
    [SerializeField] STATE_ANIM oldStateLock = STATE_ANIM.STATE_NONE;

    [Header("プレイヤーアニメーション用デバックフラグ<Space:ジャンプ><B:攻撃><N:スライディング>")]
    [SerializeField] bool bDebugMode = false;
    
    // Use this for initialization
    void Start()
    {
        playerVecCharacterPos = player.transform.position;

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if(playerRigidbody == null)
        {
            playerRigidbody = player.GetComponent<Rigidbody>();
        }

        if(playerColliderCheck == null)
        {
            playerColliderCheck = player.GetComponent<PlayerColliderCheck>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //攻撃アクションのテストコード
        //if (Input.GetKey(KeyCode.N))
        //{
        //    PlayerAtackControl(true);
        //}
        //else
        //{
        //    PlayerAtackControl(false);
        //}

        if (bDebugMode == true)
        {
            //テストコード、仮システム
            DebugTestController();
        }
        //アニメーション監視
        
        //PlayerJumpAttack();
        PlayerJump();
        PlayerRun();
        PlayerClimd();
        PlayerAttack();

    }

    void initParameter()
    {
        bClimd = false;
    }

    void DebugTestController()
    {
        //仮ジャンプ実装
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRigidbody.AddForce(Vector3.up * 1000);
        }

        //しゃがみアクションのテストコード
        if (Input.GetKey(KeyCode.B))
        {
            PlayerClimdControl(true);
        }else
        {
            PlayerClimdControl(false);
        }
    }

    //再生中のアニメーション以外はtrueを返す
    bool SetStateLock(bool play , STATE_ANIM anim)
    {
        //アニメーションを再生したい　かつ　現在の状態が同じものの場合
        if(play == true && anim == stateLock || play == true && STATE_ANIM.STATE_NONE == stateLock)
        {
            stateLock = anim;
            return false;
        }

        return true;
    }
    //過去と現在で違う場合はリセット
    void PlayerStateLock()
    {
        if(oldStateLock != stateLock )
        {
            stateLock = STATE_ANIM.STATE_NONE;
        }
        oldStateLock = stateLock;
    }
    //走る、歩く
    void PlayerRun()
    {
        animator.SetFloat("ran", speed);

        //移動方向ベクトルの取得
        charVec = playerVecCharacterPos - transform.position;

        if (charVec.magnitude > 0.01f)
        {
            speed = Mathf.Clamp01(charVec.magnitude);
            animator.SetFloat("ran", speed);
        }

        //座標の更新
        playerVecCharacterPos = transform.position;
    }
    
    //ジャンプ
    void PlayerJump()
    {
        if (bJumpAttack == true)
        {
            bJump = false;
            return;
        }

        if (playerColliderCheck.GetCollisionEnterExit())
        {
            bJump = false;
        }
        else
        {
            bJump = true;
        }

        
        //アニメーションの設定
        animator.SetBool("jump", bJump);
    }

    //しゃがむ
    void PlayerClimd()
    {
        animator.SetBool ("climd", bClimd);
    }
    void PlayerClimdControl(bool play)
    {
        bClimd = play;
    }

   //攻撃
    void PlayerAttack()
    {
        animator.SetBool("attack", bAttack);
    }
    public void PlayerAtackControl(bool play)
    {
        bAttack = play;
    }

    //ジャンプ攻撃
    //void PlayerJumpAttack()
    //{
    //    animator.SetBool("jumpAttack", bJumpAttack);
    //}
    //void PlayerJumpAttackControl(bool play)
    //{
    //    bJumpAttack = play;
    //    
    //
    //    //地面に着いたらステート変える
    //    if(playerColliderCheck.GetCollisionEnterExit())
    //    {
    //        bJumpAttack = false;
    //    }
    //}

    //ジャンプ壁すり
    void PlayerWallJump()
    {

    }

    //スペシャル攻撃１
    void PlayerSpecialAttack1()
    {

    }

    //スペシャル攻撃２
    void PlayerSpecialAttack2()
    {

    }
}