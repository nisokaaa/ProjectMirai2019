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

    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
