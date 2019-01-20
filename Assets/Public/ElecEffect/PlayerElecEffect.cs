using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//蓄電処理が通った時にエフェクトを流す
public class PlayerElecEffect : MonoBehaviour {

    [SerializeField] GameObject _elecGameObject; //電気オブジェクト
    [SerializeField] GameObject _gameObjectAPosPlayer; //プレイヤー用
    [SerializeField] GameObject _gameObjectBPosObject; //蓄電オブジェクト用
    GameObject _player;
    GameObject _target;

    int _cnt = 0;
    [SerializeField] bool _EffectStart = false;
    [SerializeField] GameObject _ElecSpher;

    // Use this for initialization
    void Start () {
        _player = GameObject.Find("Player");
        _elecGameObject.SetActive(false);
        _gameObjectAPosPlayer.SetActive(false);
        _gameObjectBPosObject.SetActive(false);
        _ElecSpher.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        

        if(_EffectStart == true)
        {
            _gameObjectAPosPlayer.transform.position = _player.transform.position;
            _gameObjectBPosObject.transform.position = _target.transform.position;
            //_ElecSpher.transform.position = _player.transform.position;
            _cnt++;
            if(_cnt++ > 500)
            {
                _cnt = 0;
                _EffectStart = false;
                _elecGameObject.SetActive(false);
                _gameObjectAPosPlayer.SetActive(false);
                _gameObjectBPosObject.SetActive(false);
                _ElecSpher.SetActive(false);
            }
        }
	}

    public void SetElecPosition(GameObject gameObject)
    {
        _elecGameObject.SetActive(true);
        _gameObjectAPosPlayer.SetActive(true);
        _gameObjectBPosObject.SetActive(true);
        _ElecSpher.SetActive(true);
        _EffectStart = true;
        _target = gameObject;
    }
}
