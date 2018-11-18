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

        if (bVecMoveRotation == true)
        {
            VecMoveRotation();
        }
	}

    public void VecMoveRotation()
    {
        //移動方向ベクトルの取得
        Vector3 charVec = playerVecCharacterPos - transform.position;

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
        //Vector3 charVec = transform.position - Position;

        //if (charVec.magnitude > 0.01f)
        //{
        //    //回転量を関数によって取得し、rotationに入れる
        //    transform.rotation = Quaternion.LookRotation(charVec);
        //}
        ////座標の更新
        //playerVecCharacterPos = transform.position;
    }
}
