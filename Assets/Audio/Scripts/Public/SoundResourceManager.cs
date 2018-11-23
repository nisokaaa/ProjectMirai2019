using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundResourceManager : MonoBehaviour {

    [SerializeField]
    int m_ScoreValue;
    
    // Use this for initialization
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        Debug.Log("スコア"+ m_ScoreValue);
	}

    //スコアの加算
    public void AddScoreValue(int value)
    {
        m_ScoreValue += value;
    }

    //スコアの減算
    public void SubScoreValue(int value)
    {
        m_ScoreValue -= value;
    }

    //値の取得
    public int GetScoreValue()
    {
        return m_ScoreValue;
    }

    //値の代入
    public void SetScoreValue(int scoreValue)
    {
        m_ScoreValue = scoreValue;
    }
}
