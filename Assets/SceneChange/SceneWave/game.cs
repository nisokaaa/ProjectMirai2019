using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンの名前取得用

/// <summary>
/// 「ゲームの処理」
/// 現在のゲームの状態を把握して、シーンの流れを変える
/// 流れ
/// １．
/// ２．
/// ３．
/// 作成者：志村まさき
/// </summary>
public class Game : MonoBehaviour
{
    enum WAVE
    {
        NONE = 0,
        TUTORIAL_START,
        TUTORIAL_000,
        TUTORIAL_001,
        TUTORIAL_END,
        GAME_START,
        GAME_000,
        GAME_END,
        GAME_BOSS_START,
        GAME_BOSS_000,
        GAME_BOSS_END,
        MAX
    };

    WAVE game = WAVE.NONE;

    BossBattleEnd bossBattleEnd;

    bool _check = false;

    // Use this for initialization
    void Start () {
        string text = SceneManager.GetActiveScene().name;

        if (text != "Game" || text != "GameSim")
        {
            return;
        }

        if (bossBattleEnd == null && text == "Game")
        {
            bossBattleEnd = GameObject.Find("BossBattleEnd").GetComponent<BossBattleEnd>();
        }
        SceneChangeController.Instance.FadeOut();
    }
	
	// Update is called once per frame
	void Update () {
        string text = SceneManager.GetActiveScene().name;
        
        //Debug.Log(text);
        //Debug.Log("やばいで！" + text);

        if (text != "Game")
        {
            return;
        }

        
        if(bossBattleEnd == null)
        {
            return;
        }
        
        //  Debug.Log("test");
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SceneChangeController.Instance.SetChangeScene("Ending");
            SceneChangeController.Instance.SetChangeSceneExecution();
            SceneChangeController.Instance.FadeIn();
        }

        if (bossBattleEnd.GetBossBattleFlag() == false)
        {
            return;
        }

        if(Input.anyKeyDown)
        {
            SceneChangeController.Instance.SetChangeScene("Result");
            SceneChangeController.Instance.SetChangeSceneExecution();
            SceneChangeController.Instance.FadeIn();
        }
        
    }
}