using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンの名前取得用

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

    //制御用スクリプト
    Title titleScript;
    Result resultScript;
    Game gameScript;
    // Use this for initialization
    void Start () {
		if(titleScript == null)
        {
            titleScript = GetComponent<Title>();
        }
        if(resultScript == null)
        {
            resultScript = GetComponent<Result>();
        }
        if(gameScript == null)
        {
            gameScript = GetComponent<Game>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        string text = SceneManager.GetActiveScene().name;

        if(text == "Title")
        {
            titleScript.enabled = true;
            gameScript.enabled = false;
            resultScript.enabled = false;
        }
        if(text == "Game")
        {
            titleScript.enabled = false;
            gameScript.enabled = true;
            resultScript.enabled = false;
        }
        if(text == "Result")
        {
            titleScript.enabled = false;
            gameScript.enabled = false;
            resultScript.enabled = true;
        }
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
