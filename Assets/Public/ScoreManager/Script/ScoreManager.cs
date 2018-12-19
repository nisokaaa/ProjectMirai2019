using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 「スコアを管理するクラス」
/// 引数　 ：解説
/// 戻り値 ：解説
/// １．手順
/// ２．手順
/// ３．手順
/// 作成者：志村まさき
/// </summary>
public class ScoreManager : MonoBehaviour {

    [SerializeField] float maxLimitValue;
    [SerializeField] float minLimitValue;
    [SerializeField] float ValuePercentage;
    public float scoreValue;    //現在のデータ保存

    [Header("デバック処理")]
    [SerializeField] bool debugScore;

    //プレイヤー関連
    PlayerElecMode _playerElecMode;
    DischargeAction _playerDischargeAction;
    int _addScoreCnt = 0;

    // Use this for initialization
    void Start () {
        _playerElecMode = GameObject.Find("Player").GetComponent<PlayerElecMode>();
        _playerDischargeAction = GameObject.Find("Player").GetComponent<DischargeAction>();

    }
	
	// Update is called once per frame
	void Update () {
        if(_playerElecMode.GetMode() == true)
        {
            AddScoreValue(1);
        }
        _addScoreCnt++;

        if (_playerDischargeAction.GetPlayerMode() == DischargeAction.ELEC_MODE.EXECUTION)
        {
            if (_addScoreCnt > 60)
            {
                _addScoreCnt = 0;
                AddScoreValue(10);
            }
             
        }
        ValuePercentage = (scoreValue / maxLimitValue) * 100;

        DebugScoreValue();
    }

    //加算
    public void AddScoreValue(float value)
    {
        scoreValue += value;
    }

    //減算
    public void SubScoreValue(float value)
    {
        scoreValue -= value;
    }

    //取得
    public float GetScoreValue()
    {
        return scoreValue;
    }

    //設定
    public void SetScoreValue(float value)
    {
        scoreValue = value;
    }

    //データの破棄
    public void ResetScoreValue()
    {
        scoreValue = 0;
    }

    void DebugScoreValue()
    {
        if(debugScore == false)
        {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddScoreValue(10);
        }
    }
}