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

    [SerializeField]
    string text;

    [SerializeField]
    Ranking _ranking;

    ScoreManager _scoreManager;
    LedController _ledController;

    public bool _bBossMove = false;
    bool _Lock1 = false;
    bool _Lock2 = false;

    // Use this for initialization
    void Start () {
        text = SceneManager.GetActiveScene().name;

        if (text != "Game")
        {
            return;
        }

        if (bossBattleEnd == null && text == "Game")
        {
            bossBattleEnd = GameObject.Find("BossBattleEnd").GetComponent<BossBattleEnd>();
        }
        _ledController = GameObject.Find("ledController").GetComponent<LedController>();
        SceneChangeController.Instance.FadeOut();

        _ranking = GameObject.Find("Ranking").GetComponent<Ranking>();
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        _Lock1 = false;
        _Lock2 = false;
    }
	
	// Update is called once per frame
	void Update () {
        text = SceneManager.GetActiveScene().name;
        
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

        if(_bBossMove == false)
        {
            if(_Lock1 == false)
            {
                _Lock1 = true;
                _ledController.SetGameMode();
            }
        }
        else
        {
            if (_Lock2 == false)
            {
                _Lock2 = true;
                _ledController.SetBossBattleStart();
            }
            
        }
        


        if (Input.GetKeyDown(KeyCode.F1))
        {
            //ランキング登録
            _ranking.SetRanking(_scoreManager.GetScoreValue());

            Debug.Log("Endingに移行します");
            SceneChangeController.Instance.SetTime(1);
            SceneChangeController.Instance.SetChangeScene("Ending");
            SceneChangeController.Instance.SetChangeSceneExecution();
            SceneChangeController.Instance.FadeIn();
            //SceneChangeController.Instance.SetTime(400);
        }

        if (bossBattleEnd.GetBossBattleFlag() == false)
        {
            return;
        }

        if(Input.anyKeyDown)
        {
            //ランキング登録
            _ranking.SetRanking(_scoreManager.GetScoreValue());

            Debug.Log("Endingに移行します");
            SceneChangeController.Instance.SetTime(1);
            SceneChangeController.Instance.SetChangeScene("Ending");
            SceneChangeController.Instance.SetChangeSceneExecution();
            SceneChangeController.Instance.FadeIn();
        }
        
    }
}