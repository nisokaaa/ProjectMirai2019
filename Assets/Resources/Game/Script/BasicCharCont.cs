/*
 * 引用
 http://inter-high-blog.unity3d.jp/2017/07/22/characonjump/
 
 */
using UnityEngine;
using System.Collections;

public class BasicCharCont : MonoBehaviour    // コンポーネントに追加できるクラスとして　PlayerControl_MadeBySome　を設定
{
    // このスクリプトで使う変数一覧

    //private:インスペクタで調整可能な変数
    private CharacterController charaCon;       // キャラクターコンポーネント用の変数
    private Animator animCon;  //  アニメーションするための変数
    private Vector3 moveDirection = Vector3.zero;   //  移動する方向とベクトル（動く力、速度）の変数（最初は初期化しておく）

    //public float:インスペクタで調整可能な変数
    public float idoSpeed = 5.0f;         // 移動速度
    public float rotateSpeed = 3.0F;     // 向きを変える速度
    public float kaitenSpeed = 1200.0f;   // プレイヤーの回転速度
    public float gravity = 20.0F;   //重力の強さ
    public float jumpPower = 6.0F;  //ジャンプのスピード

    //public void:謎の変数
    public void Hit()        // ヒット時のアニメーションイベント（今のところからっぽ。ないとエラーが出る）
    {
    }
    public void Jump()        // ヒット時のアニメーションイベント（今のところからっぽ。ないとエラーが出る）
    {
    }


    // ■最初に1回だけ実行する処理
    void Start()
    {
        charaCon = GetComponent<CharacterController>(); // キャラクターコントローラーのコンポーネントを参照する
        animCon = GetComponent<Animator>(); // アニメーターのコンポーネントを参照する
    }


    // ■毎フレーム常に実行する処理
    void Update()
    {

        // ▼▼▼カメラの難しい処理▼▼▼
        var cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;  //  カメラが追従するための動作
        Vector3 direction = cameraForward * Input.GetAxis("Vertical") + Camera.main.transform.right * Input.GetAxis("Horizontal");  //  テンキーや3Dスティックの入力（GetAxis）があるとdirectionに値を返す


        // ▼▼▼移動処理▼▼▼
        charaCon.Move(moveDirection * Time.deltaTime);  //CharacterControllerの付いているこのオブジェクトを移動させる処理

        if (Input.GetAxis("Vertical") == 0 && Input.GetAxis("Horizontal") == 0)  //  テンキーや3Dスティックの入力（GetAxis）がゼロの時の動作
        {
            //animCon.SetBool("Run", false);  //  Runモーションしない
        }

        else //  テンキーや3Dスティックの入力（GetAxis）がゼロではない時の動作
        {
            MukiWoKaeru(direction);  //  向きを変える動作の処理を実行する（後述）
            //animCon.SetBool("Run", true);  //  Runモーションする
        }


        // ▼▼▼落下処理▼▼▼
        if (charaCon.isGrounded)    //CharacterControllerの付いているこのオブジェクトが接地している場合の処理
        {
            //animCon.SetBool("Jump", Input.GetKeyDown("space") || Input.GetButtonDown("Jump"));  //  キーorボタンを押したらジャンプアニメを実行
            moveDirection.y = 0f;  //Y方向への速度をゼロにする
            moveDirection = direction * idoSpeed;  //移動スピードを向いている方向に与える

            if (Input.GetKeyDown("space") || Input.GetButtonDown("Jump")) //Spaceキーorジャンプボタンが押されている場合
            {
                moveDirection.y = jumpPower; //Y方向への速度に「ジャンプパワー」の変数を代入する
            }
            else //Spaceキーorジャンプボタンが押されていない場合
            {
                moveDirection.y -= gravity * Time.deltaTime; //マイナスのY方向（下向き）に重力を与える（これを入れるとなぜかジャンプが安定する…）
            }

        }
        else  //CharacterControllerの付いているこのオブジェクトが接地していない場合の処理
        {
            moveDirection.y -= gravity * Time.deltaTime;  //マイナスのY方向（下向き）に重力を与える
        }


        // ▼▼▼アクション処理▼▼▼
        //animCon.SetBool("Action", Input.GetKeyDown("x") || Input.GetButtonDown("Action1"));  //  キーorボタンを押したらアクションアニメを実行
        //animCon.SetBool("Action2", Input.GetKeyDown("z") || Input.GetButtonDown("Action2"));  //  キーorボタンを押したらアクション2アニメを実行
        //animCon.SetBool("Action3", Input.GetKeyDown("c") || Input.GetButtonDown("Action3"));  //  キーorボタンを押したらアクション3アニメを実行
    }


    // ■向きを変える動作の処理
    void MukiWoKaeru(Vector3 mukitaiHoukou)
    {
        Quaternion q = Quaternion.LookRotation(mukitaiHoukou);          // 向きたい方角をQuaternion型に直す
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, kaitenSpeed * Time.deltaTime);   // 向きを q に向けてじわ～っと変化させる.
    }
}