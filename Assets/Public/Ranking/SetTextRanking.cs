using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetTextRanking : MonoBehaviour {
    
    Text _text;
    Ranking _ranking;

    [SerializeField]
    int RANKING_NO;

    [SerializeField]
    bool mySocore;

    [SerializeField]
    bool myRanking;

    // Use this for initialization
    void Start () {
        _text = GetComponent<Text>();
        _ranking = GameObject.Find("Ranking").GetComponent<Ranking>();
	}
	
	// Update is called once per frame
	void Update () {
        if(myRanking == true)
        {
            _text.text = _ranking.GetRankingVal().ToString("f0");
            return;
        }

        if(mySocore == true)
        {
            _text.text = _ranking.GetMyScore().ToString("f1");
            return;
        }
        _text.text = _ranking.GetRanking(RANKING_NO).ToString("f1");
    }
}
