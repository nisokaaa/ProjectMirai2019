using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialLoop : MonoBehaviour {

    [SerializeField]
    GameObject _tutorialPointStartPosition;

    //[SerializeField]
    //GameObject _tutorialPointEndPosition;

    [SerializeField]
    bool _bPositionReset = false;

    Transform _player;

    // Use this for initialization
    void Start () {
        _player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void Update () {
        //強制

        //位置初期化処理
		if(_bPositionReset == true)
        {
            //Vector3 newPos = new Vector3(
            //    _player.transform.position.x,
            //    _tutorialPointStartPosition.transform.worldToLocalMatrix.m20,
            //    _player.transform.position.y);
            _player.transform.position =
                new Vector3(_player.position.x,
                _player.position.y,
                _tutorialPointStartPosition.transform.position.z);
                

            _bPositionReset = false;
        }
	}
    public void SetPositionReset()
    {
        _bPositionReset = true;
    }
}
