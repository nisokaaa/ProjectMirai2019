﻿using System.Collections;
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
    [SerializeField] GameObject TutorialObject;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}