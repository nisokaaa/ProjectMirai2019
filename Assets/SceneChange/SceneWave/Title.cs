using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンの名前取得用

/// <summary>
/// 「タイトル処理」
/// 流れ
/// １．タイトル開始演出
/// ２．通常タイトル表示
/// ３．タイトル終了演出
/// 作成者：志村まさき
/// </summary>
public class Title : MonoBehaviour {
    
    enum WAVE
    {
        NONE = 0,
        START,
        PLAY,
        END,
        MAX
    };

    [SerializeField] WAVE title = WAVE.NONE;

    // Use this for initialization
    void Start () {
        title = WAVE.START;
    }
	
	// Update is called once per frame
	void Update () {
        string text = SceneManager.GetActiveScene().name;
        if(text != "Title")
        {
            return;
        }

        switch (title)
        {
            case WAVE.START:
                title = WAVE.PLAY;
                break;
            case WAVE.PLAY:
                if(Input.anyKey)
                {
                    SceneChangeController.Instance.SetChangeScene("Game");
                    title = WAVE.END;
                }
                break;
            case WAVE.END:
                SceneChangeController.Instance.SetChangeSceneExecution();
                title = WAVE.NONE;
                break;
        }
    }
}