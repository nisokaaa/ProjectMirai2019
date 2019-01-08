using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームの進行状況を把握
/// シナリオの再生などを行う
/// </summary>
public class GameManager : MonoBehaviour {

    GameObject _pauseCanvas;
    Pausable _pausable;

    [SerializeField]
    bool _bTutorial = false;

    [SerializeField]
    GameObject _tutorial;

    [SerializeField]
    int _phase = 0;

    [SerializeField]
    TimerScript timerScript;

    ScoreManager _scoreManager;

    // Use this for initialization
    void Start () {
        _tutorial.SetActive(false);
        _scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }
	
	// Update is called once per frame
	void Update () {
		switch(_phase)
        {
            //ゲームオープニング
            case 0:
                
                break;
            //チュートリアル
            case 1:
                break;
            //ゲーム
            case 2:
                break;
            //ボス
            case 3:
                break;
        }
	}
    public void SetTutorialOn()
    {
        _tutorial.SetActive(true);
    }
    public void SetScoreManagerOn()
    {
        //_timeManager.SetActive(true);
        //_scoreManager.SetActive(true);
    }
    public void SetGameStart()
    {
        timerScript.SetActiveOn();
        _scoreManager.SetActiveOn();
    }
}
