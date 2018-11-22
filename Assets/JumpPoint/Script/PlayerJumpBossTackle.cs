using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpBossTackle : MonoBehaviour {

    [SerializeField]
    GameObject _JumpPointObject;

    [SerializeField]
    bool _Jump = false;

    [SerializeField]
    GameObject objB;

    [SerializeField]
    float dis;

    [SerializeField]
    private TimeManager timeManager;

    // Use this for initialization
    void Start () {
        _JumpPointObject = Instantiate(_JumpPointObject, transform.position, Quaternion.identity)as GameObject;
        _JumpPointObject.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        //if()
        //{
        //    _Jump = true;
        //}

		if((timeManager.GetSlowFlag() == true) && Input.GetKeyDown(KeyCode.Space))
        {
            _JumpPointObject.transform.position = transform.position;
            _JumpPointObject.SetActive(true);
            _Jump = false;
            timeManager.SetNormalTime();
        }
        //Vector3 Apos = transform.position;
        //Vector3 Bpos = objB.transform.position;
        //dis = Vector3.Distance(Apos, Bpos);
    }
}
