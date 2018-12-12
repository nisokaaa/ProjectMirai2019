using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「フィールドに立っているか確認するクラス」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．手順
/// ２．手順
/// ３．手順
/// 作成者：志村まさき
/// </summary>
public class PlayerColliderCheck : MonoBehaviour {

    [SerializeField] bool tagPlane_CollisionEnter;
    bool _SE = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(_SE == true)
        {
            AudioManager.Instance.PlaySE(AUDIO.SE_GAME_JUMP_000);
            _SE = false;    
        }

    }

    /// <summary>
    /// オブジェクトが当たっているかちぇっくする
    /// </summary>
    /// <returns></returns>
    public bool GetCollisionEnterExit()
    {
        return tagPlane_CollisionEnter;
    }

    //衝突したとき
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Plane")
        {
            _SE = true;
            tagPlane_CollisionEnter = true;
        }
    }

    //離れた時
    private void OnCollisionExit(Collision collision)
    {
        tagPlane_CollisionEnter = false;
    }

    //振れている間
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Plane")
        {
            tagPlane_CollisionEnter = true;
        }
    }
}
