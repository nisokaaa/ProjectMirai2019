using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「SceneChange関係のデータを保持しているゲームオブジェクト用のクラス」
/// 引数　 ：
/// 戻り値 ：
/// １．
/// ２．
/// 作成者：志村まさき
/// </summary>
public class SceneChangeMgr : SingletonMonoBehaviour<SceneChangeMgr>
{
    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //テストコード
        /*
        //次のシーンの読み込み処理
		if(Input.GetKeyDown(KeyCode.Q))
        {
            SceneChangeController.Instance.SetChangeScene("Title");
        }

        //シーンの切り替え実行
        if(Input.GetKeyDown(KeyCode.W))
        {
            SceneChangeController.Instance.SetChangeSceneExecution();
        }
        */
	}
    
}
