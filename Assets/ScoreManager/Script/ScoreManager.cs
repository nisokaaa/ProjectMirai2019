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
    
    

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
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