?�using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンの名前取得用

/// <summary>
/// 「SceneChange関係�?�??タを保持して�?��ゲー�?オブジェクト用のクラス�?
/// 引数�? ??
/// 戻り�?� ??
/// ?��?
/// ?��?
/// 作�?�?��志村まさき
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
        //�?��トコー�?
        
        //次のシーンの読み込み処�?
		if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneChangeController.Instance.SetChangeScene("Game");
        }

        //シーンの�?��替え実�?
        if(Input.GetKeyDown(KeyCode.F2))
        {
            SceneChangeController.Instance.SetChangeSceneExecution();
        }
    }
    
}
