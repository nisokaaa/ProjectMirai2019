using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;  //シーンの名前取得用

/// <summary>
/// 「」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．手順
/// ２．手順
/// ３．手順
/// 作成者：志村まさき
/// </summary>
public class Result : MonoBehaviour {

    enum WAVE
    {
        NONE = 0,
        START,
        PLAY,
        PLAY_RANKING,
        END,
        MAX
    };

    [SerializeField] WAVE result = WAVE.NONE;

    LedController _ledController;
    public bool _bRanking = false;
    Ranking _ranking;

    // Use this for initialization
    void Start () {
        result = WAVE.START;
        SceneChangeController.Instance.FadeOut();
        _ledController = GameObject.Find("ledController").GetComponent<LedController>();
        _ranking = GameObject.Find("Ranking").GetComponent<Ranking>();
    }
	
	// Update is called once per frame
	void Update () {
        string text = SceneManager.GetActiveScene().name;
        if (text != "Ending")
        {
            return;
        }
        if(_bRanking == true)
        {
            _ledController.SetRankingMode();
        }
        else
        {
            _ledController.SetEnding();
        }
        switch (result)
        {
            case WAVE.START:
                result = WAVE.PLAY;
                break;
            case WAVE.PLAY:
                if (Input.anyKeyDown)
                {
                    _ranking.SetStart();
                    result = WAVE.PLAY_RANKING;
                }
                    break;
            case WAVE.PLAY_RANKING:
                if (Input.anyKeyDown)
                {
                    //Debug.Log("まじで？");
                    SceneChangeController.Instance.SetTime(400);
                    SceneChangeController.Instance.SetChangeScene("Title");
                    result = WAVE.END;
                }
                break;
            case WAVE.END:
                SceneChangeController.Instance.SetChangeSceneExecution();
                //SceneChangeController.Instance.FadeIn();
                result = WAVE.NONE;
                break;
        }
    }
}
