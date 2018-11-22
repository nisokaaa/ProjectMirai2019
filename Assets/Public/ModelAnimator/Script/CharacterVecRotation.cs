//キャラクターのベクトルを取得してオブジェクトを回転させる
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「キャラクターの移動ベクトルを見て、その方向に回転させる」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．手順
/// ２．手順
/// ３．手順
/// 作成者：志村まさき
/// </summary>
public class CharacterVecRotation : MonoBehaviour {

    [SerializeField] GameObject player;
    [SerializeField] Vector3 playerVecCharacterPos;

    [SerializeField] bool bVecMoveRotation=true;

    [SerializeField]
    bool bActive = false;

    [SerializeField]
    bool bPlayerPosVecRotation = false;

    [SerializeField]
    public GameObject targetBlack;

    public enum MODE
    {
        NONE = 0,
        CHAR_VEC_ROT,
        TARGET_ROT
    };

    public MODE mode = MODE.NONE;

    // Use this for initialization
    void Start () {
        //ベクトルの起点
        playerVecCharacterPos = player.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        
        if (bActive == false)
        {
            return;
        }

        switch(mode)
        {
            case MODE.NONE:
                break;
            case MODE.CHAR_VEC_ROT:
                VecMoveRotation();
                break;
            case MODE.TARGET_ROT:
                var aim = this.targetBlack.transform.position - this.transform.position;
                var look = Quaternion.LookRotation(aim, Vector3.up);
                this.transform.localRotation = look;
                break;
        }
	}

    public void SetCharRot_Vec()
    {
        mode = MODE.CHAR_VEC_ROT;
    }
    public void SetCharRot_Target()
    {
        mode = MODE.TARGET_ROT;
    }
    public void VecMoveRotation()
    {
        //移動方向ベクトルの取得
        //Vector3 charVec = playerVecCharacterPos - transform.position;
        Vector3 charVec = transform.position - playerVecCharacterPos;

        if (charVec.magnitude > 0.01f)
        {
            //回転量を関数によって取得し、rotationに入れる
            transform.rotation = Quaternion.LookRotation(charVec);
        }

        //座標の更新
        playerVecCharacterPos = transform.position;
    }

    //第一引数のポジションの方向に自分が向く
    public void SetPosRotation( Vector3 Position)
    {
        ////移動方向ベクトルの取得
        Vector3 charVec = transform.position - Position;

        if (charVec.magnitude > 0.01f)
        {
            //回転量を関数によって取得し、rotationに入れる
            transform.rotation = Quaternion.LookRotation(charVec);
        }
        //座標の更新
        playerVecCharacterPos = transform.position;
    }
}
