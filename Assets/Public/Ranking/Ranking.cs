//http://www.project-unknown.jp/entry/2017/09/22/124614
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : SingletonMonoBehaviour<Ranking> {

    string RANKING_KEY = "ranking";
    int RANKING_NUM = 99;
    List<int> _ranking = new List<int>();
    int _myScore = 0;
    int cnt = -1;

    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start () {
        
        
        
        
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// ランキングにデータを保存する
    /// </summary>
    /// <param name="myScore"></param>
    public void SetRanking(int myScore)
    {
        cnt++;
        _myScore = myScore;
        _ranking.Add(myScore);
    }

    public int GetRankingVal()
    {
        return _ranking.IndexOf(_myScore) + 1;
    }

    /// <summary>
    /// 自分のスコアデータを取得する
    /// </summary>
    /// <returns></returns>
    public float GetMyScore()
    {
        return _myScore;
    }

    /// <summary>
    /// 任意のランキングのスコアデータを取得する
    /// </summary>
    /// <param name="rankingVal"></param>
    /// <returns></returns>
   public float GetRanking(int rankingVal)
    {
        if(cnt < rankingVal)
        {
            return 0000;
        }
        //if(cnt <= rankingVal)
        //{
        //    return 9999;
        //}
        //ソート
        _ranking.Sort((a, b) => b - a);

        return _ranking[rankingVal];
    }
}
