using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager_Canvas_TextGameScore : MonoBehaviour {

    ScoreManager scoreManager;
    
    // Use this for initialization
    void Start()
    {
        if (scoreManager == null)
        {
            scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = scoreManager.GetScoreValue().ToString();
    }
}
