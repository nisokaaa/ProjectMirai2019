using UnityEngine;
using System.Collections;
/*
 Log
 下記導入済み
 TITLE:SceneChange/Title
 RANKING_MODE:SceneChange/Result
 GAME_MODE:SceneChange/Game
 BOSS_BATTLE_START:SceneChange/Game
 ENDING:SceneChange/Result
 PLAYER_ACTION※未実装

サンプルメモ
 LedController _ledController;
 _ledController = GameObject.Find("ledController").GetComponent<LedController>();
*/
/// <summary>
/// 電飾制御用のスクリプト
/// 制作者：山下航平 追記修正：志村昌紀
/// テスト方法：「1」Key~「6」Keyで実行テスト出来ます。
/// 実装方法：ヒエラルキーに「serialHandler」、「ledController」を配置して、
/// 「serialHandler」のインスペクターの「_bNonActive」のﾁｪｯｸﾎﾞｯｸｽをtrueにすると出来ます。
///</summary>
public class LedController : MonoBehaviour
{ 
    public SerialHandler serialHandler;

    //状態
    const string TITLE = "1";                   //優先度 大：タイトル待機状態
    const string RANKING_MODE = "2";            //優先度 大：ランキング画面中
    const string GAME_MODE = "3";               //優先度 大：ゲーム中
    const string ENDING = "4";                  //優先度 中：エンディングシーン中
    const string BOSS_BATTLE_START = "5";       //優先度 中：ボスが登場したとき
    const string PLAYER_ACTION = "6";           //優先度 小：プレイヤー特殊アクション中※未実装

    SerialHandler _serialHandler;

    private void Start()
    {
        _serialHandler = GameObject.Find("serialHandler").GetComponent<SerialHandler>();
    }

    void Update()
    {
        Sample();
    }
    
    /// <summary>
    /// タイトル状態の時に実行する
    /// </summary>
    public void SetTitle()
    {
        SetLED(TITLE);
    }

    /// <summary>
    /// ランキング状態の時実行する
    /// </summary>
    public void SetRankingMode()
    {
        SetLED(RANKING_MODE);
    }

    /// <summary>
    /// ゲームの状態の時実行する
    /// </summary>
    public  void SetGameMode()
    {
        SetLED(GAME_MODE);
    }

    /// <summary>
    /// ボスが登場したとき実行する
    /// </summary>
    public void SetBossBattleStart()
    {
        SetLED(BOSS_BATTLE_START);
    }

    /// <summary>
    /// エンディング状態の時実行する
    /// </summary>
    public void SetEnding()
    {
        SetLED(ENDING);
    }

    /// <summary>
    /// プレイヤーが特殊アクションを実行したとき実行する
    /// </summary>
    public void SetPlayerAction()
    {
        SetLED(PLAYER_ACTION);
    }

    //サンプル用の関数
    void Sample()
    {
        if (Input.GetKey(KeyCode.Alpha1)) { serialHandler.Write(TITLE);}
        if (Input.GetKey(KeyCode.Alpha2)) { serialHandler.Write(RANKING_MODE); }
        if (Input.GetKey(KeyCode.Alpha3)) { serialHandler.Write(GAME_MODE); }
        if (Input.GetKey(KeyCode.Alpha4)) { serialHandler.Write(BOSS_BATTLE_START); }
        if (Input.GetKey(KeyCode.Alpha5)) { serialHandler.Write(ENDING); }
        if (Input.GetKey(KeyCode.Alpha6)) { serialHandler.Write(PLAYER_ACTION); }
    }

    // string型なので"num"といった形の引数
    public void SetLED(string num)
    {
        if (_serialHandler._bNonActive) { return; }
        serialHandler.Write(num);
    }
}