using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//フラグがONの時にプレイヤーを固定座標に留めるスクリプト
public class BossBattlePlayerStartPosition : MonoBehaviour {

    GameObject _player;
    public bool _bStopPlayer;

	// Use this for initialization
	void Start () {
        _player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
        if(_bStopPlayer == false)
        {
            return;
        }
        _player.transform.position = transform.position;
	}

    public void SetPlayerStop()
    {
        _bStopPlayer = true;
    }

    public void SetPlayerStart()
    {
        _bStopPlayer = false;
    }
}
